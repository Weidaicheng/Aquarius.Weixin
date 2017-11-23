using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 链接消息处理
    /// </summary>
    public interface ILinkMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 链接消息处理
        /// </summary>
        /// <param name="message"></param>
        string LinkMessageHandler(LinkMessage message);
    }
}
