﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Entity;
using Weixin.Netcore.Entity.Ticket;

namespace Weixin.Netcore.Core.InterfaceCaller
{
    /// <summary>
    /// 票据接口调用
    /// </summary>
    public class TicketInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public TicketInterfaceCaller(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
        }
        #endregion

        #region JS-SDK
        /// <summary>
        /// 获取JS-SDK票据
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        internal Ticket GetJsApiTicket(string accessToken)
        {
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/ticket/getticket", Method.GET);
            request.AddQueryParameter("access_token", accessToken);
            request.AddQueryParameter("type", "jsapi");

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<Ticket>(response.Content);
        }
        #endregion
    }
}
