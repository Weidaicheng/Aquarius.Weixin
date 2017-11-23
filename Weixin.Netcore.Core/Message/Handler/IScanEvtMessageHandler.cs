using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 扫描二维码事件处理
    /// </summary>
    public interface IScanEvtMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 扫描二维码事件处理
        /// </summary>
        /// <param name="message"></param>
        string ScanEventHandler(ScanEvtMessage message);
    }
}
