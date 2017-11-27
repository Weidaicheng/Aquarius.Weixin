using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（普通消息）
    /// </summary>
    public class NormalMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;
        private readonly IMessageRepetValidUsage _messageRepetValidUsage;
        private readonly ITextMessageHandler _textMessageHandler;
        private readonly IImageMessageHandler _imageMessageHandler;
        private readonly IVoiceMessageHandler _voiceMessageHandlder;
        private readonly IVideoMessageHandler _videoMessageHandler;
        private readonly IShortVideoMessageHandler _shortVideoMeessageHandler;
        private readonly ILocationMessageHandler _locationMessageHandler;
        private readonly ILinkMessageHandler _linkMessageHandlder;

        public NormalMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IMessageRepetValidUsage messageRepetValidUsage,
            ITextMessageHandler textMessageHandler, IImageMessageHandler imageMessageHandler,
            IVoiceMessageHandler voiceMessageHandler, IVideoMessageHandler videoMessageHandler,
            IShortVideoMessageHandler shortVideoMessageHandler, ILocationMessageHandler locationMessageHandler,
            ILinkMessageHandler linkMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            _messageRepetValidUsage = messageRepetValidUsage;
            _textMessageHandler = textMessageHandler;
            _imageMessageHandler = imageMessageHandler;
            _voiceMessageHandlder = voiceMessageHandler;
            _videoMessageHandler = videoMessageHandler;
            _shortVideoMeessageHandler = shortVideoMessageHandler;
            _locationMessageHandler = locationMessageHandler;
            _linkMessageHandlder = linkMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is TextMessage)//文本消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as TextMessage).MsgId.ToString()))
                    return "success";
                return _textMessageHandler.TextMessageHandler(message as TextMessage);
            }
            else if (message is ImageMessage)//图片消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as ImageMessage).MsgId.ToString()))
                    return "success";
                return _imageMessageHandler.ImageMessageHandler(message as ImageMessage);
            }
            else if (message is VoiceMessage)//语音消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as VoiceMessage).MsgId.ToString()))
                    return "success";
                return _voiceMessageHandlder.VoiceMessageHandler(message as VoiceMessage);
            }
            else if (message is VideoMessage)//视频消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as VideoMessage).MsgId.ToString()))
                    return "success";
                return _videoMessageHandler.VideoMessageHandler(message as VideoMessage);
            }
            else if (message is ShortVideoMessage)//小视频消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as ShortVideoMessage).MsgId.ToString()))
                    return "success";
                return _shortVideoMeessageHandler.ShortVideoMessageHandler(message as ShortVideoMessage);
            }
            else if (message is LocationMessage)//位置消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as LocationMessage).MsgId.ToString()))
                    return "success";
                return _locationMessageHandler.LocationMessageHandler(message as LocationMessage);
            }
            else if (message is LinkMessage)//链接消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as LinkMessage).MsgId.ToString()))
                    return "success";
                return _linkMessageHandlder.LinkMessageHandler(message as LinkMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
