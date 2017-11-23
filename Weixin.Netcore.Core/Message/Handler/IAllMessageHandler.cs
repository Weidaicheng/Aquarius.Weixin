namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 消息/事件处理
    /// </summary>
    public interface IAllMessageHandler : INormalMessageHandler, IEventMessageHandler
    { }
}
