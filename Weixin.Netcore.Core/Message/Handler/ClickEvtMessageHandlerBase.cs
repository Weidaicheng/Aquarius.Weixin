using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public class ClickEvtMessageHandlerBase
    {
        public virtual string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }
    }
}
