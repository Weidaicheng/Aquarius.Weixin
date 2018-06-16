using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Core.MaintainContainer;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.OAuth;
using Aquarius.Weixin.Entity.WeixinMenu;
using Aquarius.Weixin.Entity.WeixinMenu.Button;
using Aquarius.Weixin.Entity.WeixinMenu.Conditional;

namespace Aquarius.Weixin.Test
{
    [TestClass]
     public class MenuTest
    {
        [TestMethod]
        public void CreateMenuTest()
        {
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(weixinSetting);
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            var accessToken = container.GetAccessToken();
            string menu = @"
 {
     ""button"":[
     {
                ""type"":""click"",
          ""name"":""今日歌曲"",
          ""key"":""V1001_TODAY_MUSIC""
      },
      {
                ""name"":""菜单"",
           ""sub_button"":[
           {	
               ""type"":""view"",
               ""name"":""搜索"",
               ""url"":""http://www.soso.com/""
            },
            {
               ""type"":""click"",
               ""name"":""赞一下我们"",
               ""key"":""V1001_GOOD""
            }]
       }]
 }
";
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller();
            Console.WriteLine(menuInterfaceCaller.CreateMenu(accessToken, menu));
        }

        [TestMethod]
        public void GetMenuTest()
        {
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(weixinSetting);
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            var accessToken = container.GetAccessToken();
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller();
            Console.WriteLine(menuInterfaceCaller.GetMenu(accessToken));
        }

        [TestMethod]
        public void CreateConditionalMenuTest()
        {
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(weixinSetting);
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            var accessToken = container.GetAccessToken();
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller();
            IConditionalMenu conditionalMenu = new ConditionalMenu();
            conditionalMenu.button.Add(new SingleClickButton("Man")
            {
                key = "Conditional_Key_Man"
            });
            conditionalMenu.matchrule = new MatchRule()
            {
                sex = "1"
            };
            var menuId = menuInterfaceCaller.CreateConditionalMenu(accessToken, conditionalMenu.ToJson());
            Console.WriteLine(menuId.menuid);
        }

        [TestMethod]
        public void TryMatchConditionalMenuTest()
        {
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(weixinSetting);
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            var accessToken = container.GetAccessToken();
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller();
            Console.WriteLine(menuInterfaceCaller.TryMatchConditionalMenu(accessToken, "oGV7Kv0bgXvAUabe8sDopmKlzPNE"));
        }

        [TestMethod]
        public void DeleteConditionalMenuTest()
        {
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(weixinSetting);
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            AccessTokenContainer container = new AccessTokenContainer(cache, oAuthInterface);
            var accessToken = container.GetAccessToken();
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller();
            var menuId = new MenuId()
            {
                menuid = "415822427"
            };
            Console.WriteLine(menuInterfaceCaller.DeleteConditionalMenu(accessToken, menuId));
        }
    }
}
