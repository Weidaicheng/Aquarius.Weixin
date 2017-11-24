namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public abstract class EventMessage : MessageBase, IMessage
    {
        public EventMessage()
        {
            MsgType = "event";
        }

        public string Event { get; internal set; }
    }
}
