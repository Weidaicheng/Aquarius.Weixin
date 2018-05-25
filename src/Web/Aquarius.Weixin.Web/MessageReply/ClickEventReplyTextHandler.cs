using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Core.Message.Reply;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Web.MessageReply
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
