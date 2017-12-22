using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 订阅事件处理
    /// </summary>
    public class SubscribeEvtMessageHandler : IMessageHandler
    {
        public virtual string Handle(IMessage message)
        {
            return "success";
        }
    }
}
