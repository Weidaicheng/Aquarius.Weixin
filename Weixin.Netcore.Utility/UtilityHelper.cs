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
        public static bool VerifyMsgSignature(string timestamp, string nonce, string token, string msgEncrypted, string msgSignature)
        {
            string hash = GenerateSinature(timestamp, nonce, token, msgEncrypted);
            return hash == msgSignature;
        }

        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string GenerateSinature(params string[] strings)
        {
            var arr = strings.OrderBy(z => z).ToArray();
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
        /// 创建JS-API签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateJsApiSignature(string ticket, string noncestr, long timestamp, string url)
        {
            string str = $"jsapi_ticket={ticket}&noncestr={noncestr}&timestamp={timestamp}&url={url}";
            return GenerateSinature(str);
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

        #region 距离计算
        #region private
        private static double EARTH_RADIUS = 6371.0;//km 地球半径 平均值，千米

        private static double HaverSin(double theta)
        {
            var v = Math.Sin(theta / 2);
            return v * v;
        }

        /// <summary>
        /// 将角度换算为弧度。
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>弧度</returns>
        private static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private static double ConvertRadiansToDegrees(double radian)
        {
            return radian * 180.0 / Math.PI;
        }
        #endregion

        /// <summary>
        /// 给定的经度1，纬度1；经度2，纬度2. 计算2个经纬度之间的距离。
        /// </summary>
        /// <param name="lon1">经度1</param>
        /// <param name="lat1">纬度1</param>
        /// <param name="lon2">经度2</param>
        /// <param name="lat2">纬度2</param>
        /// <returns>距离（公里、千米）</returns>
        public static double Distance(double lon1, double lat1, double lon2, double lat2)
        {
            //用haversine公式计算球面两点间的距离。
            //经纬度转换成弧度
            lat1 = ConvertDegreesToRadians(lat1);
            lon1 = ConvertDegreesToRadians(lon1);
            lat2 = ConvertDegreesToRadians(lat2);
            lon2 = ConvertDegreesToRadians(lon2);

            //差值
            var vLon = Math.Abs(lon1 - lon2);
            var vLat = Math.Abs(lat1 - lat2);

            //h is the great circle distance in radians, great circle就是一个球体上的切面，它的圆心即是球心的一个周长最大的圆。
            var h = HaverSin(vLat) + Math.Cos(lat1) * Math.Cos(lat2) * HaverSin(vLon);

            var distance = 2 * EARTH_RADIUS * Math.Asin(Math.Sqrt(h));

            return distance;
        }
        #endregion
    }
}
