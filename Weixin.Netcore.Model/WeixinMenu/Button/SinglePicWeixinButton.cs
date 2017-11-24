namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// 调起微信相册按钮
    /// </summary>
    public class SinglePicWeixinButton : SingleButton, IButton
    {
        public SinglePicWeixinButton(string name) : base(name)
        {
            type = "pic_weixin";
        }

        public string key { get; set; }
    }
}
