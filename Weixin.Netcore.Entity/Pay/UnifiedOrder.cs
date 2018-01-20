using Weixin.Netcore.Entity.Enums;

namespace Weixin.Netcore.Entity.Pay
{
    /// <summary>
    /// 统一下单
    /// </summary>
    public class UnifiedOrder
    {
        /// <summary>
        /// 公众账号ID
        /// (MaxLength:32)
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// (MaxLength:32)
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号，自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// (MaxLength:32)
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串
        /// (MaxLength:32)
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// (MaxLength:32)
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 签名类型
        /// (MaxLength:32)
        /// </summary>
        public string sign_type { get; set; }

        /// <summary>
        /// 商品描述
        /// (MaxLength:128)
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 商品详情
        /// (MaxLength:6000)
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 附加数据
        /// (MaxLength:127)
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户订单号
        /// (MaxLength:32)
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 标价币种
        /// (MaxLength:16)
        /// </summary>
        public FeeType fee_type { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 终端IP，APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
        /// (MaxLength:16)
        /// </summary>
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 交易起始时间，订单生成时间，格式为yyyyMMddHHmmss
        /// (MaxLength:14)
        /// </summary>
        public string time_start { get; set; }

        /// <summary>
        /// 交易结束时间，订单失效时间，格式为yyyyMMddHHmmss
        /// 建议：最短失效时间间隔大于1分钟
        /// (MaxLength:14)
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// (MaxLength:32)
        /// </summary>
        public string goods_tag { get; set; }

        /// <summary>
        /// 通知地址，异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// (MaxLength:256)
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 交易类型，取值如下：JSAPI，NATIVE，APP
        /// (MaxLength:16)
        /// </summary>
        public TradeType trade_type { get; set; }

        /// <summary>
        /// 商品ID
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// (MaxLength:32)
        /// </summary>
        public string product_id { get; set; }

        /// <summary>
        /// 指定支付方式
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// (MaxLength:32)
        /// </summary>
        public string limit_pay { get; set; }

        /// <summary>
        /// 用户标识
        /// trade_type=JSAPI时（即公众号支付），此参数必传
        /// (MaxLength:128)
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 场景信息
        /// 目前支持上报实际门店信息。该字段为JSON对象数据，对象格式为{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }} 
        /// (MaxLength:256)
        /// </summary>
        public SceneInfo scene_info { get; set; }
    }
}
