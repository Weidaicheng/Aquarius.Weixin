using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 视频消息回复
    /// </summary>
    public class VideoMessageReply : IMessageReply<VideoMessage>
    {
        public string CreateXml(VideoMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<Video>");
            sb.Append($"<MediaId><![CDATA[{entity.MediaId}]]></MediaId>");
            sb.Append($"<Title><![CDATA[{entity.Title}]]></Title> ");
            sb.Append($"<Description><![CDATA[{entity.Description}]]></Description>");
            sb.Append($"</Video>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
