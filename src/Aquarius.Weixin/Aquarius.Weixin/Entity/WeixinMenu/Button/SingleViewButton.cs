namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// View按钮
    /// </summary>
    public class SingleViewButton : SingleButton
    {
        public SingleViewButton(string name) : base(name)
        {
            type = "view";
        }

        public SingleViewButton(string name, string url) : this(name)
        {
            this.url = url;
        }

        public string url { get; set; }
    }
}
