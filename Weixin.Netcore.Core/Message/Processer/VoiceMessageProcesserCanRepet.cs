using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（语音消息）- 无重复验证
    /// </summary>
    public class VoiceMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IVoiceMessageHandler _voiceMessageHandlder;

        public VoiceMessageProcesserCanRepet(IVoiceMessageHandler voiceMessageHandler)
        {
            _voiceMessageHandlder = voiceMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is VoiceMessage)//语音消息
            {
                return _voiceMessageHandlder.VoiceMessageHandler(message as VoiceMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
