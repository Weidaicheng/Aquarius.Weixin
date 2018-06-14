using System;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultLocationMessageHandler : ILocationMessageHandler
    {
        public string Handle(LocationMessage message)
        {
            return Consts.Success;
        }
    }
}
