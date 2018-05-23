namespace Aquarius.Weixin.Entity.OAuth
{
    /// <summary>
    /// OpenId返回结果类
    /// </summary>
    public class OpenId
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
    }
}
