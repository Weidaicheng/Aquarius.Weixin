using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Aquarius.Weixin.Utility;
using System.IO;
using Microsoft.Extensions.Logging;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Core.Message;
using Aquarius.Weixin.Core.Message.Processer;
using Aquarius.Weixin.Core.Middleware;
using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.MaintainContainer;
using Aquarius.Weixin.Entity.WeixinMenu;
using Aquarius.Weixin.Entity.WeixinMenu.Button;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Web.Controllers
{
    /// <summary>
    /// 微信业务控制器
    /// </summary>
    public class WeixinController : Controller
    {
        #region .ctor
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeixinController> _logger;
        private readonly MessageProcesser _processer;
        private readonly IMessageMiddleware _messageMiddleware;
        private readonly Verifyer _verifyer;
        private readonly AuthorizationContainer _authorizationContainer;
        private readonly AccessTokenContainer _accessTokenContainer;
        private readonly MenuInterfaceCaller _menuInterfaceCaller;

        public WeixinController(IConfiguration configuration, ILogger<WeixinController> logger,
            MessageProcesser processer, IMessageMiddleware messageMiddleware, Verifyer verifyer,
            AuthorizationContainer authorizationContainer, AccessTokenContainer accessTokenContainer,
            MenuInterfaceCaller menuInterfaceCaller)
        {
            _configuration = configuration;
            _logger = logger;
            _processer = processer;
            _messageMiddleware = messageMiddleware;
            _verifyer = verifyer;
            _authorizationContainer = authorizationContainer;
            _accessTokenContainer = accessTokenContainer;
            _menuInterfaceCaller = menuInterfaceCaller;
        }
        #endregion

        /// <summary>
        /// 服务器配置
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <param name="encrypt_type"></param>
        /// <param name="msg_signature"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string signature, string timestamp, string nonce, string echostr, string encrypt_type, string msg_signature)
        {
            try
            {
                if(!string.IsNullOrEmpty(echostr))
                {
                    //服务器认证
                    if (_verifyer.VerifySignature(signature, timestamp, nonce, "token"))
                    {
                        return Content(echostr);
                    }
                    else
                    {
                        return Content("success");
                    }
                }
                else
                {
                    //消息接收
                    using (var sr = new StreamReader(Request.Body))
                    {
                        string data = await sr.ReadToEndAsync();
                        _logger.LogInformation(data);

                        //接收消息中间处理
                        data = _messageMiddleware.ReceiveMessageMiddle(signature, msg_signature, timestamp, nonce, data);

                        IMessage message = MessageParser.ParseMessage(data);
                        string reply = _processer.ProcessMessage(message);

                        //回复消息中间处理
                        reply = _messageMiddleware.ReplyMessageMiddle(reply);

                        return Content(reply);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error", ex);
                return Content("success");
            }
        }

        /// <summary>
        /// 网页授权
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult Authorization(string code)
        {
            var userInfo = _authorizationContainer.GetUserInfoByCode(code);
            return Content($"your openId is {userInfo.openid}, your nickname is {userInfo.nickname}");
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateMenu()
        {
            var menu = new Menu()
            {
                button = new List<IButton>()
                {
                    new SingleClickButton("按钮1")
                    {
                        key = "Button1"
                    },
                    new SubButton("二级菜单")
                    {
                        sub_button = new List<SingleButton>()
                        {
                            new SingleClickButton("按钮2")
                            {
                                key = "Button2"
                            },
                            new SingleViewButton("网页")
                            {
                                url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=&redirect_uri=http://yourdomain.com/Weixin/Authorization&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect"
                            }
                        }
                    }
                }
            };
            var accessToken = _accessTokenContainer.GetAccessToken();
            return Content(_menuInterfaceCaller.CreateMenu(accessToken, menu.ToJson()));
        }
    }
}