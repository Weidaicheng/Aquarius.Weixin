namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// 单击按钮
    /// </summary>
    public abstract class SingleButton : ButtonBase, IButton
    {
        public SingleButton(string name) : base(name)
        {
        }

        public string type { get; internal set; }
    }
}
