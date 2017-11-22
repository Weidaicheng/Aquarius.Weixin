using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 自定义菜单点击事件消息
    /// </summary>
    public class ClickEvtMessage : EventMessage, IMessageReceive
    {
        public ClickEvtMessage()
        {
            Event = "CLICK";
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            EventKey = dic["EventKey"];
        }
    }
}
