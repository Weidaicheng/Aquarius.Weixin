using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（小视频消息）- 无重复验证
    /// </summary>
    public class ShortVideoMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IShortVideoMessageHandler _shortVideoMeessageHandler;

        public ShortVideoMessageProcesserCanRepet(IShortVideoMessageHandler shortVideoMessageHandler)
        {
            _shortVideoMeessageHandler = shortVideoMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ShortVideoMessage)//小视频消息
            {
                return _shortVideoMeessageHandler.ShortVideoMessageHandler(message as ShortVideoMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
