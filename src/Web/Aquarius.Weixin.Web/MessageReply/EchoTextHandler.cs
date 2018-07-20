using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Core.Message.Reply;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Web.MessageReply
{
    /// <summary>
    /// 文本消息Echo
    /// </summary>
    public class EchoTextHandler : ITextMessageHandler
    {
        private readonly IMessageReply<TextMessage> _messageReply;

        public EchoTextHandler(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }

        public string Handle(TextMessage message)
        {
            var textMessage = new TextMessage(message)
            {
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = message.Content
            };

            return _messageReply.CreateXml(textMessage);
        }
    }
}
