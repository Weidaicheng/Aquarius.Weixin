using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Middleware
{
    /// <summary>
    /// 消息中间件-明文
    /// </summary>
    public class MessageMiddlePlain : IMessageMiddleware
    {
        private readonly BaseSettings _baseSettings;

        public MessageMiddlePlain(BaseSettings baseSettings)
        {
            _baseSettings = baseSettings;
        }

        public string ReceiveMessageMiddle(string signature, string msgSignature, string timestamp, string nonce, string data)
        {
            //验证签名
            if (!UtilityHelper.VerifySignature(timestamp, nonce, _baseSettings.Token, signature))
            {
                throw new SignatureInValidException("签名非法");
            }

            return data;
        }

        public string ReplyMessageMiddle(string replyMsg)
        {
            return replyMsg;
        }
    }
}
