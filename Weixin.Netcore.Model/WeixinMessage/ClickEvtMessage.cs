namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 自定义菜单点击事件消息
    /// </summary>
    public class ClickEvtMessage : EventMessage, IMessage
    {
        public ClickEvtMessage()
        {
            Event = "CLICK";
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; internal set; }
    }
}
