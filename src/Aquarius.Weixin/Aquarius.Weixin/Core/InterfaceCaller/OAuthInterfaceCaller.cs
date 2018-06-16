using Newtonsoft.Json;
using RestSharp;
using System;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.OAuth;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    /// <summary>
    /// 微信接口调用
    /// </summary>
    public class OAuthInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;
        private readonly BaseSettings _weixinSetting;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public OAuthInterfaceCaller(BaseSettings weixinSetting)
        {
            _restClient = new RestClient(WeixinUri);
            _weixinSetting = weixinSetting;
        }
        #endregion

        /// <summary>
        /// 获取普通AccessToken
        /// </summary>
        /// <returns></returns>
        public AccessToken GetAccessToken()
        {
            IRestRequest request = new RestRequest("cgi-bin/token", Method.GET);
            request.AddQueryParameter("grant_type", "client_credential");
            request.AddQueryParameter("appid", _weixinSetting.AppId);
            request.AddQueryParameter("secret", _weixinSetting.AppSecret);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<AccessToken>(response.Content);
        }

        /// <summary>
        /// 通过Code获取OpenId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public OpenId GetOpenId(string code)
        {
            if(string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("code为空");
            }

            IRestRequest request = new RestRequest("sns/oauth2/access_token", Method.GET);
            request.AddQueryParameter("appid", _weixinSetting.AppId);
            request.AddQueryParameter("secret", _weixinSetting.AppSecret);
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("grant_type", "authorization_code");

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<OpenId>(response.Content);
        }

        /// <summary>
        /// 刷新网页授权access_token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public OpenId RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("Refresh Token为空");
            }

            IRestRequest request = new RestRequest("sns/oauth2/refresh_token", Method.GET);
            request.AddQueryParameter("appid", _weixinSetting.AppId);
            request.AddQueryParameter("grent_type", "refresh_token");
            request.AddQueryParameter("refresh_token", refreshToken);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<OpenId>(response.Content);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken">网页授权Token</param>
        /// <param name="openId"></param>
        /// <param name="lang">语言</param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string accessToken, string openId, Language lang)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("OpenId为空");
            }

            IRestRequest request = new RestRequest("sns/userinfo", Method.GET);
            request.AddQueryParameter("access_token", accessToken);
            request.AddQueryParameter("openid", openId);
            request.AddQueryParameter("lang", lang.ToString());

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<UserInfo>(response.Content);
        }

        /// <summary>
        /// 检查网页授权AccessToken是否有效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="errMsg">错误消息</param>
        /// <returns></returns>
        public bool CheckToken(string accessToken, string openId, out string errMsg)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if(string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("OpenId为空");
            }

            IRestRequest request = new RestRequest("sns/auth", Method.GET);
            request.AddQueryParameter("access_token", accessToken);
            request.AddQueryParameter("openid", openId);

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            errMsg = err.errmsg;
            if (err.errcode == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
