using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（所有消息）
    /// </summary>
    public class MessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ITextMessageHandler _textMessageHandler;
        private readonly IImageMessageHandler _imageMessageHandler;
        private readonly IVoiceMessageHandler _voiceMessageHandlder;
        private readonly IVideoMessageHandler _videoMessageHandler;
        private readonly IShortVideoMessageHandler _shortVideoMeessageHandler;
        private readonly ILocationMessageHandler _locationMessageHandler;
        private readonly ILinkMessageHandler _linkMessageHandlder;
        private readonly ISubscribeEvtMessageHandler _subscribeEventHandler;
        private readonly IUnsubscribeEvtMessageHandler _unsubscribeEventHandler;
        private readonly IScanEvtMessageHandler _scanEventHandler;
        private readonly ILocationEvtMessageHandler _locationEventHandler;
        private readonly IClickEvtMessageHandler _clickEventHandler;

        public MessageProcesser(IMessageRepetHandler messageRepetHandler,
            ITextMessageHandler textMessageHandler, IImageMessageHandler imageMessageHandler,
            IVoiceMessageHandler voiceMessageHandler, IVideoMessageHandler videoMessageHandler,
            IShortVideoMessageHandler shortVideoMessageHandler, ILocationMessageHandler locationMessageHandler,
            ILinkMessageHandler linkMessageHandler, ISubscribeEvtMessageHandler subscribeEventHandler,
            IUnsubscribeEvtMessageHandler unsubscribeEventHandler, IScanEvtMessageHandler scanEventHandler,
            ILocationEvtMessageHandler locationEventHandler, IClickEvtMessageHandler clickEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _textMessageHandler = textMessageHandler;
            _imageMessageHandler = imageMessageHandler;
            _voiceMessageHandlder = voiceMessageHandler;
            _videoMessageHandler = videoMessageHandler;
            _shortVideoMeessageHandler = shortVideoMessageHandler;
            _locationMessageHandler = locationMessageHandler;
            _linkMessageHandlder = linkMessageHandler;
            _subscribeEventHandler = subscribeEventHandler;
            _unsubscribeEventHandler = unsubscribeEventHandler;
            _scanEventHandler = scanEventHandler;
            _locationEventHandler = locationEventHandler;
            _clickEventHandler = clickEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if(message is TextMessage)//文本消息
            {
                return _textMessageHandler.TextMessageHandler(message as TextMessage);
            }
            else if(message is ImageMessage)//图片消息
            {
                return _imageMessageHandler.ImageMessageHandler(message as ImageMessage);
            }
            else if(message is VoiceMessage)//语音消息
            {
                return _voiceMessageHandlder.VoiceMessageHandler(message as VoiceMessage);
            }
            else if(message is VideoMessage)//视频消息
            {
                return _videoMessageHandler.VideoMessageHandler(message as VideoMessage);
            }
            else if(message is ShortVideoMessage)//小视频消息
            {
                return _shortVideoMeessageHandler.ShortVideoMessageHandler(message as ShortVideoMessage);
            }
            else if(message is LocationMessage)//位置消息
            {
                return _locationMessageHandler.LocationMessageHandler(message as LocationMessage);
            }
            else if(message is LinkMessage)//链接消息
            {
                return _linkMessageHandlder.LinkMessageHandler(message as LinkMessage);
            }
            else if(message is SubscribeEvtMessage)//订阅事件消息
            {
                return _subscribeEventHandler.SubscribeEventHandler(message as SubscribeEvtMessage);
            }
            else if(message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                return _unsubscribeEventHandler.UnsubscribeEventHandler(message as UnSubscribeEvtMessage);
            }
            else if(message is ScanEvtMessage)//扫码事件消息
            {
                return _scanEventHandler.ScanEventHandler(message as ScanEvtMessage);
            }
            else if(message is LocationEvtMessage)//位置上报事件消息
            {
                return _locationEventHandler.LocationEventHandler(message as LocationEvtMessage);
            }
            else if(message is ClickEvtMessage)//点击事件消息
            {
                return _clickEventHandler.ClickEventHandler(message as ClickEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
