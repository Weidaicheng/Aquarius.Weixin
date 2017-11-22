namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 事件消息处理
    /// </summary>
    public interface IEventMessageHandler : ISubscribeEventHandler, IUnsubscribeEventHandler, 
        IScanSubscribeEventHandler, IScanEventHandler, ILocationEventHandler, IClickEventHandler
    { }
}
