using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 扫码二维码事件处理
    /// </summary>
    public class ScanEvtMessageHandlerBase
    {
        public virtual string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }
    }
}
