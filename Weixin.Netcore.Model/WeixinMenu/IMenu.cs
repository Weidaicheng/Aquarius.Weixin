using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMenu.Button;

namespace Weixin.Netcore.Model.WeixinMenu
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// 按钮列表
        /// </summary>
        List<IButton> button { get; set; }

        /// <summary>
        /// 转化Json
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
