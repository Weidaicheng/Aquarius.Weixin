using Weixin.Netcore.Model.Enums;
using Weixin.Netcore.Model.WeixinInterface;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// 认证容器
    /// </summary>
    public interface IAuthorizationContainer
    {
        /// <summary>
        /// 通过code获取Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string GetTokenByCode(string code);

        /// <summary>
        /// 通过OpenId/UnionId获取Token
        /// </summary>
        /// <param name="userId">OpenId/UnionId</param>
        /// <returns></returns>
        string GetTokenByUserId(string userId);

        /// <summary>
        /// 获取OpenId/UnionId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetUserId(string token);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="lang">语言</param>
        /// <returns></returns>
        UserInfo GetUserInfo(string token, Language lang);
    }
}
