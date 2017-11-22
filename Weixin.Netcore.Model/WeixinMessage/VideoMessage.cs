using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : MessageNormal, IMessageReceive
    {
        public VideoMessage()
        {
            MsgType = "video";
        }

        /// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 素材Id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图素材Id
        /// </summary>
        public string ThumbMediaId { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            MediaId = dic["MediaId"];
            ThumbMediaId = dic["ThumbMediaId"];
        }
    }
}
