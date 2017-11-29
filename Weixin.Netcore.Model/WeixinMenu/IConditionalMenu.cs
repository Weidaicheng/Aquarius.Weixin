using Weixin.Netcore.Model.WeixinMenu.Conditional;

namespace Weixin.Netcore.Model.WeixinMenu
{
    /// <summary>
    /// 个性化菜单
    /// </summary>
    public interface IConditionalMenu : IMenu
    {
        IMatchRule matchrule { get; set; }
    }
}
