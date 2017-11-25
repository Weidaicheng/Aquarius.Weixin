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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //RestSharp
            services.AddScoped<IRestClient, RestClient>();

            //Cache
            services.AddScoped<ICache, RedisCache>(provider =>
            {
                string redisServerHost = Configuration["RedisHost"];
                int redisServerPort = string.IsNullOrEmpty(Configuration["RedisPort"]) ? 6379 : int.Parse(Configuration["RedisPort"]);
                string redisPassword = string.IsNullOrEmpty(Configuration["RedisPasswd"]) ? string.Empty : Configuration["RedisPasswd"];
                return new RedisCache(redisServerHost, redisServerPort, redisPassword);
            });

            //MessageRepetHandler
            services.AddScoped<IMessageRepetHandler, MessageRepetHandler>();

            //MessageReply
            services.AddScoped<IMessageReply<TextMessage>, TextMessageReply>();
            services.AddScoped<IMessageReply<ImageMessage>, ImageMessageReply>();
            services.AddScoped<IMessageReply<MusicMessage>, MusicMessageReply>();
            services.AddScoped<IMessageReply<VoiceMessage>, VoiceMessageReply>();
            services.AddScoped<IMessageReply<VideoMessage>, VideoMessageReply>();
            services.AddScoped<IMessageReply<NewsMessage>, NewsMessageReply>();

            //MessageProcesser
            //根据需要添加相应的消息类型配置
            services.AddScoped<IMessageProcesser, ClickEvtMessageProcesser>();

            //MessageHandler
            //在Weixin.Netcore.Extensions项目中
            //namespace：Weixin.Netcore.Extensions.Message.Handler
            //根据需要添加各种消息消息处理类
            services.AddScoped<IClickEvtMessageHandler, ClickEventReplyTextExtension>();
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
