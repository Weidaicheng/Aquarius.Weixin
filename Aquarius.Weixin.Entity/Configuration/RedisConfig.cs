namespace Aquarius.Weixin.Entity.Configuration
{
    /// <summary>
    /// Redis设置
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6379;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
