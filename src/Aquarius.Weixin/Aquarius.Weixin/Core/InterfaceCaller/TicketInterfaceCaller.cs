using Newtonsoft.Json;
using RestSharp;
using System;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Ticket;
using Aquarius.Weixin.Entity.Enums;
using System.Threading.Tasks;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    /// <summary>
    /// 票据接口调用
    /// </summary>
    public class TicketInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        public TicketInterfaceCaller()
        {
            _restClient = new RestClient(Uris.WxUri);
        }
        #endregion

        #region JS-SDK
        /// <summary>
        /// 获取JS-SDK票据
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Ticket GetJsApiTicket(string accessToken)
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

        /// <summary>
        /// 获取JS-SDK票据-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Ticket> GetJsApiTicketAsync(string accessToken)
        {
            return await Task.FromResult(GetJsApiTicket(accessToken));
        }
        #endregion
    }
}
