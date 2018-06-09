using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 扫码二维码事件处理
    /// </summary>
    public interface IScanEvtMessageHandler : IMessageHandler<ScanEvtMessage>
    { }
}
