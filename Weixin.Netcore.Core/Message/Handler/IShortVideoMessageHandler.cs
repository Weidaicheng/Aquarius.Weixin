using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 小视频消息处理
    /// </summary>
    public interface IShortVideoMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 小视频消息处理
        /// </summary>
        /// <param name="message"></param>
        string ShortVideoMessageHandler(ShortVideoMessage message);
    }
}
