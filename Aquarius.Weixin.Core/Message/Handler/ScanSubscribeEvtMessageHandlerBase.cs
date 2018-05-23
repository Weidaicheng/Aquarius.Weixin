using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
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
