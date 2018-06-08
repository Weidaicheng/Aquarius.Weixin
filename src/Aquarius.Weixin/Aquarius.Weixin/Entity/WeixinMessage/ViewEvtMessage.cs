namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 自定义View菜单事件
    /// </summary>
    public class ViewEvtMessage : EventMessage
    {
        public ViewEvtMessage()
        {
            Event = "VIEW";
        }

        public ViewEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            Event = "VIEW";
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }
    }
}
