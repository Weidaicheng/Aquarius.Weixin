using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 文本消息接收
    /// </summary>
    public class TextMessageReceive : IMessageReceive<TextMessage>
    {
        public TextMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            var message = new TextMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                Content = dic["Content"]
            };

            return message;
        }
    }
}
