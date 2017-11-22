using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 取消关注事件消息
    /// </summary>
    public class UnSubscribeEvtMessage : EventMessage, IMessageReceive
    {
        public UnSubscribeEvtMessage()
        {
            Event = "unsubscribe";
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
