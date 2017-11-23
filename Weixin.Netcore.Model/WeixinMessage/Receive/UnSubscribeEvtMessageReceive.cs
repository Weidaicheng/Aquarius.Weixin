using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 取消关注事件消息接收
    /// </summary>
    public class UnSubscribeEvtMessageReceive : IMessageReceive<UnSubscribeEvtMessage>
    {
        public UnSubscribeEvtMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            var message = new UnSubscribeEvtMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"])
            };

            return message;
        }
    }
}
