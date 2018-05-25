using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 链接消息接收
    /// </summary>
    public class LinkMessageReceive : IMessageReceive//<LinkMessage>
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new LinkMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                Title = dic["Title"],
                Description = dic["Description"],
                Url = dic["Url"]
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
        {
            return new LinkMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                Title = dic["Title"],
                Description = dic["Description"],
                Url = dic["Url"]
            };
        }
    }
}
