namespace Weixin.Netcore.Model.WeixinMessage
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
    }
}
