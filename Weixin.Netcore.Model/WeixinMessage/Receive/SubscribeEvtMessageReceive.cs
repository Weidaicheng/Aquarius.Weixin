using System.Collections.Generic;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 关注事件消息接收
    /// </summary>
    public class SubscribeEvtMessageReceive : IMessageReceive<SubscribeEvtMessage>, IMessageReceive
    {
        public SubscribeEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new SubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };
        }

        public SubscribeEvtMessage GetEntity(Dictionary<string, string> dic)
        {
            return new SubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };
        }
    }
}
