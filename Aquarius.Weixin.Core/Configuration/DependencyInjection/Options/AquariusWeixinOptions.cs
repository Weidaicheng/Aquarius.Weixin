using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Core.Configuration.DependencyInjection.Options
{
    /// <summary>
    /// Aquarius.Weixin 选项配置
    /// </summary>
    public class AquariusWeixinOptions
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
