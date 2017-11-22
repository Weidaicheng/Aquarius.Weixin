using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    public class LocationMessage : MessageNormal, IMessageReceive
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

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            LocationX = double.Parse(dic["Location_X"]);
            LocationY = double.Parse(dic["Location_Y"]);
            Scale = int.Parse(dic["Scale"]);
            Label = dic["Label"];
        }
    }
}
