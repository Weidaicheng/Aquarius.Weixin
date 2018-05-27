using System;
using System.Collections.Generic;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 查询订单返回结果
    /// </summary>
    public class OrderQueryResult
    {
        /// <summary>
        /// 返回状态码，此字段是通信标识，非交易标识
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息，如非空，为错误原因 
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 公众账号ID 
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 随机字符串 
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 业务结果，SUCCESS/FAIL 
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述 
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 是否关注公众号
        /// </summary>
        public bool? IsSubscribe { get; set; }

        /// <summary>
        /// 交易类型 
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TradeType TradeType { get; set; }

        /// <summary>
        /// 交易状态 
        /// </summary>
        public string trade_state { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public TradeState TradeState { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public BankType BankType { get; set; }

        /// <summary>
        /// 标价金额，订单总金额，单位为分 
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 应结订单金额，当订单使用了免充值型优惠券后返回该参数，应结订单金额=订单金额-免充值优惠券金额
        /// </summary>
        public int? settlement_total_fee { get; set; }

        /// <summary>
        /// 标价币种
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 标价币种
        /// </summary>
        public FeeType FeeType { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金支付币种
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 现金支付币种
        /// </summary>
        public FeeType CashFeeType { get; set; }

        /// <summary>
        /// 代金券金额，“代金券”金额小于等于订单金额，订单金额-“代金券”金额=现金支付金额
        /// </summary>
        public int? coupon_fee { get; set; }

        /// <summary>
        /// 代金券使用数量
        /// </summary>
        public int? coupon_count  { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 支付完成时间，格式为yyyyMMddHHmmss
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// 交易状态描述
        /// </summary>
        public string trade_state_desc { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public List<Coupon> Coupons { get; set; }
    }
}
