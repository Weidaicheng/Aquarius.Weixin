using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 订阅事件处理
    /// </summary>
    public class SubscribeEvtMessageHandlerBase
    {
        public virtual string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
