using Newtonsoft.Json;
using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMenu.Button;

namespace Aquarius.Weixin.Entity.WeixinMenu
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : IMenu
    {
        public Menu()
        {
            button = new List<IButton>();
        }

        public List<IButton> button { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
