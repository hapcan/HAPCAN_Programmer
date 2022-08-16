using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.General;

internal class HapcanConfigFile
{
    public async Task<(byte[] Eeprom, byte[] Flash)> OpenMemoryConfigFromFile(string path)
    {
        //create buffer of hex and fill it
        var fileBuffer = new byte[0x10000];
        for (int i = 0; i < fileBuffer.Length; i++)
            fileBuffer[i] = 0xFF;

        try
        {
            string hStartCode;
            byte hByteCount;
            int hAddress;
            int addrOffset = 0;
            byte hRecordType;
            byte hLineChecksum;
            byte calculatedCheckSum;
            byte hByteValue;
            bool eepromPresent = false;
            bool flashPresent = false;

            string line;

            //read file to buffer line by line
            using var reader = new StreamReader(path);

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("<"))           //comments
                    continue;
                if (line == ":0200000400F00A")      //eeprom
                {
                    eepromPresent = true;
                    addrOffset = 0;
                    continue;
                }
                if (line == ":020000040000FA")      //flash
                {
                    flashPresent = true;
                    addrOffset = 0;
                    continue;
                }

                hStartCode = line.Substring(0, 1);
                hByteCount = Byte.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                hAddress = Int32.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.HexNumber) + addrOffset;
                hRecordType = Byte.Parse(line.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);
                hLineChecksum = Byte.Parse(line.Substring(line.Length - 2, 2), System.Globalization.NumberStyles.HexNumber);
                calculatedCheckSum = (byte)(hByteCount + (hAddress >> 8) + hAddress + hRecordType);

                //read data bytes of line
                if (hStartCode == ":" && hByteCount <= 0x10 && hAddress < 0x10000 && hRecordType == 0x00)
                {
                    for (int i = 0; i < hByteCount; i++)
                    {
                        hByteValue = Byte.Parse(line.Substring(9 + 2 * i, 2), System.Globalization.NumberStyles.HexNumber);

                        //update register
                        fileBuffer[hAddress + i] = hByteValue;
                        //calculate line checksum
                        calculatedCheckSum += hByteValue;
                    }
                    //check line checksum
                    calculatedCheckSum = (byte)((~calculatedCheckSum) + 1);
                    if (calculatedCheckSum != hLineChecksum)
                    {
                        throw new InvalidDataException("Wrong checksum at address 0x" + hAddress.ToString("X6"));
                    }
                }
            }

            //at least eeprom must be in the file
            if (eepromPresent == false)
            {
                throw new InvalidDataException("The input file is not a HAPCAN config file.");
            }
            //prepare eeprom buffer
            byte[] eepromBuf = new byte[0x400];
            for (int i = 0; i < eepromBuf.Length; i++)
                eepromBuf[i] = fileBuffer[i];
            //prepare data flash buffer
            byte[] flashBuf = new byte[0x8000];
            for (int i = 0; i < flashBuf.Length; i++)
                flashBuf[i] = fileBuffer[i + 0x8000];

            return (eepromBuf, flashBuf);
        }
        catch (InvalidDataException)
        {
            throw;              //rethrow if wrong checksum
        }
        catch (Exception)
        {
            throw new InvalidDataException("The input file is not a HAPCAN config file.");
        }
    }

    public async void SaveMemoryConfigToFile(int serialNumber, byte[] Eeprom, byte[] Flash, string fileName)
    {
        //file header
        var hacFile = "<--- HAPCAN - Home Automation Project ---->" + System.Environment.NewLine;
        hacFile +=    "<---------- website: hapcan.com ---------->" + System.Environment.NewLine;
        var sn = serialNumber.ToString("X8");
        hacFile +=    "<    Module s/n "+ sn +"h memory config   >" + System.Environment.NewLine;

        //file data
        hacFile += ":020000040000FA" + System.Environment.NewLine;      //based addres 0x000000
        hacFile += GetMemoryDataToString(Flash, 0x8000);
        hacFile += ":0200000400F00A" + System.Environment.NewLine;      //based addres 0xF00000
        hacFile += GetMemoryDataToString(Eeprom, 0);
        hacFile += ":00000001FF";                                       //end of file

        //save file
        using var writer = new StreamWriter(fileName);
        await writer.WriteAsync(hacFile);
    }

    private string GetMemoryDataToString(byte[] mem, int offset)
    {
        string dataFile = "", dataFileLine;
        for (int i = 0; i < mem.Length; i += 16)
        {
            //line header
            var adr = i + offset;
            dataFileLine = ":10" + adr.ToString("X4") + "00";
            byte LineCheckSum = (byte)(0x10 + (adr >> 8) + adr);

            //line bytes
            for (int j = 0; j < 16; j++)
            {
                dataFileLine += mem[i + j].ToString("X2");
                LineCheckSum += mem[i + j];
            }

            //line checksum
            LineCheckSum = (byte)((~LineCheckSum) + 1);
            dataFileLine += LineCheckSum.ToString("X2") + System.Environment.NewLine;

            //add line to file only if data not equal 0xFF
            if (dataFileLine.Substring(9, 32) != "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
                dataFile += dataFileLine;
        }
        return dataFile;
    }
}
