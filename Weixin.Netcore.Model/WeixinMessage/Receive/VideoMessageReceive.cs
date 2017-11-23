using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 视频消息接收
    /// </summary>
    public class VideoMessageReceive : IMessageReceive<VideoMessage>
    {
        public VideoMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            var message = new VideoMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                ThumbMediaId = dic["ThumbMediaId"]
            };


            return message;
        }
    }
}
