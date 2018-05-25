using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 图文消息回复
    /// </summary>
    public class NewsMessageReply : IMessageReply<NewsMessage>
    {
        public string CreateXml(NewsMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<ArticleCount>{entity.NewsDetails.Count}</ArticleCount>");
            sb.Append($"<Articles>");
            foreach (var item in entity.NewsDetails)
            {
                sb.Append($"<item>");
                sb.Append($"<Title><![CDATA[{item.Title}]]></Title> ");
                sb.Append($"<Description><![CDATA[{item.Description}]]></Description>");
                sb.Append($"<PicUrl><![CDATA[{item.PicUrl}]]></PicUrl>");
                sb.Append($"<Url><![CDATA[{item.Url}]]></Url>");
                sb.Append($"</item>");
            }
            sb.Append($"</Articles>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
