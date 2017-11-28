using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 文本消息处理-未实现
    /// </summary>
    public sealed class TextMessageHandlerNotImp : ITextMessageHandler
    {
        public string TextMessageHandler(TextMessage message)
        {
            return "success";
        }
    }
}
