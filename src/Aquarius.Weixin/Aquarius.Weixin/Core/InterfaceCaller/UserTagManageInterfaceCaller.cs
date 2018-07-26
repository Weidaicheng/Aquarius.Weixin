using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity.UserManage;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.Argument;
using System.Threading.Tasks;

namespace Aquarius.Weixin.Core.InterfaceCaller
{
    /// <summary>
    /// 用户标签管理
    /// </summary>
    public class UserTagManageInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;

        public UserTagManageInterfaceCaller()
        {
            _restClient = new RestClient(Uris.WxUri);
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
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(tagName))
            {
                throw new ArgumentException("标签名为空");
            }

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
        /// 创建标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public async Task<int> CreateTagAsync(string accessToken, string tagName)
        {
            return await Task.FromResult(CreateTag(
                accessToken,
                tagName));
        }

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public Tags GetTags(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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
        /// 获取公众号已创建的标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Tags> GetTagsAsync(string accessToken)
        {
            return await Task.FromResult(GetTags(accessToken));
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
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(newTagName))
            {
                throw new ArgumentException("标签名为空");
            }

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
        /// 编辑标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="newTagName"></param>
        /// <returns></returns>
        public async Task<string> UpdateTagAsync(string accessToken, int tagId, string newTagName)
        {
            return await Task.FromResult(UpdateTag(
                accessToken,
                tagId,
                newTagName));
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public string DeleteTag(string accessToken, int tagId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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
        /// 删除标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<string> DeleteTagAsync(string accessToken, int tagId)
        {
            return await Task.FromResult(DeleteTag(
                accessToken,
                tagId));
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
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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

        /// <summary>
        /// 获取标签下粉丝-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public async Task<UserList> GetTagFansAsync(string accessToken, int tagId, string nextOpenId = null)
        {
            return await Task.FromResult(GetTagFans(
                accessToken,
                tagId,
                nextOpenId));
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
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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
        /// 给用户打标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public async Task<string> TaggingAsync(string accessToken, int tagId, params string[] openIds)
        {
            return await Task.FromResult(Tagging(
                accessToken,
                tagId,
                openIds));
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
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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
        /// 为用户取消标签-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tagId"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public async Task<string> UnTaggingAsync(string accessToken, int tagId, params string[] openIds)
        {
            return await Task.FromResult(UnTagging(
                accessToken,
                tagId,
                openIds));
        }

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetUserTags(string accessToken, string openId)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if (string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("OpenId为空");
            }

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

            return JsonConvert.DeserializeObject<int[]>(response.Content.Replace("\"tagid_list\":", "").Replace("{", "").Replace("}", ""));
        }

        /// <summary>
        /// 获取用户身上的标签列表-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetUserTagsAsync(string accessToken, string openId)
        {
            return await Task.FromResult(GetUserTags(
                accessToken,
                openId));
        }

        /// <summary>
        /// 获取已关注用户列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public UserList GetUserList(string accessToken, string nextOpenId = null)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

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
        /// 获取已关注用户列表-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public async Task<UserList> GetUserListAsync(string accessToken, string nextOpenId = null)
        {
            return await Task.FromResult(GetUserList(
                accessToken,
                nextOpenId));
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
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }
            if(string.IsNullOrEmpty(openId))
            {
                throw new ArgumentException("OpenId为空");
            }
            if(string.IsNullOrEmpty(remark))
            {
                throw new ArgumentException("备注为空");
            }

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

        /// <summary>
        /// 设置用户备注-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<string> RemarkAsync(string accessToken, string openId, string remark)
        {
            return await Task.FromResult(Remark(
                accessToken,
                openId,
                remark));
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
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

            IRestRequest request = new RestRequest("cgi-bin/user/info", Method.GET);
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
        /// 获取用户信息-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetUserInfoAsync(string accessToken, string openId, Language lang)
        {
            return await Task.FromResult(GetUserInfo(
                accessToken,
                openId,
                lang));
        }

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        public UserInfoList BatchGetUserInfo(string accessToken, BatchGetUserInfoArg[] userList)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/user/info", Method.GET);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                user_list = userList
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<UserInfoList>(response.Content);
        }

        /// <summary>
        /// 批量获取用户信息-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        public async Task<UserInfoList> BatchGetUserInfoAsync(string accessToken, BatchGetUserInfoArg[] userList)
        {
            return await Task.FromResult(BatchGetUserInfo(
                accessToken,
                userList));
        }
        #endregion

        #region 黑名单管理
        /// <summary>
        /// 获取黑名单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginOpenId"></param>
        /// <returns></returns>
        public BlackList GetBlackList(string accessToken, string beginOpenId = null)
        {
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/tags/members/getblacklist", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
                begin_openid = beginOpenId
            });

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("errcode"))
            {
                var err = JsonConvert.DeserializeObject<Error>(response.Content);
                throw new WeixinInterfaceException(err.errmsg);
            }

            return JsonConvert.DeserializeObject<BlackList>(response.Content);
        }

        /// <summary>
        /// 获取黑名单-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginOpenId"></param>
        /// <returns></returns>
        public async Task<BlackList> GetBlackListAsync(string accessToken, string beginOpenId = null)
        {
            return await Task.FromResult(GetBlackList(
                accessToken,
                beginOpenId));
        }

        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public string BlackUser(string accessToken, params string[] openIds)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/tags/members/batchblacklist", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
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
        /// 拉黑用户-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public async Task<string> BlackUserAsync(string accessToken, params string[] openIds)
        {
            return await Task.FromResult(BlackUser(
                accessToken, openIds));
        }

        /// <summary>
        /// 取消拉黑用户
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public string UnBlackUser(string accessToken, params string[] openIds)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access Token为空");
            }

            IRestRequest request = new RestRequest("cgi-bin/tags/members/batchunblacklist", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            request.AddJsonBody(new
            {
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
        /// 取消拉黑用户-异步
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public async Task<string> UnBlackUserAsync(string accessToken, params string[] openIds)
        {
            return await Task.FromResult(UnBlackUser(
                accessToken,
                openIds));
        }
        #endregion
    }
}
