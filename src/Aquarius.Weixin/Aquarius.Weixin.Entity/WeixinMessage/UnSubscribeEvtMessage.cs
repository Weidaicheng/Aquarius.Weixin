namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 取消关注事件消息
    /// </summary>
    public class UnSubscribeEvtMessage : EventMessage
    {
        public UnSubscribeEvtMessage()
        {
            Event = "unsubscribe";
        }

        public UnSubscribeEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            Event = "unsubscribe";
        }
    }
}
