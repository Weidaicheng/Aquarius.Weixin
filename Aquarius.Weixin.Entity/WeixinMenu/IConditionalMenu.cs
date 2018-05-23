using Aquarius.Weixin.Entity.WeixinMenu.Conditional;

namespace Aquarius.Weixin.Entity.WeixinMenu
{
    /// <summary>
    /// 个性化菜单
    /// </summary>
    public interface IConditionalMenu : IMenu
    {
        IMatchRule matchrule { get; set; }
    }
}
