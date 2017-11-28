using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 上报位置时间处理-未实现
    /// </summary>
    public sealed class LocationEvtMessageHandlerNotImp : ILocationEvtMessageHandler
    {
        public string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }
    }
}
