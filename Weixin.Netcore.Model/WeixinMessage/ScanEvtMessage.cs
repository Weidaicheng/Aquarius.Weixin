using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 扫码事件消息
    /// </summary>
    public class ScanEvtMessage : EventMessage, IMessageReceive
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码Ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            Event = dic["Event"];
            EventKey = dic["EventKey"];
            Ticket = dic["Ticket"];
        }
    }
}
