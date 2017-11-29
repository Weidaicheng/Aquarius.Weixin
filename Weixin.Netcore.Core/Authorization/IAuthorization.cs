using Weixin.Netcore.Model.Enums;

namespace Weixin.Netcore.Core.Authorization
{
    /// <summary>
    /// 认证
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// 通过OpenId/UnionId换取Token
        /// </summary>
        /// <param name="id">OpenId/UnionId</param>
        /// <returns></returns>
        string GetToken(string id, AuthType type);

        /// <summary>
        /// 通过Token换取OpenId/UnionId
        /// </summary>
        /// <param name="token"></param>
        /// <returns>OpenId/UnionId</returns>
        string GetUserId(string token, AuthType type);
    }
}
