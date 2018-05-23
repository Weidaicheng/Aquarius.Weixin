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

        public WeixinController(IConfiguration configuration, ILogger<WeixinController> logger,
            MessageProcesser processer, IMessageMiddleware messageMiddleware, Verifyer verifyer)
        {
            _configuration = configuration;
            _logger = logger;
            _processer = processer;
            _messageMiddleware = messageMiddleware;
            _verifyer = verifyer;
        }
        #endregion

        public async Task<IActionResult> Index(string signature, string timestamp, string nonce, string echostr, string encrypt_type, string msg_signature)
        {
            try
            {
                if(!string.IsNullOrEmpty(echostr))
                {
                    //服务器认证
                    if (_verifyer.VerifySignature(signature, timestamp, nonce, _configuration["Token"]))
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