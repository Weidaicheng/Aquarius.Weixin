using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Weixin.Netcore.Utility
{
    /// <summary>
    /// 通用静态方法帮助类
    /// </summary>
    public static class UtilityHelper
    {
        /// <summary>
		/// 获取时间戳
		/// </summary>
		/// <returns></returns>
		public static long GetTimeStamp()
        {
            DateTime startUtc = new DateTime(1970, 1, 1);
            DateTime nowUtc = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
            return (long)(nowUtc - startUtc).TotalSeconds;
        }

        /// <summary>
		/// XML转换为字典
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static Dictionary<string, string> Xml2Dictionary(string xml)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xml);
            XmlElement root = xmlDoc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                dictionary.Add(node.Name, node.InnerText);
            }

            return dictionary;
        }

        /// <summary>
		/// 签名有效性验证
		/// </summary>
		/// <param name="signature"></param>
		/// <param name="timestamp"></param>
		/// <param name="nonce"></param>
		/// <returns></returns>
		public static bool VerifySignature(string timestamp, string nonce, string token, string signature)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);

            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            if (enText.ToString() == signature)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 消息签名有效性验证
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <param name="msgEncrypted"></param>
        /// <param name="msgSignature"></param>
        /// <returns></returns>
        public static bool VerifySignature(string timestamp, string nonce, string token, string msgEncrypted, string msgSignature)
        {
            string hash = GenarateMsgSinature(timestamp, nonce, token, msgEncrypted);
            return hash == msgSignature;
        }

        /// <summary>
        /// 创建消息签名
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="msgEncrypted"></param>
        /// <returns></returns>
        public static string GenarateMsgSinature(string timestamp, string nonce, string token, string msgEncrypted)
        {
            var arr = new[] { token, timestamp, nonce, msgEncrypted }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            sha = new SHA1CryptoServiceProvider();
            enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(arrString);
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            hash = BitConverter.ToString(dataHashed).Replace("-", "");
            hash = hash.ToLower();
            return hash;
        }

        /// <summary>
        /// 创建随机nonce
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonce()
        {
            Random random = new Random();
            int nonce = random.Next(100000000, 999999999);
            return nonce.ToString();
        }
    }
}
