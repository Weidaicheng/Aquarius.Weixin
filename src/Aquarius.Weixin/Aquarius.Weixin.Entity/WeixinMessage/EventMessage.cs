namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public abstract class EventMessage : MessageBase
    {
        public EventMessage()
        {
            MsgType = "event";
        }

        public EventMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "event";
        }

        public string Event { get; set; }
    }
}
