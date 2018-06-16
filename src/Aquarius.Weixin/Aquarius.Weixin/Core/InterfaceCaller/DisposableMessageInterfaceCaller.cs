using Newtonsoft.Json;
using RestSharp;
using System;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Argument;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    /// <summary>
    /// 一次性订阅消息接口调用
    /// </summary>
    public class DisposableMessageInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public DisposableMessageInterfaceCaller()
        {
            _restClient = new RestClient(WeixinUri);
        }
        #endregion

        #region 发送一次性订阅消息
        /// <summary>
        /// 发送一次性订阅消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="scene"></param>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <param name="url"></param>
        /// <param name="miniProgram"></param>
        /// <returns></returns>
        public string SendDisposableMessage(string accessToken, string openId, string templateId, int scene, string title, string value, string color = null, string url = null, MiniProgram miniProgram = null)
        {
            #region 参数验证
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("接受者OpenId为空");
            }
            if(string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException("模板Id为空");
            }
            if (miniProgram != null && string.IsNullOrEmpty(miniProgram.AppId))
            {
                throw new ArgumentException("小程序AppId为空");
            }
            if (miniProgram != null && string.IsNullOrEmpty(miniProgram.PagePath))
            {
                throw new ArgumentException("小程序跳转页面为空");
            }
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value为空");
            }
            if(string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title为空");
            }
            #endregion

            IRestRequest request = new RestRequest("cgi-bin/message/template/subscribe", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                touser = openId,
                template_id = templateId,
                url = url,
                miniProgram = miniProgram == null ? null : miniProgram,
                scene = scene,
                title = title,
                data = new
                {
                    content = new
                    {
                        value = value,
                        color = color
                    }
                }
            });

            IRestResponse response = _restClient.Execute(request);

            Error err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return err.errmsg;
        }
        #endregion
    }
}
