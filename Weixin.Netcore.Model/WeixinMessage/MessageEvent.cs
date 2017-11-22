namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 事件消息
    /// </summary>
    public class MessageEvent : MessageBase
    {
        public MessageEvent()
        {
            MsgType = "event";
        }
    }
}
