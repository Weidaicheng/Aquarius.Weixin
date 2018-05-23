using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Aquarius.Weixin.Entity.Pay;
using Aquarius.Weixin.Entity.WeixinMenu;
using Aquarius.Weixin.Entity.WeixinMenu.Button;

namespace Aquarius.Weixin.Test
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void MenuModelTest()
        {
            IMenu menu = new Menu();

            menu.button.Add(new SingleClickButton("今日歌曲")
            {
                key = "V1001_TODAY_MUSIC"
            });
            menu.button.Add(new SubButton("菜单")
            {
                sub_button = new List<SingleButton>()
                {
                    new SingleViewButton("搜索")
                    {
                        url = "www.soso.com"
                    },
                    new SingleProgramButton("wxa")
                    {
                        url = "http://mp.weixin.com",
                        appid = "wx286b93c14bbf93aa",
                        pagepath = "pages/lunar/index"
                    },
                    new SingleClickButton("赞一下我们")
                    {
                        key = "V1001_GOOD"
                    }
                }
            });

            Console.WriteLine(menu.ToJson());
        }

        [TestMethod]
        public void SceneInfoToStringTest()
        {
            SceneInfo sceneInfo = new SceneInfo()
            {
                id = "1",
                address = "1",
                area_code = "1",
                name = "1"
            };

            string assert = "{\"id\":\"1\",\"name\":\"1\",\"area_code\":\"1\",\"address\":\"1\"}";
            Assert.AreEqual(assert, sceneInfo.ToString());
        }
    }
}
