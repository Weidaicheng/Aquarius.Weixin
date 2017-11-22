using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    public class LinkMessage : MessageNormal, IMessageReceive
    {
        public LinkMessage()
        {
            MsgType = "link";
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            Title = dic["Title"];
            Description = dic["Description"];
            Url = dic["Url"];
        }
    }
}
