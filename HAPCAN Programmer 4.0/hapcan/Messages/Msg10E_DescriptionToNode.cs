﻿namespace Hapcan.Programmer.Hapcan.Messages
{
    class Msg10E_DescriptionToNode
    {
        private HapcanFrame _frame;

        public Msg10E_DescriptionToNode(HapcanFrame frame)
        {
            _frame = frame;
        }
        public Msg10E_DescriptionToNode(byte nodeTx, byte groupTx, byte nodeRx, byte groupRx)
        {
            _frame = new HapcanFrame(new byte[] { 0x10, 0xE0, nodeTx, groupTx, 0xFF, 0xFF, nodeRx, groupRx, 0xFF, 0xFF, 0xFF, 0xFF }, false);
        }

        public HapcanFrame GetFrame()
        {
            return _frame;
        }
        public string GetDescription()
        {
            if (!_frame.IsResponse())
            {
                return string.Format("SYSTEM - Description request to node ({0},{1})", _frame.Data[7], _frame.Data[8]);
            }
            else
            {
                return new Msg10D_DescriptionResponse(_frame).GetDescription();
            }
        }
    }
}
