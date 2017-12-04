namespace Weixin.Netcore.Model.UserTag
{
    /// <summary>
    /// 标签下粉丝
    /// </summary>
    public class TagFans
    {
        public int count { get; set; }
        public OpenIds data { get; set; }
        public string next_openid { get; set; }
    }

    public class OpenIds
    {
        public string[] openid { get; set; }
    }
}
