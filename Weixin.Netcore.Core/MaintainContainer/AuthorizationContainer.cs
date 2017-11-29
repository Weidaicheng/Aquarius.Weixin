using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.Authorization;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Model;
using Weixin.Netcore.Model.Enums;
using Weixin.Netcore.Model.WeixinInterface;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// 认证容器
    /// </summary>
    public class AuthorizationContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly IAuthorization _authorization;
        private readonly OAuthInterfaceCaller _oAuthInterfaceCaller;
        private readonly BaseSettings _baseSettings;

        public AuthorizationContainer(ICache cache, IAuthorization authorization, OAuthInterfaceCaller oAuthInterfaceCaller,
            BaseSettings baseSettings)
        {
            _cache = cache;
            _authorization = authorization;
            _oAuthInterfaceCaller = oAuthInterfaceCaller;
            _baseSettings = baseSettings;
        }
        #endregion

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetToken(string code, Language lang)
        {
            //通过code获取OpenId
            OpenId openId = _oAuthInterfaceCaller.GetOpenId(code);
            string token;

            if(_baseSettings.AuthType == AuthType.OpenId)
            {
                //OpenId方式
                //通过OpenId换取token
                token = _authorization.GetToken(openId.openid, AuthType.OpenId);
            }
            else
            {
                //UnionId方式
                //获取UserInfo
                UserInfo userInfo = _oAuthInterfaceCaller.GetUserInfo(openId.access_token, openId.openid, lang);
                //通过UnionId换取token
                token = _authorization.GetToken(userInfo.unionid, AuthType.UnionId);
            }

            //保存Access Token
            _cache.Set($"{token}AccessToken", openId.access_token, TimeSpan.FromSeconds(openId.expires_in));
            //保存Refresh Token
            _cache.Set($"{token}RefreshToken", openId.refresh_token, TimeSpan.FromMinutes(30));

            return token;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="userId">OpenId/UnionId</param>
        /// <returns></returns>
        public string GetToken(string userId)
        {
            //通过OpenId/UnionId换取token
            return _authorization.GetToken(userId, _baseSettings.AuthType);
        }

        /// <summary>
        /// 获取OpenId/UnionId
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetUserId(string token)
        {
            //通过token换取OpenId/UnionId
            return _authorization.GetUserId(token, _baseSettings.AuthType);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string token, Language lang)
        {
            //通过token换取OpenId
            string openId = _authorization.GetUserId(token, _baseSettings.AuthType);
            //读取Access Token
            string accessToken = _cache.Get($"{token}AccessToken");
            if(!string.IsNullOrEmpty(accessToken))
            {
                //Access Token未过期
                //通过Access Token获取UserInfo
                return _oAuthInterfaceCaller.GetUserInfo(accessToken, openId, lang);
            }
            else
            {
                //Access Token已过期
                //读取Refresh Token
                string refreshToken = _cache.Get($"{token}RefreshToken");
                if(!string.IsNullOrWhiteSpace(refreshToken))
                {
                    //Refresh Token未过期
                    //通过Refresh Token刷新Access Token
                    OpenId open = _oAuthInterfaceCaller.RefreshToken(refreshToken);
                    //保存Access Token
                    _cache.Set($"{token}AccessToken", open.access_token, TimeSpan.FromSeconds(open.expires_in));
                    //保存Refresh Token
                    _cache.Set($"{token}RefreshToken", open.refresh_token, TimeSpan.FromMinutes(30));
                    //通过Access Token获取UserInfo
                    return _oAuthInterfaceCaller.GetUserInfo(open.access_token, openId, lang);
                }
                else
                {
                    //Refresh Token已过期
                    throw new RefreshTokenExpiredException("Refresh Token已过期");
                }
            }
        }
    }
}
