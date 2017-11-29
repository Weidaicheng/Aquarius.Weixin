using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Model.WeixinInterface;
using Weixin.Netcore.Model.WeixinMenu;
using Weixin.Netcore.Model.WeixinMenu.Button;
using Weixin.Netcore.Model.WeixinMenu.Conditional;

namespace Weixin.Netcore.Test
{
    [TestClass]
     public class MenuTest
    {
        [TestMethod]
        public void CreateMenuTest()
        {
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient());
            var accessToken = oAuthInterface.GetAccessToken("wx6eff55d0d76e210f", "60ab768429e8fc6b86abaa9cfd1c6565");
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
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller(new RestClient());
            Console.WriteLine(menuInterfaceCaller.CreateMenu(accessToken.access_token, menu));
        }

        [TestMethod]
        public void GetMenuTest()
        {
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient());
            var accessToken = oAuthInterface.GetAccessToken("wx6eff55d0d76e210f", "60ab768429e8fc6b86abaa9cfd1c6565");
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller(new RestClient());
            Console.WriteLine(menuInterfaceCaller.GetMenu(accessToken.access_token));
        }

        [TestMethod]
        public void CreateConditionalMenuTest()
        {
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient());
            var accessToken = oAuthInterface.GetAccessToken("wx6eff55d0d76e210f", "60ab768429e8fc6b86abaa9cfd1c6565");
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller(new RestClient());
            IConditionalMenu conditionalMenu = new ConditionalMenu();
            conditionalMenu.button.Add(new SingleClickButton("Man")
            {
                key = "Conditional_Key_Man"
            });
            conditionalMenu.matchrule = new MatchRule()
            {
                sex = "1"
            };
            var menuId = menuInterfaceCaller.CreateConditionalMenu(accessToken.access_token, conditionalMenu.ToJson());
            Console.WriteLine(menuId.menuid);
        }

        [TestMethod]
        public void TryMatchConditionalMenuTest()
        {
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient());
            var accessToken = oAuthInterface.GetAccessToken("wx6eff55d0d76e210f", "60ab768429e8fc6b86abaa9cfd1c6565");
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller(new RestClient());
            Console.WriteLine(menuInterfaceCaller.TryMatchConditionalMenu(accessToken.access_token, "oGV7Kv0bgXvAUabe8sDopmKlzPNE"));
        }

        [TestMethod]
        public void DeleteConditionalMenuTest()
        {
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient());
            var accessToken = oAuthInterface.GetAccessToken("wx6eff55d0d76e210f", "60ab768429e8fc6b86abaa9cfd1c6565");
            MenuInterfaceCaller menuInterfaceCaller = new MenuInterfaceCaller(new RestClient());
            var menuId = new MenuId()
            {
                menuid = "415822427"
            };
            Console.WriteLine(menuInterfaceCaller.DeleteConditionalMenu(accessToken.access_token, menuId));
        }
    }
}
