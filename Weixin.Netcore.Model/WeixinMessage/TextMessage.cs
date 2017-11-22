using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : MessageNormal, IMessageReceive
    {
        public TextMessage()
        {
            MsgType = "text";
        }

        /// <summary>
		/// 文本消息内容
		/// </summary>
		public string Content { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            Content = dic["Content"];
        }
    }
}
