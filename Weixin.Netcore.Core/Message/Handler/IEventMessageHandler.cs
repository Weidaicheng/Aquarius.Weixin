using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 事件消息处理
    /// </summary>
    public interface IEventMessageHandler : ISubscribeEvtMessageHandler, IUnsubscribeEvtMessageHandler, 
        IScanEvtMessageHandler, ILocationEvtMessageHandler, IClickEvtMessageHandler
    { }
}
