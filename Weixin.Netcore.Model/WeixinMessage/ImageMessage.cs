namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : NormalMessage
    {
        public ImageMessage()
        {
            MsgType = "image";
        }

        /// <summary>
		/// 图片消息内容
		/// </summary>
		public string MediaId { get; set; }
    }
}
