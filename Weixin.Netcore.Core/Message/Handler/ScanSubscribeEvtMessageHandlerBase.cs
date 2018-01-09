using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 扫码订阅事件处理
    /// </summary>
    public class ScanSubscribeEvtMessageHandlerBase
    {
        public virtual string ScanSubscribeEventHandler(ScanSubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
