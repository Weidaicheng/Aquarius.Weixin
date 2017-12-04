﻿using System.Text;
using System.Xml;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Middleware
{
    /// <summary>
    /// 消息中间件-密文
    /// </summary>
    public class MessageMiddleCipher : IMessageMiddleware
    {
        private readonly BaseSettings _baseSettings;

        public MessageMiddleCipher(BaseSettings baseSettings)
        {
            _baseSettings = baseSettings;
        }

        public string ReceiveMessageMiddle(string signature, string msgSignature, string timestamp, string nonce, string data)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root;
            string encryptedMsg;
            doc.LoadXml(data);
            root = doc.FirstChild;
            encryptedMsg = root["Encrypt"].InnerText;

            //验证签名
            if(!UtilityHelper.VerifySignature(timestamp, nonce, _baseSettings.Token, signature))
            {
                throw new SignatureInValidException("签名非法");
            }

            if (!UtilityHelper.VerifyMsgSignature(timestamp, nonce, _baseSettings.Token, encryptedMsg, msgSignature))
            {
                throw new SignatureInValidException("消息签名非法");
            }

            //解密
            string message = CryptographyHelper.AESDecrypt(encryptedMsg, _baseSettings.EncodingAESKey, out string appId);
            if (appId != _baseSettings.AppId)
            {
                throw new AppIdInValidException("AppId验证失败");
            }
            return message;
        }

        public string ReplyMessageMiddle(string replyMsg)
        {
            string replyMsgEncrypted = CryptographyHelper.AESEncrypt(replyMsg, _baseSettings.EncodingAESKey, _baseSettings.AppId);
            string timestamp = UtilityHelper.GetTimeStamp().ToString();
            string nonce = UtilityHelper.GenerateNonce();
            string signature = UtilityHelper.GenarateMsgSinature(_baseSettings.Token, timestamp, nonce, replyMsgEncrypted);

            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<Encrypt><![CDATA[{replyMsgEncrypted}]]></Encrypt>");
            sb.Append($"<MsgSignature><![CDATA[{signature}]]></MsgSignature>");
            sb.Append($"<TimeStamp><![CDATA[{timestamp}]]></TimeStamp>");
            sb.Append($"<Nonce><![CDATA[{nonce}]]></Nonce>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
