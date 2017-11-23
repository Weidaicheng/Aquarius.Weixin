using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Receive;
using Weixin.Netcore.Model.WeixinMessage.Reply;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 菜单按钮点击消息处理
    /// </summary>
    public class MenuClickMessageHandler : IClickEventHandler
    {
        private readonly MessageRepetHandler _messageRepetHandler;
        private readonly IMessageReceive<ClickEvtMessage> _messageReceive;
        private readonly IMessageReply<TextMessage> _messageReply;

        public MenuClickMessageHandler(MessageRepetHandler messageRepetHandler, IMessageReceive<ClickEvtMessage> messageReceive,
            IMessageReply<TextMessage> messageReply) 
        {
            _messageRepetHandler = messageRepetHandler;
            _messageReceive = messageReceive;
            _messageReply = messageReply;
        }

        public string ClickEventHandler(string xml)
        {
            ClickEvtMessage receiveMsg = _messageReceive.GetEntity(xml);

            //消息重复
            if (!_messageRepetHandler.MessageRepetValid(receiveMsg.FromUserName + receiveMsg.CreateTime))
            {
                return "success";
            }

            TextMessage textMessage = new TextMessage()
            {
                ToUserName = receiveMsg.FromUserName,
                FromUserName = receiveMsg.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = $"你点击了{receiveMsg.EventKey}按钮"
            };

            return _messageReply.CreateXml(textMessage);
        }
    }
}
