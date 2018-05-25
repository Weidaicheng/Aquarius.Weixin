namespace Aquarius.Weixin.Entity.WeixinMenu.Button
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

        public SingleProgramButton(string name, string url, string appid, string pagepath) : this(name)
        {
            this.url = url;
            this.appid = appid;
            this.pagepath = pagepath;
        }

        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }
}
