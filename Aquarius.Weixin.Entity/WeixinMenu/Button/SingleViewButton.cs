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

        public string url { get; set; }
    }
}
