using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 扫码关注事件消息接收
    /// </summary>
    public class ScanSubscribeEvtMessageReceive : IMessageReceive
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new ScanSubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                EventKey = dic["EventKey"],
                Ticket = dic["Ticket"]
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
        {
            return new ScanSubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                EventKey = dic["EventKey"],
                Ticket = dic["Ticket"]
            };
        }
    }
}
