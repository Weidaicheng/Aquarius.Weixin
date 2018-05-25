namespace Aquarius.Weixin.Entity.OAuth
{
    /// <summary>
    /// Access Token 返回结果类
    /// </summary>
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
