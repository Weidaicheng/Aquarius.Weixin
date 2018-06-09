using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    public interface ITextMessageHandler : IMessageHandler<TextMessage>
    { }
}
