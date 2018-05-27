using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 位置消息接收
    /// </summary>
    public class LocationMessageReceive : IMessageReceive//<LocationMessage>
    {
        public IMessage GetEntity(string xml)
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

        public IMessage GetEntity(Dictionary<string, string> dic)
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
