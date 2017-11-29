using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Core.InterfaceCaller;

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
    }
}
