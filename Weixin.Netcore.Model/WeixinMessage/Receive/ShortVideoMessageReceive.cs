using System.Collections.Generic;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 小视频消息接收
    /// </summary>
    public class ShortVideoMessageReceive : IMessageReceive<ShortVideoMessage>, IMessageReceive
    {
        public ShortVideoMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new ShortVideoMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                ThumbMediaId = dic["ThumbMediaId"]
            };
        }

        public ShortVideoMessage GetEntity(Dictionary<string, string> dic)
        {
            return new ShortVideoMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                ThumbMediaId = dic["ThumbMediaId"]
            };
        }
    }
}
