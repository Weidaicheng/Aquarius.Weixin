using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    public interface ITextMessageHandler : IMessageHandler
    {
        /// <summary>
		/// 文本消息处理
		/// </summary>
		/// <param name="message"></param>
		string TextMessageHandler(TextMessage message);
    }
}
