using System.Collections.Generic;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 位置消息接收
    /// </summary>
    public class LocationMessageReceive : IMessageReceive<LocationMessage>
    {
        public LocationMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new LocationMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                LocationX = double.Parse(dic["Location_X"]),
                LocationY = double.Parse(dic["Location_Y"]),
                Scale = int.Parse(dic["Scale"]),
                Label = dic["Label"]
            };
        }

        public LocationMessage GetEntity(Dictionary<string, string> dic)
        {
            return new LocationMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                LocationX = double.Parse(dic["Location_X"]),
                LocationY = double.Parse(dic["Location_Y"]),
                Scale = int.Parse(dic["Scale"]),
                Label = dic["Label"]
            };
        }
    }
}
