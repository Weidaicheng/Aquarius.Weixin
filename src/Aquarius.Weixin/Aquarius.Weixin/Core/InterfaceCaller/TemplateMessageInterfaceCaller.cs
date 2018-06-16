using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using IndustryEnum = Aquarius.Weixin.Entity.Enums.Industry;
using IndustryClass = Aquarius.Weixin.Entity.TemplateMessage.Industry;
using Aquarius.Weixin.Entity.TemplateMessage;
using Aquarius.Weixin.Entity.Argument;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    /// <summary>
    /// 模板消息接口调用
    /// </summary>
    public class TemplateMessageInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public TemplateMessageInterfaceCaller()
        {
            _restClient = new RestClient(WeixinUri);
        }
        #endregion

        #region 行业
        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="industry1"></param>
        /// <param name="industry2"></param>
        /// <returns></returns>
        public string SetIndustry(string accessToken, IndustryEnum industry1, IndustryEnum industry2)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/template/api_set_industry", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                industry_id1 = industry1,
                industry_id2 = industry2
            });

            IRestResponse response = _restClient.Execute(request);

            Error err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            return err.errmsg;
        }

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IndustryClass GetIndustry(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/template/get_industry", Method.GET);
            request.AddQueryParameter("access_token", accessToken);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            IndustryClass industry = JsonConvert.DeserializeObject<IndustryClass>(response.Content);
            return industry;
        }
        #endregion

        #region 模板管理
        /// <summary>
        /// 获得模板Id
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shortTemplateId"></param>
        /// <returns></returns>
        public string GetTemplateId(string accessToken, string shortTemplateId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if(string.IsNullOrEmpty(shortTemplateId))
            {
                throw new ArgumentException("Short Template Id为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/template/api_add_template", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                template_id_short = shortTemplateId
            });

            IRestResponse response = _restClient.Execute(request);

            var err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            TemplateId templateId = JsonConvert.DeserializeObject<TemplateId>(response.Content);
            return templateId.template_id;
        }

        /// <summary>
        /// 获取所有模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public IEnumerable<Template> GetAllTemplate(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/template/get_all_private_template", Method.GET);
            request.AddQueryParameter("access_token", accessToken);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            TemplateList templates = JsonConvert.DeserializeObject<TemplateList>(response.Content);
            return templates.template_list;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public string DeleteTemplate(string accessToken, string templateId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if(string.IsNullOrEmpty(templateId))
            {
                throw new ArgumentException("Template Id为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/template/del_private_template", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                template_id = templateId
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

        #region 发送模板消息
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <param name="miniProgram"></param>
        /// <returns></returns>
        public long SendTMessage(string accessToken, string openId, string templateId, Dictionary<string, TemplateData> data, string url = null, MiniProgram miniProgram = null)
        {
            #region 参数验证
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if(string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("接受者OpenId为空");
            }
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException("模板Id为空");
            }
            if (miniProgram != null && string.IsNullOrEmpty(miniProgram.AppId))
            {
                throw new ArgumentException("小程序AppId为空");
            }
            if(miniProgram != null && string.IsNullOrEmpty(miniProgram.PagePath))
            {
                throw new ArgumentException("小程序跳转页面为空");
            }
            if(data == null || data.Count == 0)
            {
                throw new ArgumentException("Data为空");
            }
            foreach(var item in data)
            {
                if(string.IsNullOrEmpty(item.Key))
                {
                    throw new ArgumentException("Data中存在为空的值");
                }
                if(string.IsNullOrEmpty(item.Value.Value))
                {
                    throw new ArgumentException($"{item.Key}的值为空");
                }
            }
            #endregion

            #region 组装Json
            string jsonStr = string.Empty;
            jsonStr += $"{{";
            jsonStr += $"\"touser\": \"{openId}\",";
            jsonStr += $"\"template_id\": \"{templateId}\",";
            jsonStr += $"\"url\": \"{url ?? ""}\",";
            if (miniProgram != null)
            {
                jsonStr += $"\"miniprogram\": {{";
                jsonStr += $"\"appid\": \"{(miniProgram == null ? "" : miniProgram.AppId)}\",";
                jsonStr += $"\"pagepath\": \"{(miniProgram == null ? "" : miniProgram.PagePath)}\"";
                jsonStr += $"}},";
            }
            jsonStr += $"\"data\": {{";
            foreach (var item in data)
            {
                jsonStr += $"\"{item.Key}\": {{";
                jsonStr += $"\"value\": \"{item.Value.Value}\",";
                jsonStr += $"\"color\": \"{item.Value.Color ?? ""}\"";
                jsonStr += $"}},";
            }
            if (jsonStr.EndsWith(","))
            {
                jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            }
            jsonStr += $"}}";
            jsonStr += $"}}";
            #endregion

            IRestRequest request = new RestRequest("cgi-bin/message/template/send", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddQueryParameter("access_token", accessToken);
            request.AddParameter("application/json", jsonStr, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            Error err = JsonConvert.DeserializeObject<Error>(response.Content);
            if (err.errcode != 0)
            {
                throw new WeixinInterfaceException(err.errmsg);
            }

            TMessageId tMessageId = JsonConvert.DeserializeObject<TMessageId>(response.Content);
            return tMessageId.msgid;
        }
        #endregion
    }
}
