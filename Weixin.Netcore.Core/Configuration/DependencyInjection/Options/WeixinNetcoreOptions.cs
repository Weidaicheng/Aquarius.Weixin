using Weixin.Netcore.Entity.Configuration;
using Weixin.Netcore.Entity.Enums;

namespace Weixin.Netcore.Core.Configuration.DependencyInjection.Options
{
    /// <summary>
    /// Weixin.Netcore 选项配置
    /// </summary>
    public class WeixinNetcoreOptions
    {
        /// <summary>
        /// 基础设置
        /// </summary>
        public BaseSettings BaseSetting { get; set; }

        /// <summary>
        /// 缓存模式
        /// </summary>
        public CacheType CacheType { get; set; }

        /// <summary>
        /// 消息中间件模式
        /// </summary>
        public MessageMiddlewareType MsgMiddlewareType { get; set; }

        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisConfig RedisConfig { get; set; }
    }
}
