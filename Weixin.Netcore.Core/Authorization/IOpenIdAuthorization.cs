namespace Weixin.Netcore.Core.Authorization
{
    /// <summary>
    /// 认证-OpenId
    /// </summary>
    public interface IOpenIdAuthorization
    {
        /// <summary>
        /// 通过OpenId换取Token
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        string GetToken(string openId);

        /// <summary>
        /// 通过Token换取OpenId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetOpenId(string token);
    }
}
