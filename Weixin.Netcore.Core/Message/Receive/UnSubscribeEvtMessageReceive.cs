using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message.Receive
{
    /// <summary>
    /// 取消关注事件消息接收
    /// </summary>
    public class UnSubscribeEvtMessageReceive : IMessageReceive<UnSubscribeEvtMessage>
    {
        public UnSubscribeEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new UnSubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };
        }

        public UnSubscribeEvtMessage GetEntity(Dictionary<string, string> dic)
        {
            return new UnSubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };
        }
    }
}
