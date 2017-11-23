using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（语音消息）
    /// </summary>
    public class VoiceMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IVoiceMessageHandler _voiceMessageHandlder;

        public VoiceMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IVoiceMessageHandler voiceMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _voiceMessageHandlder = voiceMessageHandler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (message is VoiceMessage)//语音消息
            {
                _voiceMessageHandlder.VoiceMessageHandler(message as VoiceMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
