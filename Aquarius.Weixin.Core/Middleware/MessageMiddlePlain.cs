using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity.Configuration;

namespace Aquarius.Weixin.Core.Middleware
{
    /// <summary>
    /// 消息中间件-明文
    /// </summary>
    public class MessageMiddlePlain : IMessageMiddleware
    {
        private readonly BaseSettings _baseSettings;
        private readonly Verifyer _verify;

        public MessageMiddlePlain(BaseSettings baseSettings, Verifyer verifyer)
        {
            _baseSettings = baseSettings;
            _verify = verifyer;
        }

        public string ReceiveMessageMiddle(string signature, string msgSignature, string timestamp, string nonce, string data)
        {
            //验证签名
            if (!_verify.VerifySignature(signature, timestamp, nonce, _baseSettings.Token))
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
