using Hapcan.General;

namespace Hapcan.Messages
{
    class Msg1F1_FirmwareError : MsgBase
    {
        public Msg1F1_FirmwareError(HapcanFrame frame) : base(frame)
        {
            FirmwareFlags = frame.Data[4];
            FirmwareChecksum2 = frame.Data[5];
            FirmwareChecksum1 = frame.Data[6];
            FirmwareChecksum0 = frame.Data[7];
            BootloaderMajorVersion = frame.Data[10];
            BootloaderMinorVersion = frame.Data[11];
        }
        public Msg1F1_FirmwareError(HapcanNode node) : base(node)
        {
            FirmwareFlags = node.FirmwareFlags;
            FirmwareChecksum2 = node.FirmwareChecksum2;
            FirmwareChecksum1 = node.FirmwareChecksum1;
            FirmwareChecksum0 = node.FirmwareChecksum0;
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
            var error = GetFirmwareError();

            return string.Format("SYSTEM - Firmware error: {0}", error);
        }
        private string GetFirmwareError()
        {
            int chsum = FirmwareChecksum2 * 256 * 256 + FirmwareChecksum1 * 256 + FirmwareChecksum0;
            var desc = "unknown";

            if ((FirmwareFlags & 0x04) == 0x04)                //bit <2> wrong checksum
            {
                if (chsum == 0x6F7020)
                    desc = "no firmware";
                else
                    desc = string.Format("incorrect firmware checksum, expected checksum: 0x{0:X6}", chsum);
            }
            if ((FirmwareFlags & 0x08) == 0x08)                //bit <3> wrong hardware
                desc = "hardware mismatch - firmware for other hardware";
            return desc;
        }

        public string GetFullFirmwareVersion()
        {
            var error = GetFirmwareError();

            return string.Format("Firmware error: {0}", error);
        }
    }
}
