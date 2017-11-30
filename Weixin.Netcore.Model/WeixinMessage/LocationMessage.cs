namespace Weixin.Netcore.Model.WeixinMessage
{
    public class LocationMessage : NormalMessage
    {
        public LocationMessage()
        {
            MsgType = "location";
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public double LocationX { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double LocationY { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
}
