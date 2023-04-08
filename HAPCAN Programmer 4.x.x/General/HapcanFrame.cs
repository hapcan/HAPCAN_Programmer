using Hapcan.Messages;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Hapcan.General;

public class HapcanFrame
{

    //CONSTRUCTORS
    /// <summary>
    /// The HAPCAN frame constructor
    /// </summary>
    /// <param name="data">Frame data as array byte[12], byte[10] or byte[2].</param>
    /// <param name="frameSource">FrameSource.Interface, FrameSource.Canbus or FrameSource.PC</param>
    public HapcanFrame(byte[] data, FrameSource frameSource)
    {
        if (data.Length != 12 && data.Length != 10 && data.Length != 2)
            throw new ArgumentException("An invalid frame data length was supplied.");
        else
        {
            Time = DateTime.Now;
            Data = data;
            Source = frameSource;
            FrameSourceText = GetSourceText();
            FrameDataText = GetDataStringWithStartStopChecksum();
            Description = new HapcanMessages(this).GetDescription();
        }
    }

    /// <summary>
    /// The HAPCAN frame constructor
    /// </summary>
    /// <param name="data">Frame data as 12 byte hex string eg. "AB CD EF...". Throws exception if data is incorrect.</param>
    /// <param name="frameSource">FrameSource.Interface, FrameSource.Canbus or FrameSource.PC</param>
    public HapcanFrame(string dataText, FrameSource frameSource)
    {
        if (!IsDataTextCorrect(dataText))
            throw new ArgumentException("An invalid frame data string was supplied.");
        else
        {
            Time = DateTime.Now;
            Data = ConvertStringToData(dataText);
            Source = frameSource;
            FrameSourceText = GetSourceText();
            FrameDataText = GetDataStringWithStartStopChecksum();
            Description = new HapcanMessages(this).GetDescription();
        }
    }

    //PROPERTIES
    [Browsable(false)]
    public byte[] Data { get; private set; }
    [Browsable(false)]
    public FrameSource Source { get; }
    public DateTime Time { get; }
    public string FrameSourceText { get; }
    public string FrameDataText { get; }
    public string Description { get; set; }
    public enum ByteType : byte { StartByte = 0xAA, StopByte = 0xA5 }
    public enum FrameSource { Interface, Canbus, PcToInterface, PcToCanbus }


    //METHODS
    //
    private string GetSourceText()
    {
        string result = Source switch
        {
            FrameSource.Interface => "Interface",
            FrameSource.Canbus => "Canbus",
            FrameSource.PcToInterface => "PC to interface",
            FrameSource.PcToCanbus => "PC to canbus",
            _ => "unknown"
        };
        return result;
    }
    //get data string as hexadecimal numbers
    public string GetDataString()
    {
        string str = "";
        for (int i = 0; i < Data.Length; i++)
        {
            str += string.Format("{0:X2}{1}", Data[i], (i == 3) ? "  " : i == Data.Length - 1 ? "" : " ");
        }
        return str;
    }
    //get data string with start, checksum and top bytes
    public string GetDataStringWithStartStopChecksum()
    {
        var rxtx = Source == FrameSource.PcToCanbus || Source == FrameSource.PcToInterface ? "Tx ->" : "Rx <-";
        var start = (byte)ByteType.StartByte;
        var data = GetDataString();
        var checksum = GetFrameChecksum();
        var stop = (byte)ByteType.StopByte;

        return string.Format("{0} {1:X2}  {2}  {3:X2} {4:X2}", rxtx, start, data, checksum, stop);
    }
    /// <summary>
    /// Get first two bytes which defines frame type
    /// </summary>
    /// <returns>Frame type as integer 0x000 - 0xFFF</returns>
    public int GetFrameType()
    {
        return (Data[0] * 256 + Data[1]) / 16;
    }
    /// <summary>
    /// Check if frame is sent as response
    /// </summary>
    /// <returns>True is it is a response, otherwise false</returns>
    public bool IsResponse()
    {
        if ((Data[1] & 0x01) == 0x01)
            return true;
        else
            return false;
    }
    /// <summary>
    /// Calculate frame checksum
    /// </summary>
    /// <returns>Checksum value as byte</returns>
    public byte GetFrameChecksum()
    {
        byte checksum = 0;
        for (int i = 0; i < Data.Length; i++)
            checksum += Data[i];
        return checksum;
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
    public static bool IsDataTextCorrect(string msg)
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
    private byte[] ConvertStringToData(string dataText)
    {
        if (!IsDataTextCorrect(dataText))
            return null;
        else
        {
            byte[] data = new byte[12];
            dataText = dataText.Replace(" ", "");
            for (int i = 0; i < 24; i += 2)
            {
                data[i / 2] = Convert.ToByte(dataText.Substring(i, 2), 16);
            }
            return data;
        }
    }
}