using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 链接消息处理
    /// </summary>
    public class LinkMessageHandler : IMessageHandler
    {
        public virtual string Handle(IMessage message)
        {
            return "success";
        }
    }
}
