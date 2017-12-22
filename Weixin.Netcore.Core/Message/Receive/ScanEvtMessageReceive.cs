using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message.Receive
{
    /// <summary>
    /// 扫码事件接收
    /// </summary>
    public class ScanEvtMessageReceive : IMessageReceive<ScanEvtMessage>
    {
        public ScanEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new ScanEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                Event = dic["Event"],
                EventKey = dic["EventKey"],
                Ticket = dic["Ticket"]
            };
        }

        public ScanEvtMessage GetEntity(Dictionary<string, string> dic)
        {
            return new ScanEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                Event = dic["Event"],
                EventKey = dic["EventKey"],
                Ticket = dic["Ticket"]
            };
        }
    }
}
