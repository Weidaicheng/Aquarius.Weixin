using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Reply;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Extensions.Message.Handler
{
    /// <summary>
    /// 文本消息Echo
    /// </summary>
    public class EchoTextHandler : TextMessageHandler
    {
        private readonly IMessageReply<TextMessage> _messageReply;

        public EchoTextHandler(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }

        public override string TextMessageHandler(TextMessage message)
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
