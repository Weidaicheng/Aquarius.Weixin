using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weixin.Netcore.Core.Configuration.DependencyInjection;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Entity.Configuration;
using Weixin.Netcore.Entity.Enums;
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

            services.AddWeixinNetcore(opt =>
            {
                opt.BaseSetting = new BaseSettings()
                {
                    Debug = true,
                    IsRepetValid = false,
                    AppId = Configuration["AppId"],
                    AppSecret = Configuration["AppSecret"],
                    Token = Configuration["Token"],
                    EncodingAESKey = Configuration["EncodingAESKey"],
                    ApiKey = string.Empty,
                    CertPass = string.Empty,
                    CertRoot = string.Empty,
                    MchId = string.Empty
                };
                opt.CacheType = CacheType.InMemory;
                opt.MsgMiddlewareType = MessageMiddlewareType.Plain;
            });

            services.AddScoped<TextMessageHandlerBase, EchoTextHandler>();
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
