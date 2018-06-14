using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 扫码订阅事件处理
    /// </summary>
    public interface IScanSubscribeEvtMessageHandler : IMessageHandler<ScanSubscribeEvtMessage>
    { }
}
