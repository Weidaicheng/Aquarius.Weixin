using System.Collections.Generic;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 自定义菜单消息点击事件接收
    /// </summary>
    public class ClickEvtMessageReceive : IMessageReceive<ClickEvtMessage>, IMessageReceive
    {
        public ClickEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new ClickEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                EventKey = dic["EventKey"]
            };
        }

        public ClickEvtMessage GetEntity(Dictionary<string, string> dic)
        {
            return new ClickEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                EventKey = dic["EventKey"]
            };
        }
    }
}
