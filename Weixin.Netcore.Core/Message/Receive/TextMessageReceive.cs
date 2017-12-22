using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message.Receive
{
    /// <summary>
    /// 文本消息接收
    /// </summary>
    public class TextMessageReceive : IMessageReceive//<TextMessage>
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new TextMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                Content = dic["Content"]
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
        {
            return new TextMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                Content = dic["Content"]
            };
        }
    }
}
