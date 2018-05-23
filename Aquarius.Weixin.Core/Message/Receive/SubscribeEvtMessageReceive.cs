using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 关注事件消息接收
    /// </summary>
    public class SubscribeEvtMessageReceive : IMessageReceive//<SubscribeEvtMessage>
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new SubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
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
