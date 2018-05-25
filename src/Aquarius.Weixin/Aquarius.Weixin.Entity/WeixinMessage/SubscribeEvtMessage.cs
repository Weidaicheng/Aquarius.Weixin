namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 关注事件消息
    /// </summary>
    public class SubscribeEvtMessage : EventMessage
    {
        public SubscribeEvtMessage()
        {
            Event = "subscribe";
        }

        public SubscribeEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            Event = "subscribe";
        }
    }
}
