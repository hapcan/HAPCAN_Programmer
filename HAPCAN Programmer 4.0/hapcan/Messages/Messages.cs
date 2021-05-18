using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hapcan.Programmer.Hapcan;

namespace Hapcan.Programmer.Hapcan.Messages
{
    public class Messages
    {
        private readonly HapcanFrame _frame;
        public Messages(HapcanFrame frame)
        {
            _frame = frame;
        }
        public string GetDescription()
        {
            string desc;
            var frameType = _frame.GetFrameType();
            switch (frameType)
            {
                //programing mode
                case 0x010: desc = new Msg010_ExitAllFromProgramming(_frame).GetDescription(); break;
                case 0x020: desc = new Msg020_ExitNodeFromProgramming(_frame).GetDescription(); break;
                case 0x030: desc = new Msg030_ProgrammingAddress(_frame).GetDescription(); break;
                case 0x040: desc = new Msg040_ProgrammingData(_frame).GetDescription(); break;
                //system
                case 0x100: desc = new Msg100_EnterProgramming(_frame).GetDescription(); break;
                case 0x101: desc = new Msg101_RebootGroup(_frame).GetDescription(); break;
                case 0x102: desc = new Msg102_RebootNode(_frame).GetDescription(); break;
                case 0x103: desc = new Msg103_HardwareTypeToGroup(_frame).GetDescription(); break;
                case 0x104: desc = new Msg104_HardwareTypeToNode(_frame).GetDescription(); break;
                case 0x105: desc = new Msg105_FirmwareTypeToGroup(_frame).GetDescription(); break;
                case 0x106: desc = new Msg106_FirmwareTypeToNode(_frame).GetDescription(); break;
                case 0x107: desc = new Msg107_SetDefaultIdToNode(_frame).GetDescription(); break;
                case 0x108: desc = new Msg108_StatusToGroup(_frame).GetDescription(); break;
                case 0x109: desc = new Msg109_StatusToNode(_frame).GetDescription(); break;
                case 0x10A: desc = new Msg10A_ControlToNode(_frame).GetDescription(); break;
                case 0x10B: desc = new Msg10B_VoltageToGroup(_frame).GetDescription(); break;
                case 0x10C: desc = new Msg10C_VoltageToNode(_frame).GetDescription(); break;
                case 0x10D: desc = new Msg10D_DescriptionToGroup(_frame).GetDescription(); break;
                case 0x10E: desc = new Msg10E_DescriptionToNode(_frame).GetDescription(); break;
                case 0x10F: desc = new Msg10F_ProcessorIdToGroup(_frame).GetDescription(); break;
                case 0x111: desc = new Msg111_ProcessorIdToNode(_frame).GetDescription(); break;
                case 0x112: desc = new Msg112_UptimeToGroup(_frame).GetDescription(); break;
                case 0x113: desc = new Msg113_UptimeToNode(_frame).GetDescription(); break;
                case 0x114: desc = new Msg114_HealthToGroup(_frame).GetDescription(); break;
                case 0x115: desc = new Msg115_HealthToNode(_frame).GetDescription(); break;
                //device
                case 0x300: desc = new Msg300_Rtc(_frame).GetDesription(); break;
                case 0x301: desc = new Msg301_Button(_frame).GetDesription(); break;
                case 0x302: desc = new Msg302_Relay(_frame).GetDesription(); break;
                case 0x304: desc = new Msg304_Temperature(_frame).GetDesription(); break;


                case 0x0F0: desc = new Msg0F0_ProgrammingError(_frame).GetDescription(); break;
                case 0x1F1: desc = new Msg1F1_FirmwareError(_frame).GetDescription(); break;
                default: desc = string.Format("Unknown frame type 0x{0:X}", frameType); break;
            }
            return string.Format("{0} - {1}", GetNodeId(), desc);
        }
        private string GetNodeId()
        {
            return string.Format("Node({0},{1})", _frame.Data[3], _frame.Data[4]);
        }
    }
}
