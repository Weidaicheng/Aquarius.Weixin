using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 取消订阅事件处理
    /// </summary>
    public interface IUnsubscribeEvtMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 取消订阅事件处理
        /// </summary>
        /// <param name="message"></param>
        string UnsubscribeEventHandler(UnSubscribeEvtMessage message);
    }
}
