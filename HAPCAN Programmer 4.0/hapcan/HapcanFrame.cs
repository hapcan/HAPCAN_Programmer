using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.Programmer.Hapcan
{
    public class HapcanFrame
    {
        private static readonly ObjectIDGenerator ObjectIDGenerator = new ObjectIDGenerator();


        //CONSTRUCTORS
        /// <summary>
        /// The frame constructor
        /// </summary>
        /// <param name="data">Frame data buffer byte[15] or byte[12]</param>
        /// <param name="rxtx">Frame type Rx (received) if true or Tx (transmitted) if false</param>
        public HapcanFrame(byte[] data, bool rxtx)
        {
            if (data.Length != 15 && data.Length != 12)
                throw new ArgumentException("An invalid frame data length was supplied.");
            else
            {
                ID = ObjectIDGenerator.GetId(this, out bool firstTime);
                this.Time = DateTime.Now;
                this.RxTx = rxtx == true ? "Rx" : "Tx";
                Data = this.CopyDataToData(data);
                Description = "";
            }
        }

        /// <summary>
        /// The HAPCAN frame constructor
        /// </summary>
        /// <param name="data">Frame data as 12 byte hex string eg. "AB CD EF...". Throws exception if data is incorrect.</param>
        /// <param name="rxtx">Frame type Rx (received) if true or Tx (transmitted) if false</param>
        public HapcanFrame(string data, bool rxtx)
        {
            if (!IsDataCorrect(data))
                throw new ArgumentException("An invalid frame data string was supplied.");
            else
            {
                ID = ObjectIDGenerator.GetId(this, out bool firstTime);
                this.Time = DateTime.Now;
                this.RxTx = rxtx == true ? "Rx" : "Tx";
                Data = this.ConvertStringToData(data);
                Description = "";
            }
        }

        //PROPERTIES
        [Browsable(false)]
        public byte[] Data { get; private set; }
        public Int64 ID { get; private set; }
        public DateTime Time { get; }
        public string RxTx { get; }
        public string FrameData { get { return this.GetDataString(15); } }
        public string Description { get; set; }
        public enum ByteType : byte { StartByte = 0xAA, StopByte = 0xA5 }



        //METHODS
        //get 12byte or 15byte string
        public string GetDataString(int length)
        {
            string str = "";
            if (length == 12)
            {
                for (int i = 1; i <= 12; i++)
                {
                    str += string.Format("{0:X2} ", Data[i]);
                }
            }
            else if (length == 15)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    str += string.Format("{0:X2}{1}", Data[i], (i == 0 || i == 2 || i == 4 || i == 12) ? "  " : i != 14 ? " " : "");
                }
            }
            return str;
        }
        public int GetFrameType()
        {
            return (Data[1] * 256 + Data[2]) / 16;
        }
        //check if frame is sent as response
        public bool IsResponse()
        {
            if ((Data[2] & 0x01) == 0x01)
                return true;
            else
                return false;
        }
        public static bool IsCharHex(char chr)
        {
            //check allowed hex characters
            string hexChars = "0123456789abcdefABCDEF";
            if (!hexChars.Contains(chr))
                return false;
            else
                return true;
        }

        //check if 12 byte string can give correct Data
        public static bool IsDataCorrect(string msg)
        {
            msg = msg.Replace(" ", "");
            //check allowed hex characters
            foreach (char chr in msg)
            {
                if (IsCharHex(chr) == false)
                    return false;                 
            }
            //check data length
            if (msg.Length != 24)
                return false;
            else
                return true;
        }

        //convert 12 byte string to Data
        private byte[] ConvertStringToData(string msg)
        {
            if (!IsDataCorrect(msg))
                return null;
            else
            {
                byte[] data = new byte[15];
                msg = msg.Replace(" ", "");
                for (int i = 0; i < msg.Length; i += 2)
                {
                    data[i / 2 + 1] = Convert.ToByte(msg.Substring(i, 2), 16);
                }
                data[0] = (byte)ByteType.StartByte;
                data[13] = GetChecksum(data);
                data[14] = (byte)ByteType.StopByte;
                return data;
            }
        }

        //copy 12 byte or 15 byte array to Data
        private byte[] CopyDataToData(byte[] dataIn)
        { 
            var data = new byte[15];
            if (dataIn.Length == 15)
            {
                dataIn.CopyTo(data, 0);
            }
            else if (dataIn.Length == 12)
            {
                dataIn.CopyTo(data, 1);
                data[0] = (byte)ByteType.StartByte;
                data[13] = GetChecksum(data);
                data[14] = (byte)ByteType.StopByte;
            }
            return data;
        }

        //calculate checksum of 12 bytes
        private byte GetChecksum(byte[] data)
        {
            byte checksum = 0;
            for (int i = 1; i < 13; i++)
                checksum += data[i];
            return checksum;
        }
    }
}
