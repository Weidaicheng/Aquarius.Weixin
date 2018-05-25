namespace Aquarius.Weixin.Entity.WeixinMessage
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

        public LocationEvtMessage(MessageBase receivedMsg) : base(receivedMsg)
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
    }
}
