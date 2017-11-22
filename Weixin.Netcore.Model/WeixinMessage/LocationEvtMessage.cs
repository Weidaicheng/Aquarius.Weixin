using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 地理位置上报事件消息
    /// </summary>
    public class LocationEvtMessage : EventMessage, IMessageReceive
    {
        public LocationEvtMessage()
        {
            Event = "LOCATION";
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public double Precision { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            Latitude = double.Parse(dic["Latitude"]);
            Longitude = double.Parse(dic["Longitude"]);
            Precision = double.Parse(dic["Precision"]);
        }
    }
}
