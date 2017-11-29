using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage.Reply;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Core.Message.Processer;
using Weixin.Netcore.Extensions.Message.Handler;
using Weixin.Netcore.Core.Debug;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Weixin.Netcore.Core.Message.Handler.NotImplement;
using Weixin.Netcore.Model;
using Weixin.Netcore.Core.Maintain;
using Weixin.Netcore.Core.InterfaceCaller;

namespace Weixin.Netcore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //InMemory缓存
            services.AddMemoryCache();
            //Redis缓存
            //services.AddDistributedRedisCache(opt =>
            //{
            //    string redisHost = Configuration["RedisHost"];
            //    int redisPort = int.Parse(Configuration["RedisPort"] ?? "6379");
            //    string redisPasswd = Configuration["RedisPasswd"];
            //    opt.Configuration = $"{redisHost}:{redisPort}{(string.IsNullOrEmpty(redisPasswd) ? string.Empty : string.Concat(",password=", redisPasswd))}";
            //});
            services.AddMvc();

            //Add Autofac
            var builder = new ContainerBuilder();

            //RestSharp
            builder.RegisterType<RestClient>().As<IRestClient>();

            //Cache
            //使用InMemory缓存
            builder.RegisterType<InMemoryCache>().As<ICache>();
            //使用Redis缓存
            //builder.RegisterType<RedisCache>().As<ICache>();

            //MessageRepetHandler
            builder.RegisterType<MessageRepetHandler>().As<IMessageRepetHandler>();

            //微信设置
            builder.Register(context => new WeixinSetting()
            {
                AppId = Configuration["AppId"],
                AppSecret = Configuration["AppSecret"]
            }).As<WeixinSetting>();

            //调试模式
            builder.Register(context => new DebugMode(false)).As<IDebugMode>();

            //容器
            builder.RegisterType<AccessTokenContainer>().As<AccessTokenContainer>();

            //接口调用
            builder.RegisterType<OAuthInterfaceCaller>().As<OAuthInterfaceCaller>();
            builder.RegisterType<MenuInterfaceCaller>().As<MenuInterfaceCaller>();

            //MessageReply
            builder.RegisterType<TextMessageReply>().As<IMessageReply<TextMessage>>();
            builder.RegisterType<ImageMessageReply>().As<IMessageReply<ImageMessage>>();
            builder.RegisterType<MusicMessageReply>().As<IMessageReply<MusicMessage>>();
            builder.RegisterType<VoiceMessageReply>().As<IMessageReply<VoiceMessage>>();
            builder.RegisterType<VideoMessageReply>().As<IMessageReply<VideoMessage>>();
            builder.RegisterType<NewsMessageReply>().As<IMessageReply<NewsMessage>>();

            //MessageProcesser
            //根据需要添加相应的消息类型配置
            builder.RegisterType<NorEvtMessageProcesser>().As<IMessageProcesser>();

            //启用消息重复验证
            builder.Register(context => new MessageRepetValidUsage(true)).As<IMessageRepetValidUsage>();

            //MessageHandler
            //在Weixin.Netcore.Extensions项目中
            //namespace：Weixin.Netcore.Extensions.Message.Handler
            //根据需要替换各种消息消息处理类
            builder.RegisterType<ClickEventReplyTextExtension>().As<IClickEvtMessageHandler>();
            builder.RegisterType<EchoTextExtension>().As<ITextMessageHandler>();
            builder.RegisterType<ImageMessageHandlerNotImp>().As<IImageMessageHandler>();
            builder.RegisterType<VoiceMessageHandlerNotImp>().As<IVoiceMessageHandler>();
            builder.RegisterType<VideoMessageHandlerNotImp>().As<IVideoMessageHandler>();
            builder.RegisterType<ShortVideoMessageHandlerNotImp>().As<IShortVideoMessageHandler>();
            builder.RegisterType<LocationMessageHandlerNotImp>().As<ILocationMessageHandler>();
            builder.RegisterType<LinkMessageHandlerNotImp>().As<ILinkMessageHandler>();
            builder.RegisterType<SubscribeEvtMessageHandlerNotImp>().As<ISubscribeEvtMessageHandler>();
            builder.RegisterType<UnsubscribeEvtMessageHandlerNotImp>().As<IUnsubscribeEvtMessageHandler>();
            builder.RegisterType<ScanEvtMessageHandlerNotImp>().As<IScanEvtMessageHandler>();
            builder.RegisterType<LocationEvtMessageHandlerNotImp>().As<ILocationEvtMessageHandler>();

            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
