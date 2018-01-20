using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Entity.Enums;
using Weixin.Netcore.Entity.OAuth;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// 认证容器
    /// </summary>
    public class AuthorizationContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly OAuthInterfaceCaller _oAuthInterfaceCaller;

        public AuthorizationContainer(ICache cache, OAuthInterfaceCaller oAuthInterfaceCaller)
        {
            _cache = cache;
            _oAuthInterfaceCaller = oAuthInterfaceCaller;
        }
        #endregion

        public string GetOpenId(string code)
        {
            //通过code获取OpenId
            OpenId openId = _oAuthInterfaceCaller.GetOpenId(code);

            //保存Access Token
            _cache.Set($"{openId.openid}AccessToken", openId.access_token, TimeSpan.FromSeconds(openId.expires_in));
            //保存Refresh Token
            _cache.Set($"{openId.openid}RefreshToken", openId.refresh_token, TimeSpan.FromDays(30));

            return openId.openid;
        }

        public UserInfo GetUserInfo(string openId, Language lang)
        {
            //读取Access Token
            string accessToken = _cache.Get($"{openId}AccessToken");
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
                string refreshToken = _cache.Get($"{openId}RefreshToken");
                if(!string.IsNullOrWhiteSpace(refreshToken))
                {
                    //Refresh Token未过期
                    //通过Refresh Token刷新Access Token
                    OpenId open = _oAuthInterfaceCaller.RefreshToken(refreshToken);
                    //保存Access Token
                    _cache.Set($"{openId}AccessToken", open.access_token, TimeSpan.FromSeconds(open.expires_in));
                    //保存Refresh Token
                    _cache.Set($"{openId}RefreshToken", open.refresh_token, TimeSpan.FromMinutes(30));
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
