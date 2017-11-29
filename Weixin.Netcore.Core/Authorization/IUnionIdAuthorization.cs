namespace Weixin.Netcore.Core.Authorization
{
    /// <summary>
    /// 认证-UnionId
    /// </summary>
    public interface IUnionIdAuthorization
    {
        /// <summary>
        /// 通过UnionId换取Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetToken(string id);

        /// <summary>
        /// 通过Token换取UnionId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetUnionId(string token);
    }
}
