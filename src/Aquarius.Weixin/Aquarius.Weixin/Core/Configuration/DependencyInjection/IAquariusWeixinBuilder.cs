namespace Aquarius.Weixin.Core.Configuration.DependencyInjection
{
    /// <summary>
    /// Aquarius.Weixin Builder
    /// </summary>
    public interface IAquariusWeixinBuilder
    {
        /// <summary>
        /// 添加接口调用
        /// </summary>
        void AddInterfaceCallers();

        /// <summary>
        /// 添加容器
        /// </summary>
        void AddContainers();

        /// <summary>
        /// 添加签名生产器和认证器
        /// </summary>
        void AddSignGenerAndVerifyer();

        /// <summary>
        /// 添加消息处理器
        /// </summary>
        void AddMsgRepetHandler();

        /// <summary>
        /// 添加消息解析器
        /// </summary>
        void AddMsgParser();

        /// <summary>
        /// 添加消息处理器
        /// </summary>
        void AddMsgProcesser();

        /// <summary>
        /// 添加消息处理器
        /// </summary>
        void AddMsgHandler();

        /// <summary>
        /// 添加消息回复
        /// </summary>
        void AddMsgReply();

        /// <summary>
        /// 添加Js-Api
        /// </summary>
        void AddJsApi();

        /// <summary>
        /// 添加基础设置
        /// </summary>
        void AddBaseSetting();

        /// <summary>
        /// 添加缓存
        /// </summary>
        void AddCache();

        /// <summary>
        /// 添加消息中间件
        /// </summary>
        void AddMsgMiddleware();

        /// <summary>
        /// 添加支付服务
        /// </summary>
        void AddPayService();
    }
}
