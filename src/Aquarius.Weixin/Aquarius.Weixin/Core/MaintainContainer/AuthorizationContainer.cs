using System;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.OAuth;

namespace Aquarius.Weixin.Core.MaintainContainer
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

        /// <summary>
        /// 获取OpenId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetOpenId(string code)
        {
            //通过code获取OpenId
            OpenId openId = _oAuthInterfaceCaller.GetOpenId(code);

            //保存用户Access Token
            _cache.Set($"{openId.openid}AccessToken", openId.access_token, TimeSpan.FromSeconds(openId.expires_in));
            //保存Refresh Token
            _cache.Set($"{openId.openid}RefreshToken", openId.refresh_token, TimeSpan.FromDays(30));

            return openId.openid;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string openId, Language lang)
        {
            //读取用户Access Token
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

        /// <summary>
        /// 通过code换取<see cref="UserInfo"/>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lang">语言，默认为<see cref="Language.zh_CN"/></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByCode(string code, Language lang = Language.zh_CN)
        {
            var openId = GetOpenId(code);
            return GetUserInfo(openId, lang);
        }
    }
}
