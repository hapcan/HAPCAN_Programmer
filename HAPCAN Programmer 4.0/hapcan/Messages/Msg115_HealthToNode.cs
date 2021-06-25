namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg115_HealthToNode
    {
        private HapcanFrame _frame;

        public Msg115_HealthToNode(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg115_HealthToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx, byte command)
        {
            _frame = new HapcanFrame(new byte[] { 0x11, 0x50, nodeTx, groupTx, command, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            var cmd = GetCommand();
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Health {0} request to node ({0},{1})", _frame.Data[7], _frame.Data[8]);
            }
            else
            {
                return new Msg114_HealthResponse(_frame).GetDescription();
            }
        }
        private string GetCommand()
        {
            var cmd = _frame.Data[5];
            switch (cmd)
            {
                case 1: return "status";
                case 2: return "clear";
                default: return "";
            }
        }
    }
}
