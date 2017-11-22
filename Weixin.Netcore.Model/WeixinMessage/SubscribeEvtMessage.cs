using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 关注事件消息
    /// </summary>
    public class SubscribeEvtMessage : EventMessage, IMessageReceive
    {
        public SubscribeEvtMessage()
        {
            Event = "subscribe";
        }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
        }
    }
}
