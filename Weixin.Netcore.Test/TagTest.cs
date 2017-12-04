using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Core.MaintainContainer;
using Weixin.Netcore.Model;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void CreateTagTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.CreateTag(accessToken, "Tag1"));
        }

        [TestMethod]
        public void GetTagTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            var tags = tagManageInterfaceCaller.GetTags(accessToken);
            Console.WriteLine(JsonConvert.SerializeObject(tags));
        }

        [TestMethod]
        public void UpdateTagTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.UpdateTag(accessToken, 100, "新名字"));
        }

        [TestMethod]
        public void TaggingTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.Tagging(accessToken, 100, "oGV7Kv0bgXvAUabe8sDopmKlzPNE", "oGV7Kv5pT6m1P5zHDU3sHg4FT1JA"));
        }

        [TestMethod]
        public void UnTaggingTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.UnTagging(accessToken, 100, "oGV7Kv5pT6m1P5zHDU3sHg4FT1JA"));
        }

        [TestMethod]
        public void GetTagFansTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            var tagFans = tagManageInterfaceCaller.GetTagFans(accessToken, 100);
            Console.WriteLine(JsonConvert.SerializeObject(tagFans));
        }

        [TestMethod]
        public void GetUserTagsTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            var tags = tagManageInterfaceCaller.GetUserTags(accessToken, "oGV7Kv0bgXvAUabe8sDopmKlzPNE");
            Console.WriteLine(JsonConvert.SerializeObject(tags));
        }

        [TestMethod]
        public void DeleteTagTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.DeleteTag(accessToken, 100));
        }

        [TestMethod]
        public void GetUserListTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            var userList = tagManageInterfaceCaller.GetUserList(accessToken);
            Console.WriteLine(JsonConvert.SerializeObject(userList));
        }

        [TestMethod]
        public void RemarkTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            Console.WriteLine(tagManageInterfaceCaller.Remark(accessToken, "oGV7Kv0bgXvAUabe8sDopmKlzPNE", "备注"));
        }

        [TestMethod]
        public void UserInfoTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            string accessToken = container.GetAccessToken();
            UserTagManageInterfaceCaller tagManageInterfaceCaller = new UserTagManageInterfaceCaller(new RestClient());
            var userInfo = tagManageInterfaceCaller.GetUserInfo(accessToken, "oGV7Kv0bgXvAUabe8sDopmKlzPNE", Model.Enums.Language.zh_CN);
            Console.WriteLine(JsonConvert.SerializeObject(userInfo));
        }
    }
}
