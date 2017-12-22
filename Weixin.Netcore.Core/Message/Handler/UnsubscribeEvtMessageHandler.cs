using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 取消订阅事件处理
    /// </summary>
    public class UnsubscribeEvtMessageHandler : IMessageHandler
    {
        public virtual string Handle(IMessage message)
        {
            return "success";
        }
    }
}
