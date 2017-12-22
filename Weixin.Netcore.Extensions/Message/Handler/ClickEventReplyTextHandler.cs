using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.Message.Reply;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Extensions.Message.Handler
{
    /// <summary>
    /// 点击回复文本消息扩展
    /// </summary>
    public class ClickEventReplyTextHandler : ClickEvtMessageHandlerBase
    {
        private readonly IMessageReply<TextMessage> _messageReply;

        public ClickEventReplyTextHandler(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }

        public override string ClickEventHandler(ClickEvtMessage message)
        {
            var textMessage = new TextMessage()
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = $"你点击了{message.EventKey}按钮"
            };

            return _messageReply.CreateXml(textMessage);
        }
    }
}
