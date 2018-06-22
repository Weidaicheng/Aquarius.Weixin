using System;
using System.ComponentModel.DataAnnotations;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 统一下单
    /// </summary>
    public class UnifiedOrder
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号，自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        [MaxLength(32)]
        public string device_info { get; set; } = "WEB";

        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string sign { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [MaxLength(32)]
        public string sign_type { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string body { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        [MaxLength(6000)]
        public string detail { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        [MaxLength(127)]
        public string attach { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 标价币种
        /// </summary>
        [MaxLength(16)]
        public FeeType fee_type { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        [Required]
        public int total_fee { get; set; }

        /// <summary>
        /// 终端IP，APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
        /// </summary>
        [Required]
        [MaxLength(16)]
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 交易起始时间，订单生成时间，格式为yyyyMMddHHmmss
        /// </summary>
        [MaxLength(14)]
        public string time_start { get; set; }

        /// <summary>
        /// 交易结束时间，订单失效时间，格式为yyyyMMddHHmmss
        /// 建议：最短失效时间间隔大于1分钟
        /// </summary>
        [MaxLength(14)]
        public string time_expire { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// </summary>
        [MaxLength(32)]
        public string goods_tag { get; set; }

        /// <summary>
        /// 通知地址，异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string notify_url { get; set; }

        /// <summary>
        /// 交易类型，取值如下：JSAPI，NATIVE，APP
        /// </summary>
        [Required]
        [MaxLength(16)]
        public TradeType trade_type { get; set; }

        /// <summary>
        /// 商品ID
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// </summary>
        [MaxLength(32)]
        public string product_id { get; set; }

        /// <summary>
        /// 指定支付方式
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        [MaxLength(32)]
        public string limit_pay { get; set; }

        /// <summary>
        /// 用户标识
        /// trade_type=JSAPI时（即公众号支付），此参数必传
        /// </summary>
        [MaxLength(128)]
        public string openid { get; set; }

        /// <summary>
        /// 场景信息
        /// 目前支持上报实际门店信息。该字段为JSON对象数据，对象格式为{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }} 
        /// </summary>
        [MaxLength(256)]
        public SceneInfo scene_info { get; set; }
    }

    /// <summary>
    /// 统一下单信息
    /// </summary>
    public class UnifiedOrderInfo
    {
        /// <summary>
        /// 统一下单信息
        /// </summary>
        /// <param name="tradeNo">订单号</param>
        /// <param name="totalFee">订单金额</param>
        /// <param name="title">标题</param>
        /// <param name="notifyUrl">回调Url</param>
        /// <param name="openId">OpenId</param>
        /// <param name="deviceIp">设备Ip</param>
        /// <param name="tradeType">交易类型</param>
        /// <param name="signType">签名类型</param>
        public UnifiedOrderInfo(string tradeNo, decimal totalFee, string title, string notifyUrl, string openId, string deviceIp, TradeType tradeType = TradeType.JSAPI, WxPaySignType signType = WxPaySignType.MD5)
        {
            TradeNo = tradeNo;
            TotalFee = totalFee;
            Body = title;
            NotifyUrl = notifyUrl;
            OpenId = openId;
            DeviceIp = deviceIp;
            TradeType = tradeType;
            SignType = signType;
        }

        /// <summary>
        /// 随机字符串（默认赋值）
        /// </summary>
        public string NonceStr { get; set; } = UtilityHelper.GenerateNonce();

        /// <summary>
        /// 时间戳（默认赋值）
        /// </summary>
        public long TimeStamp { get; set; } = UtilityHelper.GetTimeStamp();

        /// <summary>
        /// 订单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalFee { get; set; }

        /// <summary>
        /// 通知Url
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 交易方式（默认<see cref="TradeType.JSAPI"/>）
        /// </summary>
        public TradeType TradeType { get; set; } = TradeType.JSAPI;

        /// <summary>
        /// 签名类型（默认<see cref="WxPaySignType.MD5"/>）
        /// </summary>
        public WxPaySignType SignType { get; set; } = WxPaySignType.MD5;

        /// <summary>
        /// 货币类型（默认<see cref="FeeType.CNY"/>）
        /// </summary>
        public FeeType FeeType { get; set; } = FeeType.CNY;

        /// <summary>
        /// 设备IP
        /// </summary>
        public string DeviceIp { get; set; }

        /// <summary>
        /// 交易起始时间（默认为<see cref="DateTime.Now"/>）
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 交易结束时间（默认为30分钟后）
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now.AddMinutes(30);

        /// <summary>
        /// 附加数据（非必需）
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 商品详情（非必需）
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 订单优惠标记（非必需）
        /// </summary>
        public string GoodTags { get; set; }

        /// <summary>
        /// 启用限制信用卡支付（默认为<see cref="false"/>）
        /// </summary>
        public bool UseLimitPay { get; set; } = false;

        /// <summary>
        /// 场景信息（非必需）
        /// </summary>
        public SceneInfo SceneInfo { get; set; }

        /// <summary>
        /// H5支付跳转Url
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
