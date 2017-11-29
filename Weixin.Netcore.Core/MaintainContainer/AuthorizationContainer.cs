using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.Authorization;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Model.Enums;
using Weixin.Netcore.Model.WeixinInterface;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// 认证容器
    /// </summary>
    public class AuthorizationContainer : IAuthorizationContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly IOpenIdAuthorization _openIdAuthorization;
        private readonly OAuthInterfaceCaller _oAuthInterfaceCaller;

        public AuthorizationContainer(ICache cache, IOpenIdAuthorization openIdAuthorization, OAuthInterfaceCaller oAuthInterfaceCaller)
        {
            _cache = cache;
            _openIdAuthorization = openIdAuthorization;
            _oAuthInterfaceCaller = oAuthInterfaceCaller;
        }
        #endregion

        public string GetTokenByCode(string code)
        {
            //通过code获取OpenId
            OpenId openId = _oAuthInterfaceCaller.GetOpenId(code);
            //通过OpenId换取token
            string token = _openIdAuthorization.GetToken(openId.openid);

            //保存Access Token
            _cache.Set($"{token}AccessToken", openId.access_token, TimeSpan.FromSeconds(openId.expires_in));
            //保存Refresh Token
            _cache.Set($"{token}RefreshToken", openId.refresh_token, TimeSpan.FromDays(30));

            return token;
        }

        public string GetTokenByUserId(string userId)
        {
            //通过OpenId/UnionId换取token
            return _openIdAuthorization.GetToken(userId);
        }

        public string GetUserId(string token)
        {
            //通过token换取OpenId/UnionId
            return _openIdAuthorization.GetOpenId(token);
        }

        public UserInfo GetUserInfo(string token, Language lang)
        {
            //通过token换取OpenId
            string openId = _openIdAuthorization.GetOpenId(token);
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
