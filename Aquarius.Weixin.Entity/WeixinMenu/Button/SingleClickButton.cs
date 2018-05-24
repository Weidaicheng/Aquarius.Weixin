namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// Click按钮
    /// </summary>
    public class SingleClickButton : SingleButton
    {
        public SingleClickButton(string name) : base(name)
        {
            type = "click";
        }

        public SingleClickButton(string name, string key) : this(name)
        {
            this.key = key;
        }

        public string key { get; set; }
    }
}
