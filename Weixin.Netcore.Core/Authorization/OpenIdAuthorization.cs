namespace Weixin.Netcore.Core.Authorization
{
    /// <summary>
    /// 认证-OpenId
    /// todo:当前未进行任何转换与处理
    /// </summary>
    public class OpenIdAuthorization : IOpenIdAuthorization
    {
        public string GetOpenId(string token)
        {
            return token;
        }

        public string GetToken(string openId)
        {
            return openId;
        }
    }
}
