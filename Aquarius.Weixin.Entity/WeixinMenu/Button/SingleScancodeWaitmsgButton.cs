namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 调起扫一扫工具，然后弹出“消息接收中”提示框
    /// </summary>
    public class SingleScancodeWaitmsgButton : SingleButton
    {
        public SingleScancodeWaitmsgButton(string name) : base(name)
        {
            type = "scancode_waitmsg";
        }

        public SingleScancodeWaitmsgButton(string name, string key) : this(name)
        {
            this.key = key;
        }

        public string key { get; set; }
    }
}
