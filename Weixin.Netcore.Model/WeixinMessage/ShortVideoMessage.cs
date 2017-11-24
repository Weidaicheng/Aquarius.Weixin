namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 小视频消息
    /// </summary>
    public class ShortVideoMessage : NormalMessage, IMessage
    {
        public ShortVideoMessage()
        {
            MsgType = "shortvideo";
        }

        /// <summary>
        /// 视频消息素材Id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图素材Id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
