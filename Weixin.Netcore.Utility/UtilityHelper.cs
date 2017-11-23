using System;
using System.Collections.Generic;
using System.Linq;
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
        //private static string pattern = @"(?<=`)[a-zA-Z]+?(?=`)";

        /// <summary>
		/// 获取时间戳
		/// </summary>
		/// <returns></returns>
		public static long GetTimeStamp()
        {
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (long)(DateTime.Now - startTime).TotalSeconds;
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
		/// 消息有效性验证
		/// </summary>
		/// <param name="signature"></param>
		/// <param name="timestamp"></param>
		/// <param name="nonce"></param>
        /// <param name="isDebug">调试模式（默认为false）</param>
		/// <returns></returns>
		public static bool CheckSignature(string signature, string timestamp, string nonce, string token, bool isDebug = false)
        {
            if (isDebug)
                return true;

            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
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
        /// 正则替换
        /// </summary>
        /// <param name="handleStr"></param>
        /// <param name="configuration"></param>
        public static void RegexReplace(ref string handleStr, string pattern, IEnumerable<KeyValuePair<string, string>> configuration)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matchs = regex.Matches(handleStr);
            foreach (Match item in matchs)
            {
                var value = string.IsNullOrEmpty(configuration.FirstOrDefault(x => x.Key == item.Value).Value) ? "" : configuration.FirstOrDefault(x => x.Key == item.Value).Value;
                handleStr = handleStr.Replace($"`{item.Value}`", value);
            }
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="handleStr"></param>
        /// <param name="configuration"></param>
        public static void RegexReplace(ref string handleStr, string pattern, Dictionary<string, string> configuration)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matchs = regex.Matches(handleStr);
            foreach (Match item in matchs)
            {
                var value = string.IsNullOrEmpty(configuration[item.Value]) ? "" : configuration[item.Value];
                handleStr = handleStr.Replace($"`{item.Value}`", value);
            }
        }
    }
}
