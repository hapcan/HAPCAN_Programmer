﻿using Hapcan.General;

namespace Hapcan.Messages;

class IntMsg1F1_FirmwareError
{
    private HapcanFrame _frame;

    public IntMsg1F1_FirmwareError(HapcanFrame frame)
    {
        _frame = frame;
        FirmwareFlags = frame.Data[2];
        FirmwareChecksum2 = frame.Data[3];
        FirmwareChecksum1 = frame.Data[4];
        FirmwareChecksum0 = frame.Data[5];
        BootloaderMajorVersion = frame.Data[8];
        BootloaderMinorVersion = frame.Data[9];
    }
    public IntMsg1F1_FirmwareError(HapcanNode node)
    {
        FirmwareFlags = node.FirmwareError;
        FirmwareChecksum2 = node.ApplicationType;               //checksum is written into apptype, appver, firmver bytes when firmware error
        FirmwareChecksum1 = node.ApplicationVersion;
        FirmwareChecksum0 = node.FirmwareVersion;
        BootloaderMajorVersion = node.BootloaderMajorVersion;
        BootloaderMinorVersion = node.BootloaderMinorVersion;
    }

    public byte FirmwareFlags { get; }
    public byte FirmwareChecksum2 { get; }
    public byte FirmwareChecksum1 { get; }
    public byte FirmwareChecksum0 { get; }
    public byte BootloaderMajorVersion { get; }
    public byte BootloaderMinorVersion { get; }

    public string GetDescription()
    {
        var error = GetError();
        return string.Format("INTERFACE - SYSTEM - Firmware error: {0}", error);
    }
    public string GetFirmwareError()
    {
        var error = GetError();
        return string.Format("Firmware error: {0}", error);
    }
    private string GetError()
    {
        var chsum = FirmwareChecksum2 * 256 * 256 + FirmwareChecksum1 * 256 + FirmwareChecksum0;
        var desc = "unknown";

        if ((FirmwareFlags & 0x02) == 0x02)                //bit <1> no firmware - legacy
        {
            desc = "no firmware";
        }
        else if ((FirmwareFlags & 0x04) == 0x04)           //bit <2> wrong checksum
        {
            if (chsum == 0x6F7020)
                desc = "no firmware";
            else
                desc = string.Format("incorrect firmware checksum, expected checksum: 0x{0:X6}", chsum);
        }
        else if ((FirmwareFlags & 0x08) == 0x08)           //bit <3> wrong hardware
        {
            desc = "hardware mismatch - firmware for other hardware";
        }
        return desc;

    }
}
