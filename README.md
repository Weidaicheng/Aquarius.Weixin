# Weixin.Netcore
由于工作需要在公司做公众号，正巧自己也在学习.net core，所以就把自己总结的一些方法（解决方案）放到了该项目中。如有错误之处，还请指正。[Issues][Issues]

## 解决方案介绍
>+[Weixin.Netcore.Cache][Cache]:缓存的实现，使用了asp.net core中的InMemory缓存以及Redis缓存。

>+[Weixin.Netcore.Core][Core]:整个公众号的核心方法。查看[详细介绍][CoreDetail]。

>+[Weixin.Netcore.Entity][Entity]:实体类定义。

>+[Weixin.Netcore.Extensions][Extensions]:用于编写具体消息处理业务方法的项目，*其实我自己也觉着这个项目名字起得不好*，在消息处理中会有[详细介绍][MessageDetail]。

>+[Weixin.Netcore.Test][Test]:单元测试项目，里边单元测试写的很丑陋，最近也在学习单元测试。

>+[Weixin.Netcore.Utility][Utility]:通用帮助方法类，静态方法。

>+[Weixin.Netcore.Web][Web]:公众号的web项目，可以认为这是一个Sample。

## 快速开始
>方式一：前往[Releases][Releases]下载文件，直接在该解决方案中进行开发**（推荐方式）**

>方式二：PM控制台执行 `Install-Package Weixin.Netcore.Core -Version 0.1.0-preview`

>#### 1、网页授权

>网页授权分snsapi_base和snsapi_userinfo两种方式，具体区别请查看微信[官方文档][微信网页授权文档]，这里以snsapi_base方式介绍。

>
	private readonly AuthorizationContainer _container;
	string openId = _container.GetOpenId(code);//回调中微信服务器返回的code
	UserInfo userInfo = _container.GetUserInfo(openId, Language.zh_CN);

>至此，已经获取到用户信息。

>#### 2、消息处理

>以Echo文本消息介绍。

>编写EchoTextHandler类继承自TextMessageHandlerBase，重写其TextMessageHandler方法

>
	public class EchoTextHandler : TextMessageHandlerBase
    {
        private readonly IMessageReply<TextMessage> _messageReply;
        public EchoTextHandler(IMessageReply<TextMessage> messageReply)
        {
            _messageReply = messageReply;
        }
        public override string TextMessageHandler(TextMessage message)
        {
            var textMessage = new TextMessage()
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = message.Content
            };
            return _messageReply.CreateXml(textMessage);
        }
    }

>如果使用方式一进行的开发，Echo文本消息已经完成。

>如果使用方式二，在处理文本消息时用此类替换默认的 `TextMessageHandlerBase` 即可。

>#### 3、微信支付


>第一步：配置JS-SDK

>
	private readonly ConfigGenerater _generater;
	JsApiConfig jsConfig = _generater.GenerateJsApiConfig(url, 'chooseWXPay');

>这样就拿到了JS-SDK的配置信息，url是使用JS-SDK的页面，建议前台获取，方法：`window.location.href.split('#')[0]`

>第二步：配置chooseWXPay

>
	ChooseWxPayConfig wxPayConfig = _generater.GenerateChooseWxPayConfig(unifiedOrder);

>这样就拿到了chooseWXPay方法的配置信息，上边传入的unifiedOrder是 `UnifiedOrder` 类型的对象，[查看该类][UnifiedOrder]。

## 联系方式

>我的微信：wdcdavyc，如果有什么错误或者有什么疑问可以加我微信沟通批评（请备注：Github）。












[Releases]:https://github.com/Weidaicheng/Weixin.Netcore/releases
[Issues]:https://github.com/Weidaicheng/Weixin.Netcore/issues
[Cache]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Cache
[Core]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Core
[Entity]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Entity
[Extensions]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Extensions
[Test]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Test
[Utility]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Utility
[Web]:https://github.com/Weidaicheng/Weixin.Netcore/tree/master/Weixin.Netcore.Web
[微信网页授权文档]:https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
[JS-API文档]:https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141115
[UnifiedOrder]:https://github.com/Weidaicheng/Weixin.Netcore/blob/master/Weixin.Netcore.Entity/Pay/UnifiedOrder.cs
[CoreDetail]:#
[MessageDetail]:#