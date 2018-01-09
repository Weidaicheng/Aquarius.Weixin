namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 扫码事件消息
    /// </summary>
    public class ScanEvtMessage : EventMessage
    {
        public ScanEvtMessage()
        {
            Event = "Scan";
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码Ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }
}
