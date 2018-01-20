using Weixin.Netcore.Entity.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 取消订阅事件处理
    /// </summary>
    public class UnsubscribeEvtMessageHandlerBase
    {
        public virtual string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
