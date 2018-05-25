using System.Collections.Generic;

namespace Aquarius.Weixin.Entity.JsApi
{
    /// <summary>
    /// Js-Api配置
    /// </summary>
    public class JsApiConfig
    {
        /// <summary>
        /// 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        /// </summary>
        public bool debug { get; set; }

        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 生成签名的时间戳
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 生成签名的随机串
        /// </summary>
        public string nonceStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 需要使用的JS接口列表
        /// </summary>
        public IEnumerable<string> jsApiList { get; set; }
    }
}
