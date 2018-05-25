namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 自定义菜单点击事件消息
    /// </summary>
    public class ClickEvtMessage : EventMessage
    {
        public ClickEvtMessage()
        {
            Event = "CLICK";
        }

        public ClickEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            Event = "CLICK";
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
    }
}
