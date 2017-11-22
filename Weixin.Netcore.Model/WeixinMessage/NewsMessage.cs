using System.Collections.Generic;
using System.Text;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage : MessageBase
    {
        public NewsMessage()
        {
            MsgType = "news";
        }

        /// <summary>
        /// 图文详情
        /// </summary>
        public List<NewsDetail> NewsDetails { get; set; }
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
