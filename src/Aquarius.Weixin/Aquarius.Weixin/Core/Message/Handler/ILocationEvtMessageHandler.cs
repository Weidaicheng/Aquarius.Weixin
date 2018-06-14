using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 上报位置事件处理
    /// </summary>
    public interface ILocationEvtMessageHandler : IMessageHandler<LocationEvtMessage>
    { }
}
