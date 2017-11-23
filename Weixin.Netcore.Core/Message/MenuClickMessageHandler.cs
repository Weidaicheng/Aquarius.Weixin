using Weixin.Netcore.Model.WeixinMessage;
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

        public MenuClickMessageHandler(MessageRepetHandler messageRepetHandler, IMessageReceive<ClickEvtMessage> messageReceive) 
        {
            _messageRepetHandler = messageRepetHandler;
            _messageReceive = messageReceive;
        }

        public string ClickEventHandler(string xml)
        {
            ClickEvtMessage receiveMsd = _messageReceive.GetEntity(xml);

            //消息重复
            if (!_messageRepetHandler.MessageRepetValid(receiveMsd.FromUserName + receiveMsd.CreateTime))
            {
                return "success";
            }

            TextMessage responseMsg = new TextMessage()
            {
                ToUserName = receiveMsd.FromUserName,
                FromUserName = receiveMsd.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = $"你点击了{receiveMsd.EventKey}按钮"
            };

            return responseMsg.CreateXml();
        }
    }
}
