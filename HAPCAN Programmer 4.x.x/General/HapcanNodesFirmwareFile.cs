using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.General;

internal class HapcanNodesFirmwareFile
{
    public HapcanNode.Proc Processor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the HapcanFirmwareFile class for the specified file name.
    /// </summary>
    public HapcanNodesFirmwareFile()
    {
    }

    /// <summary>
    /// Reads content of HAPCAN firmware file into byte[] buffer
    /// </summary>
    /// <param name="path">Firmware file path.</param>
    /// <returns>A task that represents the asynchronous read. The value of the TResult contains the byte[] buffer with firmware data</returns>
    /// <exception cref="System.ArgumentException">Thrown when the file is not a firmware file</exception>
    public async Task<byte[]> ReadFirmwareFileAsync(string path)
    {
        //create buffer of hex and fill it
        var fileBuffer = new byte[0x20000];
        for (int i = 0; i < 0x20000; i++)
            fileBuffer[i] = 0xFF;

        try
        {
            string hStartCode;
            int hByteCount;
            int hAddress;
            int hRecordType;
            byte hByteValue;
            int hAddressMax = 0;
            string line;

            //read file to buffer line by line
            using var reader = new StreamReader(path);

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("<"))
                    continue;

                hStartCode = line.Substring(0, 1);
                hByteCount = Int32.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                hAddress = Int32.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
                hRecordType = Int32.Parse(line.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);

                //read data bytes of line
                if (hStartCode == ":" && hByteCount <= 0x10 && hAddress < 0x10000 && hRecordType == 0x00)
                {
                    for (int i = 0; i < hByteCount; i++)
                    {
                        hByteValue = Byte.Parse(line.Substring(9 + 2 * i, 2), System.Globalization.NumberStyles.HexNumber);

                        //update register
                        fileBuffer[hAddress + i] = hByteValue;
                        //update max address
                        if (hAddressMax < hAddress + i)
                            hAddressMax = hAddress + i;
                    }
                }
            }
            if (fileBuffer[0x1010] != 0xFF && fileBuffer[0x1011] != 0xFF && fileBuffer[0x1012] == 3)        //UNIV3
            {
                Processor = HapcanNode.Proc.PIC18F26K80;
                //reduce buffer size
                Array.Resize(ref fileBuffer, 0x010000);
            }
            else if (fileBuffer[0x2010] != 0xFF && fileBuffer[0x2011] != 0xFF && fileBuffer[0x2012] == 4)   //UNIV4
            {
                Processor = HapcanNode.Proc.PIC18F27Q83;
            }
            else
                throw new ArgumentException("The input file is not a HAPCAN firmware file.");
            

            return fileBuffer;
        }
        catch (Exception)
        {
            throw new ArgumentException("The input file is not a HAPCAN firmware file.");
        }
    }

    /// <summary>
    /// Gets full firmware name with version and revision
    /// </summary>
    /// <param name="fileBuffer">The byte[] buffer with firmware data</param>
    /// <returns>String of full firmware name with version and revision</returns>
    public string GetTextedFileFirmwareVersionRevision(byte[] fileBuffer)
    {
        HapcanFrame frame;
        string version = String.Empty;
        int revision;
        if (Processor == HapcanNode.Proc.PIC18F26K80)
        {
            //create fake frame
            frame = new HapcanFrame(new byte[]{0xFF,0xFF,0xFF,0xFF,fileBuffer[0x1010], fileBuffer[0x1011], fileBuffer[0x1012], fileBuffer[0x1013],
                                           fileBuffer[0x1014], fileBuffer[0x1015], fileBuffer[0x1016], fileBuffer[0x1017] }, HapcanFrame.FrameSource.PcToCanbus);
            revision = fileBuffer[0x1016] * 256 + fileBuffer[0x1017];
            //to get version from Msg105 message
            version = new Msg106_FirmwareTypeResponse(frame).GetFullFirmwareVersion();
        }
        else if (Processor == HapcanNode.Proc.PIC18F27Q83)
        {
            //create fake frame
            frame = new HapcanFrame(new byte[]{0xFF,0xFF,0xFF,0xFF,fileBuffer[0x2010], fileBuffer[0x2011], fileBuffer[0x2012], fileBuffer[0x2013],
                                           fileBuffer[0x2014], fileBuffer[0x2015], fileBuffer[0x2016], fileBuffer[0x2017] }, HapcanFrame.FrameSource.PcToCanbus);
            revision = fileBuffer[0x2016] * 256 + fileBuffer[0x2017];
            //to get version from Msg105 message
            version = new Msg106_FirmwareTypeResponse(frame).GetFullFirmwareVersion();
        }
        else
            throw new ArgumentException("The input buffer is not a HAPCAN firmware file.");

        return $"{version} (revision: {revision})";

    }
}
