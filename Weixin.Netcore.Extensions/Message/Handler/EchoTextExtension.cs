using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Reply;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Extensions.Message.Handler
{
    /// <summary>
    /// 文本消息Echo
    /// </summary>
    public class EchoTextExtension : ITextMessageHandler
    {
        private readonly IMessageReply<TextMessage> _messageReply;

        public EchoTextExtension(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }

        public string TextMessageHandler(TextMessage message)
        {
            var textMessage = new TextMessage()
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = message.Content
            };

            return _messageReply.CreateXml(textMessage);
        }
    }
}
