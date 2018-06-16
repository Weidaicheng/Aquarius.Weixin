using Newtonsoft.Json;
using RestSharp;
using System;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.WeixinMenu;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    public class MenuInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public MenuInterfaceCaller()
        {
            _restClient = new RestClient(WeixinUri);
        }
        #endregion

        #region 普通菜单
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

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string GetMenu(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/get", Method.POST);
            request.AddQueryParameter("access_token", accessToken);

            IRestResponse response = _restClient.Execute(request);

            return response.Content;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DeleteMenu(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/delete", Method.POST);
            request.AddQueryParameter("access_token", accessToken);

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return err.errmsg;
        }
        #endregion

        #region 个性化菜单
        /// <summary>
        /// 创建个性菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="menuJson"></param>
        /// <returns></returns>
        public MenuId CreateConditionalMenu(string accessToken, string menuJson)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(menuJson))
            {
                throw new ArgumentException("menuJson为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/addconditional", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddParameter("application/json", menuJson, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            if(response.Content.Contains("menuid"))
            {
                var menuId = JsonConvert.DeserializeObject<MenuId>(response.Content);
                return menuId;
            }
            else
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string DeleteConditionalMenu(string accessToken, MenuId menuId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/delconditional", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(menuId);

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return err.errmsg;
        }

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string TryMatchConditionalMenu(string accessToken, string userId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest($"cgi-bin/menu/trymatch", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                user_id = userId
            });

            IRestResponse response = _restClient.Execute(request);
            if(response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return response.Content;
        }
        #endregion
    }
}
