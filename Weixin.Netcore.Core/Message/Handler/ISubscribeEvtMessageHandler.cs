using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 订阅事件处理
    /// </summary>
    public interface ISubscribeEvtMessageHandler : IMessageHandler
    {
        /// <summary>
		/// 订阅事件处理
		/// </summary>
		/// <param name="message"></param>
		string SubscribeEventHandler(SubscribeEvtMessage message);
    }
}
