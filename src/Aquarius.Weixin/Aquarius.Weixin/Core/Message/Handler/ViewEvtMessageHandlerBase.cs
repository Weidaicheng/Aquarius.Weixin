using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 自定义View菜单事件
    /// </summary>
    public class ViewEvtMessageHandlerBase
    {
        public virtual string ViewEventHandler(ViewEvtMessage message)
        {
            return "success";
        }
    }
}
