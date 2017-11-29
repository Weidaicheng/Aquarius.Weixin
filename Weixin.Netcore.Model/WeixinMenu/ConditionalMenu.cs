using Newtonsoft.Json;
using System.Collections.Generic;
using Weixin.Netcore.Model.WeixinMenu.Button;
using Weixin.Netcore.Model.WeixinMenu.Conditional;

namespace Weixin.Netcore.Model.WeixinMenu
{
    /// <summary>
    /// 个性化菜单
    /// </summary>
    public class ConditionalMenu : IConditionalMenu
    {
        public ConditionalMenu()
        {
            button = new List<IButton>();
        }

        public IMatchRule matchrule { get ; set; }
        public List<IButton> button { get ; set ; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
