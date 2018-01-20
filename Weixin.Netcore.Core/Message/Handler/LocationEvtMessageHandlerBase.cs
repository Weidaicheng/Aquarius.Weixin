using Weixin.Netcore.Entity.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 上报位置时间处理
    /// </summary>
    public class LocationEvtMessageHandlerBase
    {
        public virtual string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }
    }
}
