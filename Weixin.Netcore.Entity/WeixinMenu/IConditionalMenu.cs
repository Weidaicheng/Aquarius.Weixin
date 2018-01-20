using Weixin.Netcore.Entity.WeixinMenu.Conditional;

namespace Weixin.Netcore.Entity.WeixinMenu
{
    /// <summary>
    /// 个性化菜单
    /// </summary>
    public interface IConditionalMenu : IMenu
    {
        IMatchRule matchrule { get; set; }
    }
}
