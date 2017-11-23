using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 图片消息接收
    /// </summary>
    public class ImageMessageReceive : IMessageReceive<ImageMessage>
    {
        public ImageMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ImageMessage message = new ImageMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"]
            };

            return message;
        }
    }
}
