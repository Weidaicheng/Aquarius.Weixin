using Weixin.Netcore.Entity.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    public class TextMessageHandlerBase
    {
        public virtual string TextMessageHandler(TextMessage message)
        {
            return "success";
        }
    }
}
