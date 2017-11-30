﻿using System.Collections.Generic;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 地理位置上报事件消息接收
    /// </summary>
    public class LocationEvtMessageReceive : IMessageReceive<LocationEvtMessage>
    {
        public LocationEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new LocationEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                Latitude = double.Parse(dic["Latitude"]),
                Longitude = double.Parse(dic["Longitude"]),
                Precision = double.Parse(dic["Precision"])
            };
        }

        public LocationEvtMessage GetEntity(Dictionary<string, string> dic)
        {
            return new LocationEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                Latitude = double.Parse(dic["Latitude"]),
                Longitude = double.Parse(dic["Longitude"]),
                Precision = double.Parse(dic["Precision"])
            };
        }
    }
}
