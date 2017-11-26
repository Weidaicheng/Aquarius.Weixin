using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（扫码事件）- 无重复验证
    /// </summary>
    public class ScanEvtMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IScanEvtMessageHandler _scanEventHandler;

        public ScanEvtMessageProcesserCanRepet(IScanEvtMessageHandler scanEventHandler)
        {
            _scanEventHandler = scanEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ScanEvtMessage)//扫码事件消息
            {
                return _scanEventHandler.ScanEventHandler(message as ScanEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
