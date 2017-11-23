using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（扫码事件）
    /// </summary>
    public class ScanEvtMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IScanEvtMessageHandler _scanEventHandler;

        public ScanEvtMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IScanEvtMessageHandler scanEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _scanEventHandler = scanEventHandler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (message is ScanEvtMessage)//扫码事件消息
            {
                _scanEventHandler.ScanEventHandler(message as ScanEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
