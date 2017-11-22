using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model.WeixinInterface;

namespace Weixin.Netcore.Core.InterfaceCaller
{
    public class MenuInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public MenuInterfaceCaller(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
        }
        #endregion

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="menuJson"></param>
        /// <returns></returns>
        public string CreateMenu(string accessToken, string menuJson)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(menuJson))
            {
                throw new ArgumentException("menuJson为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/create", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddParameter("application/json", menuJson, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return err.errmsg;
        }
    }
}
