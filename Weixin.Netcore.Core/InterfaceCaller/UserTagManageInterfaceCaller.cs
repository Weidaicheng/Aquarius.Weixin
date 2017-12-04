using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model.UserManage;
using Weixin.Netcore.Model.WeixinInterface;

namespace Weixin.Netcore.Core.InterfaceCaller
{
    /// <summary>
    /// 用户标签管理
    /// </summary>
    public class UserTagManageInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        #region const
        private const string WeixinUri = "https://api.weixin.qq.com";
        #endregion

        public UserTagManageInterfaceCaller(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
        }
        #endregion

        #region 标签管理
        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagName"></param>
        /// <returns>Tag Id</returns>
        public int CreateTag(string accessToken, string tagName)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/create", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new Tag()
            {
                tag = new TagDetail()
                {
                    name = tagName
                }
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            Tag tag = JsonConvert.DeserializeObject<Tag>(response.Content);
            return tag.tag.id;
        }

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Tags GetTags(string accessToken)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/get", Method.POST);
            request.AddQueryParameter("access_token", accessToken);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<Tags>(response.Content);
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="newTagName"></param>
        /// <returns></returns>
        public string UpdateTag(string accessToken, int tagId, string newTagName)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/update", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new Tag()
            {
                tag = new TagDetail()
                {
                    id = tagId,
                    name = newTagName
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

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public string DeleteTag(string accessToken, int tagId)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/delete", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new Tag()
            {
                tag = new TagDetail()
                {
                    id = tagId
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

        /// <summary>
        /// 获取标签下粉丝
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public UserList GetTagFans(string accessToken, int tagId, string nextOpenId = null)
        {
            IRestRequest request = new RestRequest("cgi-bin/user/tag/get", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                tagid = tagId,
                next_openid = nextOpenId
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<UserList>(response.Content);
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 给用户打标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public string Tagging(string accessToken, int tagId, params string[] openIds)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/members/batchtagging", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                tagid = tagId,
                openid_list = openIds
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
        /// 为用户取消标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public string UnTagging(string accessToken, int tagId, params string[] openIds)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/members/batchtagging", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                tagid = tagId,
                openid_list = openIds
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
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetUserTags(string accessToken, string openId)
        {
            IRestRequest request = new RestRequest("cgi-bin/tags/getidlist", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                openid = openId
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            var tagIds = JsonConvert.DeserializeObject<TagIds>(response.Content);
            return tagIds.tagid_list;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public UserList GetUserList(string accessToken, string nextOpenId = null)
        {
            IRestRequest request = new RestRequest("cgi-bin/user/get", Method.GET);
            request.AddQueryParameter("access_token", accessToken);
            if(!string.IsNullOrEmpty(nextOpenId))
            {
                request.AddQueryParameter("next_openid", nextOpenId);
            }

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<UserList>(response.Content);
        }

        /// <summary>
        /// 设置用户备注
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string Remark(string accessToken, string openId, string remark)
        {
            IRestRequest request = new RestRequest("cgi-bin/user/info/updateremark", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                openid = openId,
                remark = remark
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
