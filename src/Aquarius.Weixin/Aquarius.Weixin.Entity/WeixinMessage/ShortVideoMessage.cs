namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 小视频消息
    /// </summary>
    public class ShortVideoMessage : NormalMessage
    {
        public ShortVideoMessage()
        {
            MsgType = "shortvideo";
        }

        public ShortVideoMessage(MessageBase receivedMsg) : base(receivedMsg)
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
