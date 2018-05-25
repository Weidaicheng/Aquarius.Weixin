using Newtonsoft.Json;
using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMenu.Button;
using Aquarius.Weixin.Entity.WeixinMenu.Conditional;

namespace Aquarius.Weixin.Entity.WeixinMenu
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
