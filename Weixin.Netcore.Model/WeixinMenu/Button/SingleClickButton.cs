namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// Click按钮
    /// </summary>
    public class SingleClickButton : SingleButton, IButton
    {
        public SingleClickButton(string name) : base(name)
        {
            type = "click";
        }

        public string key { get; set; }
    }
}
