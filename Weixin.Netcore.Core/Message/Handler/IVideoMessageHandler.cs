using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 视频消息处理
    /// </summary>
    public interface IVideoMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 视频消息处理
        /// </summary>
        /// <param name="message"></param>
        string VideoMessageHandler(VideoMessage message);
    }
}
