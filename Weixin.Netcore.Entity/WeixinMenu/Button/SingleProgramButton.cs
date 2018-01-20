namespace Weixin.Netcore.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 小程序按钮
    /// </summary>
    public class SingleProgramButton : SingleButton
    {
        public SingleProgramButton(string name) : base(name)
        {
            type = "miniprogram";
        }

        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }
}
