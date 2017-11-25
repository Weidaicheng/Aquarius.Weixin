using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Weixin.Netcore.Utility;
using System.IO;
using Microsoft.Extensions.Logging;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Core.Message;
using Weixin.Netcore.Core.Message.Processer;

namespace Weixin.Netcore.Web.Controllers
{
    /// <summary>
    /// 微信业务控制器
    /// </summary>
    public class WeixinController : Controller
    {
        #region .ctor
        private readonly IConfiguration _configuration;
        private readonly ILogger<WeixinController> _logger;
        private readonly IMessageProcesser _processer;

        public WeixinController(IConfiguration configuration, ILogger<WeixinController> logger, IMessageProcesser processer)
        {
            _configuration = configuration;
            _logger = logger;
            _processer = processer;
        }
        #endregion

        public IActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            if(bool.Parse(_configuration["IsValidNow"]))//服务器配置
            {
                if(string.IsNullOrEmpty(echostr))
                {
                    return Content("echostr为空");
                }
                if(UtilityHelper.CheckSignature(signature, timestamp, nonce, _configuration["token"]))
                {
                    return Content(echostr);
                }
                else
                {
                    return Content("success");
                }
            }
            else//消息处理
            {
                if(UtilityHelper.CheckSignature(signature, timestamp, nonce, _configuration["token"], true))
                {
                    using (var sr = new StreamReader(Request.Body))
                    {
                        string data = sr.ReadToEnd();
                        _logger.LogInformation(data);

                        IMessage message = MessageParser.ParseMessage(data);
                        string reply = _processer.ProcessMessage(message);
                        return Content(reply);
                    }
                }
                else//消息真实性验证失败
                {
                    return Content("success");
                }
            }
        }
    }
}