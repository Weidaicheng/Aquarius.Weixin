using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Aquarius.Weixin.Core.Configuration.DependencyInjection.Options;

namespace Aquarius.Weixin.Core.Configuration.DependencyInjection
{
    /// <summary>
    /// 添加Aquarius.Weixin的DI扩展
    /// </summary>
    public static class AquariusWeixinServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Aquarius.Weixin
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IAquariusWeixinBuilder AddAquariusWeixin(this IServiceCollection services)
        {
            IAquariusWeixinBuilder builder = new AquariusWeixinBuilder(services);

            builder.AddInterfaceCallers();
            builder.AddContainers();
            builder.AddSignGenerAndVerifyer();
            builder.AddMsgRepetHandler();
            builder.AddMsgParser();
            builder.AddMsgProcesser();
            builder.AddMsgHandler();
            builder.AddMsgReply();
            builder.AddJsApi();
            builder.AddBaseSetting();
            builder.AddCache();
            builder.AddMsgMiddleware();
            builder.AddPayService();

            return builder;
        }

        /// <summary>
        /// 添加 Aquarius.Weixin
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        /// <returns></returns>
        public static IAquariusWeixinBuilder AddAquariusWeixin(this IServiceCollection services, Action<AquariusWeixinOptions> setupAction)
        {
            services.Configure(setupAction);
            return AddAquariusWeixin(services);
        }

        /// <summary>
        /// 添加 Aquarius.Weixin
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IAquariusWeixinBuilder AddAquariusWeixin(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AquariusWeixinOptions>(configuration);
            return services.AddAquariusWeixin();
        }
    }
}
