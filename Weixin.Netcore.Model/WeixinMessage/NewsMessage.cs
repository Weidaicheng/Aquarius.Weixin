using System.Collections.Generic;
using System.Text;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage : MessageBase, IMessageReply
    {
        public NewsMessage()
        {
            MsgType = "news";
        }

        /// <summary>
        /// 图文详情
        /// </summary>
        public List<NewsDetail> NewsDetails { get; set; }

        public string CreateXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            sb.Append($"<ArticleCount>{NewsDetails.Count}</ArticleCount>");
            sb.Append($"<Articles>");
            foreach (var item in NewsDetails)
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

    /// <summary>
    /// 图文详情
    /// </summary>
    public class NewsDetail
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }
    }
}
