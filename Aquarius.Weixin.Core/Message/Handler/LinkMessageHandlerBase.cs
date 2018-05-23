using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 链接消息处理
    /// </summary>
    public class LinkMessageHandlerBase
    {
        public virtual string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }
    }
}
