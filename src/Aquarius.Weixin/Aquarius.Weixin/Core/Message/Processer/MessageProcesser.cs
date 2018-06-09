using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    public sealed class MessageProcesser
    {
        private readonly MessageRepetHandler _messageRepetHandler;
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
        private readonly IScanSubscribeEvtMessageHandler _scanSubscribeEventHandler;
        private readonly IViewEvtMessageHandler _viewEvtMessageHandler;

        public MessageProcesser(MessageRepetHandler messageRepetHandler,
            ITextMessageHandler textMessageHandler, IImageMessageHandler imageMessageHandler,
            IVoiceMessageHandler voiceMessageHandler, IVideoMessageHandler videoMessageHandler,
            IShortVideoMessageHandler shortVideoMessageHandler, ILocationMessageHandler locationMessageHandler,
            ILinkMessageHandler linkMessageHandler, ISubscribeEvtMessageHandler subscribeEventHandler,
            IUnsubscribeEvtMessageHandler unsubscribeEventHandler, IScanEvtMessageHandler scanEventHandler,
            ILocationEvtMessageHandler locationEventHandler, IClickEvtMessageHandler clickEventHandler,
            IScanSubscribeEvtMessageHandler scanSubscribeEventHandler, IViewEvtMessageHandler viewEvtMessageHandler)
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
            _scanSubscribeEventHandler = scanSubscribeEventHandler;
            _viewEvtMessageHandler = viewEvtMessageHandler;
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ProcessMessage(IMessage message)
        {
            if (message is TextMessage)//文本消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as TextMessage).MsgId.ToString()))
                    return Consts.Success;
                return _textMessageHandler.Handle(message as TextMessage);
            }
            else if(message is ImageMessage)//图片消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ImageMessage).MsgId.ToString()))
                    return Consts.Success;
                return _imageMessageHandler.Handle(message as ImageMessage);
            }
            else if(message is VoiceMessage)//语音消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as VoiceMessage).MsgId.ToString()))
                    return Consts.Success;
                return _voiceMessageHandlder.Handle(message as VoiceMessage);
            }
            else if(message is VideoMessage)//视频消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as VideoMessage).MsgId.ToString()))
                    return Consts.Success;
                return _videoMessageHandler.Handle(message as VideoMessage);
            }
            else if(message is ShortVideoMessage)//小视频消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ShortVideoMessage).MsgId.ToString()))
                    return Consts.Success;
                return _shortVideoMeessageHandler.Handle(message as ShortVideoMessage);
            }
            else if(message is LocationMessage)//位置消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as LocationMessage).MsgId.ToString()))
                    return Consts.Success;
                return _locationMessageHandler.Handle(message as LocationMessage);
            }
            else if(message is LinkMessage)//链接消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as LinkMessage).MsgId.ToString()))
                    return Consts.Success;
                return _linkMessageHandlder.Handle(message as LinkMessage);
            }
            else if(message is SubscribeEvtMessage)//订阅事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as SubscribeEvtMessage).FromUserName + (message as SubscribeEvtMessage).CreateTime))
                    return Consts.Success;
                return _subscribeEventHandler.Handle(message as SubscribeEvtMessage);
            }
            else if(message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as UnSubscribeEvtMessage).FromUserName + (message as UnSubscribeEvtMessage).CreateTime))
                    return Consts.Success;
                return _unsubscribeEventHandler.Handle(message as UnSubscribeEvtMessage);
            }
            else if(message is ScanEvtMessage)//扫码事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ScanEvtMessage).FromUserName + (message as ScanEvtMessage).CreateTime))
                    return Consts.Success;
                return _scanEventHandler.Handle(message as ScanEvtMessage);
            }
            else if(message is LocationEvtMessage)//位置上报事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as LocationEvtMessage).FromUserName + (message as LocationEvtMessage).CreateTime))
                    return Consts.Success;
                return _locationEventHandler.Handle(message as LocationEvtMessage);
            }
            else if(message is ClickEvtMessage)//点击事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ClickEvtMessage).FromUserName + (message as ClickEvtMessage).CreateTime))
                    return Consts.Success;
                return _clickEventHandler.Handle(message as ClickEvtMessage);
            }
            else if(message is ScanSubscribeEvtMessage)//扫码订阅事件消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ClickEvtMessage).FromUserName + (message as ClickEvtMessage).CreateTime))
                    return Consts.Success;
                return _scanSubscribeEventHandler.Handle(message as ScanSubscribeEvtMessage);
            }
            else if(message is ViewEvtMessage)//自定义View菜单事件
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ViewEvtMessage).FromUserName + (message as ViewEvtMessage).CreateTime))
                    return Consts.Success;
                return _viewEvtMessageHandler.Handle(message as ViewEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException($"不支持的消息类型：{message.GetType().FullName}");
            }
        }

        /// <summary>
        /// 处理消息，先进行<see cref="MessageParser.ParseMessage(string)"/>
        /// </summary>
        /// <param name="messageStr"></param>
        /// <returns></returns>
        public string ProcessMessage(string messageStr)
        {
            IMessage message = MessageParser.ParseMessage(messageStr);
            return ProcessMessage(message);
        }
    }
}
