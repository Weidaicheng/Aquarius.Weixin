namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// View按钮
    /// </summary>
    public class SingleViewButton : SingleButton, IButton
    {
        public SingleViewButton(string name) : base(name)
        {
            type = "view";
        }

        public string url { get; set; }
    }
}
