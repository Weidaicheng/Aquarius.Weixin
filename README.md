# ♒Aquarius.Weixin♒

# [系列教程](https://www.jianshu.com/nb/25911549)

Quick Start
--------------------
1、在Web项目中执行 `Install-Package Aquarius.Weixin` 或 `dotnet add package Aquarius.Weixin` 安装Nuget包，建议使用最新版本以防止各种问题

2、在 [Startup.cs](https://github.com/Weidaicheng/Aquarius.Weixin/blob/master/src/Web/Aquarius.Weixin.Web/Startup.cs) 中添加 `Aquarius.Weixin.Core.Configuration.DependencyInjection` 引用

3、`ConfigurationServices` 配置

方式一：

在 `ConfigurationServices` 中添加 Aquarius.Weixin：

```
services.AddAquariusWeixin(opt =>
{
	opt.BaseSetting = new BaseSettings()
	{
		Debug = true,//调试模式
		IsRepetValid = false,//是否启用消息重复验证
		AppId = "appid",//appId
		AppSecret = "appsecret",//appSecret
		Token = "token",//token
		EncodingAESKey = "asekey",//AES Key，用于消息加密
		ApiKey = "apikey",//商户Api key
		CertPass = "certpass",//商户p12密码
		CertRoot = "certroot",//商户p12证书密码
		MchId = "mchid"//商户Id
	};
	opt.CacheType = CacheType.InMemory;//缓存模式，支持InMemory和Redis，此处采用InMemory
	opt.MsgMiddlewareType = MessageMiddlewareType.Plain;//消息中间件模式，支持Plain(明文)和Cipher(密文),此处采用明文
});
```

方式二：

添加配置文件 `AquariusWeixinOptions.json` 添加如下配置：

```
{
  "CacheType": "InMemory",
  "MsgMiddlewareType": "Plain",
  "BaseSetting": {
    "Debug": true,
    "IsRepetValid": false,
    "AppId": "appid",
    "AppSecret": "appsecret",
    "Token": "token",
    "EncodingAESKey": "asekey",
    "ApiKey": "apikey",
    "CertPass": "certpass",
    "CertRoot": "certroot",
    "MchId": "mchid"
  }
}

```

并在 `ConfigurationServices` 中添加 Aquarius.Weixin：

```
var options = new ConfigurationBuilder()
	.AddJsonFile("AquariusWeixinOptions.json")
	.Build();
services.AddAquariusWeixin(options);
```

快速使用可以指设置 BaseSetting 中的 `Debug(建议为true)`、`IsRepetValid(建议为false)`、`AppId`、`AppSecret`、`Token` 以及 `CacheType` 设置为 `InMemory`，`MsgMiddlewareType` 设置为 `Plain`，其他不进行配置或配置为空

4、使用示例 [WeixinController.cs](https://github.com/Weidaicheng/Aquarius.Weixin/blob/master/src/Web/Aquarius.Weixin.Web/Controllers/WeixinController.cs)

（1）网页授权

```
public IActionResult Authorization(string code)
{
	//通过code获取openId
	var openId = _authorizationContainer.GetOpenId(code);
	//通过openId获取userInfo
	var userInfo = _authorizationContainer.GetUserInfo(openId, Language.zh_CN);
	return Content($"your openId is {openId}, your nickname is {userInfo.nickname}");
}
```

网页授权回调链接的 Action 必须接收一个 `code` 参数或者在代码中从 request 中取 code

（2）菜单创建

```
public IActionResult CreateMenu()
{
	//创建菜单对象
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
						key = "按钮2"
					},
					new SingleViewButton("网页")
					{
						url = "http://yourdomain.com"
					}
				}
			}
		}
	};
	
	//获取AccessToken
	var accessToken = _accessTokenContainer.GetAccessToken();
	//创建菜单
	var result = _menuInterfaceCaller.CreateMenu(accessToken, menu.ToJson());
	return Content(result);
}
```

创建菜单样式如下：

![菜单样式](https://i.imgur.com/wpS1vPF.png)

（3）消息管理

```
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
				
				//解析消息
				IMessage message = MessageParser.ParseMessage(data);
				//处理消息，生成回复
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
```

将上述代码的 Action 配置为服务器链接，注意：该消息接收代码为建议模式，不建议增删

代码中默认的所有消息处理为返回 `success` 即不进行任何处理，需要编写一个消息处理类来进行实际的消息处理，以下以[点击菜单按钮返回文本](https://github.com/Weidaicheng/Aquarius.Weixin/blob/master/src/Web/Aquarius.Weixin.Web/MessageReply/ClickEventReplyTextHandler.cs)为例

新建 `ClickEventReplyTextHandler` 类，代码如下：

```
public class ClickEventReplyTextHandler : ClickEvtMessageHandlerBase
{
	private readonly IMessageReply<TextMessage> _messageReply;

	public ClickEventReplyTextHandler(IMessageReply<TextMessage> messageReply)
	{
		_messageReply = messageReply;
	}

	public override string ClickEventHandler(ClickEvtMessage message)
	{
		//组装文本消息
		var textMessage = new TextMessage(message)
		{
			CreateTime = UtilityHelper.GetTimeStamp(),
			Content = $"你点击了{message.EventKey}按钮"
		};

 		return _messageReply.CreateXml(textMessage);
	}
}
```

① `ClickEventReplyTextHandler` 类所继承的类即为要处理的接收到的消息类型，该示例为点击事件消息处理(ClickEvtMessageHandlerBase)

② `_messageReplay` 字段的类型为泛型 `IMessageReply<T>` 类型，类型 `T` 即为要返回的消息类型，该示例返回文本消息(TextMessage)

以上两点，该示例即对点击事件消息进行处理并返回文本消息

最后在 `ConfigureServices` 方法中的 `AddAquariusWeixin` 之后添加

```
services.AddScoped<ClickEvtMessageHandlerBase, ClickEventReplyTextHandler>();
```

即完成点击按钮返回文本消息

End
-------------
>支付教程不包含在 Quick Start 中，后续将会更新详细教程
>
>有问题请提 Issue 或与我联系
>
>微信：wdcdavyc（请备注：Github）。