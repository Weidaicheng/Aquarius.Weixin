using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 图片消息接收
    /// </summary>
    public class ImageMessageReceive : IMessageReceive//<ImageMessage>
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new ImageMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"]
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
        {
            return new ImageMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"]
            };
        }
    }
}
