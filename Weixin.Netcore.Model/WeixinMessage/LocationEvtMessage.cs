namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 地理位置上报事件消息
    /// </summary>
    public class LocationEvtMessage : EventMessage
    {
        public LocationEvtMessage()
        {
            Event = "LOCATION";
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; internal set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; internal set; }

        /// <summary>
        /// 精度
        /// </summary>
        public double Precision { get; internal set; }
    }
}
