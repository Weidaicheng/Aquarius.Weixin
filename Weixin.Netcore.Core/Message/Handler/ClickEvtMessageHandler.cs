using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public class ClickEvtMessageHandler : IMessageHandler
    {
        public virtual string Handle(IMessage message)
        {
            return "success";
        }
    }
}
