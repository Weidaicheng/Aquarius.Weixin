using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 图片消息回复
    /// </summary>
    public class ImageMessageReply : IMessageReply<ImageMessage>
    {
        public string CreateXml(ImageMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<Image>");
            sb.Append($"<MediaId><![CDATA[{entity.MediaId}]]></MediaId>");
            sb.Append($"</Image>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
