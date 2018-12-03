using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.Message.Processer;
using Aquarius.Weixin.Core.Middleware;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.Incoming;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message
{
    /// <summary>
    /// 服务器消息处理机
    /// </summary>
    public class MessageFacade
    {
        private readonly Verifyer _verifyer;
        private readonly IMessageMiddleware _messageMiddleware;
        private readonly MessageProcesser _processer;
        private readonly BaseSettings _baseSettings;

        public MessageFacade(Verifyer verifyer,
            IMessageMiddleware messageMiddleware,
            MessageProcesser processer,
            BaseSettings baseSettings)
        {
            _verifyer = verifyer;
            _messageMiddleware = messageMiddleware;
            _processer = processer;
            _baseSettings = baseSettings;
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="incoming">消息签名等信息</param>
        /// <param name="data">消息正文</param>
        /// <returns>消息回复</returns>
        public string Reply(MessageIncoming incoming, string data)
        {
            if (!string.IsNullOrEmpty(incoming.echostr))
            {
                //服务器认证
                if (_verifyer.VerifySignature(incoming.signature, incoming.timestamp, incoming.nonce, _baseSettings.Token))
                {
                    return incoming.echostr;
                }
                else
                {
                    return Consts.Success;
                }
            }
            else
            {
                //接收消息中间处理
                data = _messageMiddleware.ReceiveMessageMiddle(
                    incoming.signature,
                    incoming.msg_signature,
                    incoming.timestamp,
                    incoming.nonce, data);

                IMessage message = MessageParser.ParseMessage(data);
                string reply = _processer.ProcessMessage(message);

                //回复消息中间处理
                reply = _messageMiddleware.ReplyMessageMiddle(reply);

                return reply;
            }
        }
    }
}
