using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.Configuration.DependencyInjection.Options;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Core.JsApi;
using Aquarius.Weixin.Core.MaintainContainer;
using Aquarius.Weixin.Core.Message;
using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Core.Message.Handler.DefaultHandler;
using Aquarius.Weixin.Core.Message.Processer;
using Aquarius.Weixin.Core.Message.Reply;
using Aquarius.Weixin.Core.Middleware;
using Aquarius.Weixin.Core.Pay;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using System;

namespace Aquarius.Weixin.Core.Configuration.DependencyInjection
{
    public class AquariusWeixinBuilder : IAquariusWeixinBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;
        private readonly AquariusWeixinOptions _options;

        public AquariusWeixinBuilder(IServiceCollection services)
        {
            _services = services;
            _provider = services.BuildServiceProvider();//get an instance of IServiceProvider
            _options = _provider.GetRequiredService<IOptions<AquariusWeixinOptions>>().Value;//resolve an instance of AquariusWeixinOptions
        }

        public void AddInterfaceCallers()
        {
            _services.AddScoped<DisposableMessageInterfaceCaller, DisposableMessageInterfaceCaller>();
            _services.AddScoped<MenuInterfaceCaller, MenuInterfaceCaller>();
            _services.AddScoped<OAuthInterfaceCaller, OAuthInterfaceCaller>();
            _services.AddScoped<TemplateMessageInterfaceCaller, TemplateMessageInterfaceCaller>();
            _services.AddScoped<TicketInterfaceCaller, TicketInterfaceCaller>();
            _services.AddScoped<UserTagManageInterfaceCaller, UserTagManageInterfaceCaller>();
            _services.AddScoped<WxPayInterfaceCaller, WxPayInterfaceCaller>();
        }

        public void AddContainers()
        {
            _services.AddScoped<AccessTokenContainer, AccessTokenContainer>();
            _services.AddScoped<AuthorizationContainer, AuthorizationContainer>();
            _services.AddScoped<TicketContainer, TicketContainer>();
        }

        public void AddSignGenerAndVerifyer()
        {
            _services.AddScoped<SignatureGenerater, SignatureGenerater>();
            _services.AddScoped<Verifyer, Verifyer>();
        }

        public void AddMsgRepetHandler()
        {
            _services.AddScoped<MessageRepetHandler, MessageRepetHandler>();
        }

        public void AddMsgParser()
        {
            _services.AddScoped<MessageParser, MessageParser>();
        }

        public void AddMsgProcesser()
        {
            _services.AddScoped<MessageProcesser, MessageProcesser>();
        }

        public void AddMsgHandler()
        {
            _services.AddScoped<IClickEvtMessageHandler, DefaultClickEvtMessageHandler>();
            _services.AddScoped<IImageMessageHandler, DefaultImageMessageHandler>();
            _services.AddScoped<ILinkMessageHandler, DefaultLinkMessageHandler>();
            _services.AddScoped<ILocationEvtMessageHandler, DefaultLocationEvtMessageHandler>();
            _services.AddScoped<ILocationMessageHandler, DefaultLocationMessageHandler>();
            _services.AddScoped<IScanEvtMessageHandler, DefaultScanEvtMessageHandler>();
            _services.AddScoped<IScanSubscribeEvtMessageHandler, DefaultScanSubscribeEvtMessageHandler>();
            _services.AddScoped<IShortVideoMessageHandler, DefaultShortVideoMessageHandler>();
            _services.AddScoped<ISubscribeEvtMessageHandler, DefaultSubscribeEvtMessageHandler>();
            _services.AddScoped<ITextMessageHandler, DefaultTextMessageHandler>();
            _services.AddScoped<IUnsubscribeEvtMessageHandler, DefaultUnsubscribeEvtMessageHandler>();
            _services.AddScoped<IVideoMessageHandler, DefaultVideoMessageHandler>();
            _services.AddScoped<IViewEvtMessageHandler, DefaultViewEvtMessageHandler>();
            _services.AddScoped<IVoiceMessageHandler, DefaultVoiceMessageHandler>();
        }

        public void AddMsgReply()
        {
            _services.AddScoped<IMessageReply<ImageMessage>, ImageMessageReply>();
            _services.AddScoped<IMessageReply<MusicMessage>, MusicMessageReply>();
            _services.AddScoped<IMessageReply<NewsMessage>, NewsMessageReply>();
            _services.AddScoped<IMessageReply<TextMessage>, TextMessageReply>();
            _services.AddScoped<IMessageReply<VideoMessage>, VideoMessageReply>();
            _services.AddScoped<IMessageReply<VoiceMessage>, VoiceMessageReply>();
        }

        public void AddJsApi()
        {
            _services.AddScoped<ConfigGenerater, ConfigGenerater>();
        }

        public void AddBaseSetting()
        {
            _services.AddSingleton<BaseSettings, BaseSettings>(s => _options.BaseSetting);
        }

        public void AddCache()
        {
            switch (_options.CacheType)
            {
                case CacheType.InMemory:
                    //InMemory
                    _services.AddMemoryCache();
                    _services.AddScoped<ICache, InMemoryCache>();
                    break;
                case CacheType.Redis:
                    //Redis
                    if (_options.RedisConfig == null)
                        throw new RedisNotConfiguredExpection("Redis未配置");
                    _services.AddDistributedRedisCache(opt =>
                    {
                        opt.Configuration = $"{_options.RedisConfig.Host}:{_options.RedisConfig.Port}{(string.IsNullOrEmpty(_options.RedisConfig.Password) ? string.Empty : string.Concat(",password=", _options.RedisConfig.Password))}";
                    });
                    _services.AddScoped<ICache, RedisCache>();
                    break;
                default:
                    break;
            }
        }

        public void AddMsgMiddleware()
        {
            switch (_options.MsgMiddlewareType)
            {
                case MessageMiddlewareType.Plain:
                    //明文
                    _services.AddScoped<IMessageMiddleware, MessageMiddlePlain>();
                    break;
                case MessageMiddlewareType.Cipher:
                    //密文
                    _services.AddScoped<IMessageMiddleware, MessageMiddleCipher>();
                    break;
                default:
                    break;
            }
        }

        public void AddPayService()
        {
            _services.AddScoped<WxPayService, WxPayService>();
        }
    }
}
