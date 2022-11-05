using Hapcan.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.General;

internal class HapcanFirmwareFile
{
    /// <summary>
    /// Initializes a new instance of the HapcanFirmwareFile class for the specified file name.
    /// </summary>
    public HapcanFirmwareFile()
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
        var fileBuffer = new byte[0x10000];
        for (int i = 0; i < 0x10000; i++)
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
            if (fileBuffer[0x1010] == 0xFF && fileBuffer[0x1011] == 0xFF &&
                fileBuffer[0x1012] == 0xFF && fileBuffer[0x1013] == 0xFF &&
                fileBuffer[0x1014] == 0xFF && fileBuffer[0x1015] == 0xFF &&
                fileBuffer[0x1016] == 0xFF && fileBuffer[0x1017] == 0xFF)
            {
                throw new ArgumentException("The input file is not a HAPCAN firmware file.");
            }

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
        //create fake frame
        var frame = new HapcanFrame(new byte[]{0xFF,0xFF,0xFF,0xFF,fileBuffer[0x1010], fileBuffer[0x1011], fileBuffer[0x1012], fileBuffer[0x1013],
                                           fileBuffer[0x1014], fileBuffer[0x1015], fileBuffer[0x1016], fileBuffer[0x1017] }, HapcanFrame.FrameSource.PcToCanbus);
        //to get version from Msg105 message
        var version = new Msg106_FirmwareTypeResponse(frame).GetFullFirmwareVersion();
        var revision = " (revision: " + (fileBuffer[0x1016] * 256 + fileBuffer[0x1017]) + ")";
        return version + revision;
    }
}
