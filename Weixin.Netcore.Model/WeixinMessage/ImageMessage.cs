using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : MessageNormal, IMessageReceive
    {
        public ImageMessage()
        {
            MsgType = "image";
        }

        /// <summary>
		/// 图片消息内容
		/// </summary>
		public string MediaId { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            MediaId = dic["MediaId"];
        }
    }
}
