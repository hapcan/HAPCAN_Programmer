using Hapcan.General;

namespace Hapcan.Messages
{
    class IntMsg10C_VoltageResponse
    {
        private HapcanFrame _frame;

        public IntMsg10C_VoltageResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public float ModuleVoltage
        {
            get { return (float)((_frame.Data[2] * 256 + _frame.Data[3]) * 30.5 / 65472); }
        }
        public float ProcessorVoltage
        {
            get { return (float)((_frame.Data[4] * 256 + _frame.Data[5]) * 5 / 65472); }
        }

        public string GetDescription()
        {
            return string.Format("INTERFACE - SYSTEM - Supply voltage, module: {0:N2}V, processor: {1:N2}V", ModuleVoltage, ProcessorVoltage);
        }
    }
}
