using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : MessageNormal, IMessageReceive
    {
        public VoiceMessage()
        {
            MsgType = "voice";
        }

        /// <summary>
		/// 素材Id
		/// </summary>
		public string MediaId { get; set; }

        /// <summary>
        /// 语音格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别
        /// </summary>
        public string Recognition { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            MediaId = dic["MediaId"];
            Format = dic["Format"];
            Recognition = dic["Recognition"];
        }
    }
}
