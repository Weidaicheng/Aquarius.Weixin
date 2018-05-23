using System;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 地理位置消息处理
    /// </summary>
    public class LocationMessageHandlerBase
    {
        public virtual string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }
    }
}
