namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 单按钮
    /// </summary>
    public abstract class SingleButton : ButtonBase
    {
        public SingleButton(string name) : base(name)
        {
        }

        public string type { get; internal set; }
    }
}
