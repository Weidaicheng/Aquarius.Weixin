using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 音乐消息回复
    /// </summary>
    public class MusicMessageReply : IMessageReply<MusicMessage>
    {
        public string CreateXml(MusicMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<music>");
            sb.Append($"<Title><![CDATA[{entity.Title}]]></Title>");
            sb.Append($"<Description><![CDATA[{entity.Description}]]></Description>");
            sb.Append($"<MusicUrl><![CDATA[{entity.MusicURL}]]></MusicUrl>");
            sb.Append($"<HQMusicUrl><![CDATA[{entity.HQMusicUrl}]]></HQMusicUrl>");
            sb.Append($"<ThumbMediaId><![CDATA[{entity.ThumbMediaId}]]></ThumbMediaId>");
            sb.Append($"</music>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
