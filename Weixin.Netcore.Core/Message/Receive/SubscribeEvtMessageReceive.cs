using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message.Receive
{
    /// <summary>
    /// 关注事件消息接收
    /// </summary>
    public class SubscribeEvtMessageReceive : IMessageReceive<SubscribeEvtMessage>
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
