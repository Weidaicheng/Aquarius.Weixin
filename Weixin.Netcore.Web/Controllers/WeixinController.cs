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
using Weixin.Netcore.Core.Middleware;

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
        private readonly IMessageMiddleware _messageMiddleware;

        public WeixinController(IConfiguration configuration, ILogger<WeixinController> logger,
            IMessageProcesser processer, IMessageMiddleware messageMiddleware)
        {
            _configuration = configuration;
            _logger = logger;
            _processer = processer;
            _messageMiddleware = messageMiddleware;
        }
        #endregion

        public async Task<IActionResult> Index(string signature, string timestamp, string nonce, string echostr, string encrypt_type, string msg_signature)
        {
            try
            {
                if(!string.IsNullOrEmpty(echostr))
                {
                    //服务器认证
                    if (UtilityHelper.VerifySignature(timestamp, nonce, _configuration["Token"], signature))
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
    }
}