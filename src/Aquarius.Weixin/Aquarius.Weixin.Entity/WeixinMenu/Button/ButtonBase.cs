namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 按钮基类
    /// </summary>
    public abstract class ButtonBase : IButton
    {
        public ButtonBase(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
}
