using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 图片消息处理
    /// </summary>
    public interface IImageMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 图片消息处理
        /// </summary>
        /// <param name="message"></param>
        string ImageMessageHandler(ImageMessage message);
    }
}
