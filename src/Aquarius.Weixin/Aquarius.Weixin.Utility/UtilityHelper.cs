using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Aquarius.Weixin.Utility
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
        /// 创建随机nonce
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateNonce(int length = 16)
        {
            char[] source = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                var index = r.Next(0, source.Length);
                sb.Append(source[index]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 创建32位订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateTradeNo()
        {
            var tradeNo = Guid.NewGuid();
            return tradeNo.ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// 对象转换为xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="canVEmpty">值是否可以为空</param>
        /// <returns></returns>
        public static string Obj2Xml<T>(T obj, bool canVEmpty = false)
        {
            //类型
            var type = typeof(T);
            //所有属性
            var properties = type.GetProperties();

            //xml
            string xml = $"<xml>";
            //读取所有属性值
            foreach (var item in properties)
            {
                //属性名
                var name = item.Name;
                //属性值
                var value = (type.GetProperty(name).GetValue(obj) ?? "").ToString();

                //Required检查
                var required = type.GetProperty(name).GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
                if (required != null && string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"The {name} field is required");
                }
                //MaxLength检查
                var maxLength = type.GetProperty(name).GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;
                if (maxLength != null && !string.IsNullOrEmpty(value) && value.Length > maxLength.Length)
                {
                    throw new ArgumentException($"The field {name} must be a string or array type with a maximum length of '{maxLength.Length}'");
                }

                if (!canVEmpty && string.IsNullOrEmpty(value))
                    continue;

                xml += $"<{name}>{value}</{name}>";
            }
            xml += $"</xml>";

            return xml;
        }

        /// <summary>
        /// 对象转换为字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="canVEmpty">值是否可以为空</param>
        /// <returns></returns>
        public static Dictionary<string, string> Obj2Dictionary<T>(T obj, bool canVEmpty = false)
        {
            //类型
            var type = typeof(T);
            //所有属性
            var properties = type.GetProperties();

            //dic
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //读取所有属性值
            foreach (var item in properties)
            {
                //属性名
                var name = item.Name;
                //属性值
                var value = (type.GetProperty(name).GetValue(obj) ?? "").ToString();

                if (!canVEmpty && string.IsNullOrEmpty(value))
                    continue;

                dic.Add(name, value);
            }

            return dic;
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
        /// 获取客户端IP
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetClientIp(HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        #region 加密
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SHA1Encrypt(string source)
        {
            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            sha = new SHA1CryptoServiceProvider();
            enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(source);
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            hash = BitConverter.ToString(dataHashed).Replace("-", "");
            hash = hash.ToLower();
            return hash;
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string source, string key)
        {
            using (HMACSHA256 sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5加密（UTF-8编码）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        #endregion

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
