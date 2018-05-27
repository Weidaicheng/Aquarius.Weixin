namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 扫码订阅事件消息
    /// </summary>
    public class ScanSubscribeEvtMessage : ScanEvtMessage
    {
        public ScanSubscribeEvtMessage()
        {
            Event = "Subscribe";
        }

        public ScanSubscribeEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            Event = "Subscribe";
        }
    }
}
