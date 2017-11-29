using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.InterfaceCaller;

namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// Access Token容器
    /// </summary>
    public class AccessTokenContainer
    {
        private readonly ICache _cache;
        private readonly OAuthInterfaceCaller _oAuthInterfaceCaller;

        public AccessTokenContainer(ICache cache, OAuthInterfaceCaller oAuthInterfaceCaller)
        {
            _cache = cache;
            _oAuthInterfaceCaller = oAuthInterfaceCaller;
        }

        public string GetAccessToken()
        {
            string token = _cache.Get("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                var accessToken = _oAuthInterfaceCaller.GetAccessToken();
                _cache.Set("AccessToken", accessToken.access_token, TimeSpan.FromSeconds(accessToken.expires_in));
                token = accessToken.access_token;
            }

            return token;
        }
    }
}
