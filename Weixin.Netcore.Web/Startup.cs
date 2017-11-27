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
            services.AddMvc();

            //Add Autofac
            var builder = new ContainerBuilder();

            //RestSharp
            builder.RegisterType<RestClient>().As<IRestClient>();

            //Cache
            builder.Register(context =>
            {
                string redisServerHost = Configuration["RedisHost"];
                int redisServerPort = string.IsNullOrEmpty(Configuration["RedisPort"]) ? 6379 : int.Parse(Configuration["RedisPort"]);
                string redisPassword = string.IsNullOrEmpty(Configuration["RedisPasswd"]) ? string.Empty : Configuration["RedisPasswd"];
                return new RedisCache(redisServerHost, redisServerPort, redisPassword);
            }).As<ICache>();

            //MessageRepetHandler
            builder.RegisterType<MessageRepetHandler>().As<IMessageRepetHandler>();

            //调试模式
            builder.Register(context => new DebugMode(false)).As<IDebugMode>();

            //MessageReply
            builder.RegisterType<TextMessageReply>().As<IMessageReply<TextMessage>>();
            builder.RegisterType<ImageMessageReply>().As<IMessageReply<ImageMessage>>();
            builder.RegisterType<MusicMessageReply>().As<IMessageReply<MusicMessage>>();
            builder.RegisterType<VoiceMessageReply>().As<IMessageReply<VoiceMessage>>();
            builder.RegisterType<VideoMessageReply>().As<IMessageReply<VideoMessage>>();
            builder.RegisterType<NewsMessageReply>().As<IMessageReply<NewsMessage>>();

            //MessageProcesser
            //根据需要添加相应的消息类型配置
            builder.RegisterType<ClickEvtMessageProcesser>().As<IMessageProcesser>();

            //启用消息重复验证
            builder.Register(context => new MessageRepetValidUsage(true)).As<IMessageRepetValidUsage>();

            //MessageHandler
            //在Weixin.Netcore.Extensions项目中
            //namespace：Weixin.Netcore.Extensions.Message.Handler
            //根据需要添加各种消息消息处理类
            builder.RegisterType<ClickEventReplyTextExtension>().As<IClickEvtMessageHandler>();

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
