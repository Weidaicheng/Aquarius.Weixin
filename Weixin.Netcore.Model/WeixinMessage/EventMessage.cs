namespace Weixin.Netcore.Model.WeixinMessage
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

        public string Event { get; set; }
    }
}
