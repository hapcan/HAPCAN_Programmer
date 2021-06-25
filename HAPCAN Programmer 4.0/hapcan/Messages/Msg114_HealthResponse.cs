namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg114_HealthResponse
    {
        private HapcanFrame _frame;

        public Msg114_HealthResponse(HapcanFrame frame)
        {
            _frame = frame;
        }

        public string GetDescription()
        {
            if (_frame.Data[5] == 0x01)
            {
                return string.Format("SYSTEM - Health current: RXbuff={0}, TXbuff={1}, RXmax={2}, TXmax={3}, CANinit={4}, RXerr={5}, TXerr={6}",
                    _frame.Data[6], _frame.Data[7], _frame.Data[8], _frame.Data[9], _frame.Data[10], _frame.Data[11], _frame.Data[12]);
            }
            else if (_frame.Data[5] == 0x02)
            {
                return string.Format("SYSTEM - Health max ever: RXmaxever={0}, TXmaxever={1}, CANinitever={2}, RXerrever={3}, TXerrever={4}",
                    _frame.Data[8], _frame.Data[9], _frame.Data[10], _frame.Data[11], _frame.Data[12]);
            }
            return "unknown";
        }
    }
}
