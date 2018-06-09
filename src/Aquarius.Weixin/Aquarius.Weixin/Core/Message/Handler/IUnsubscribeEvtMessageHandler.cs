using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 取消订阅事件处理
    /// </summary>
    public interface IUnsubscribeEvtMessageHandler : IMessageHandler<UnSubscribeEvtMessage>
    { }
}
