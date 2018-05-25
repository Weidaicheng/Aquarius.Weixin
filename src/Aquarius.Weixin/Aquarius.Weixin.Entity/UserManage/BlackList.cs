namespace Aquarius.Weixin.Entity.UserManage
{
    /// <summary>
    /// 黑名单
    /// </summary>
    public class BlackList
    {
        public int total { get; set; }
        public int count { get; set; }
        public OpenIds data { get; set; }
        public string next_openid { get; set; }
    }
}
