using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 扫码二维码事件处理-未实现
    /// </summary>
    public sealed class ScanEvtMessageHandlerNotImp : IScanEvtMessageHandler
    {
        public string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }
    }
}
