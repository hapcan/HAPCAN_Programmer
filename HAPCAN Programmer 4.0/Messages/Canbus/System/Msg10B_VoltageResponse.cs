using Hapcan.General;

namespace Hapcan.Messages
{
    class Msg10B_VoltageResponse : MsgBase
    {
        private HapcanFrame _frame;

        public Msg10B_VoltageResponse(HapcanFrame frame) : base(frame)
        {
            _frame = frame;
        }

        public float ModuleVoltage
        {
            get { return (float)((_frame.Data[4] * 256 + _frame.Data[5]) * 30.5 / 65472); }
        }
        public float ProcessorVoltage
        {
            get { return (float)((_frame.Data[6] * 256 + _frame.Data[7]) * 5 / 65472); }
        }

        public string GetDescription()
        {
            return string.Format("SYSTEM - Supply voltage, module: {0:N2}V, processor: {1:N2}V", ModuleVoltage, ProcessorVoltage);
        }
    }
}
