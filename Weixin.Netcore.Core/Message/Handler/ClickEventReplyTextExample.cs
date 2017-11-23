using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Reply;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 点击回复文本消息例子
    /// </summary>
    public class ClickEventReplyTextExample : IClickEventHandler
    {
        private readonly IMessageReply<TextMessage> _messageReply;

        public ClickEventReplyTextExample(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }

        public string ClickEventHandler(ClickEvtMessage message)
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
