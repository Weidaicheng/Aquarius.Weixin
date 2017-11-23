namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 事件消息处理
    /// </summary>
    public interface IEventMessageHandler : ISubscribeEventHandler, IUnsubscribeEventHandler, 
        IScanEventHandler, ILocationEventHandler, IClickEventHandler
    { }
}
