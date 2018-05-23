using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
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
