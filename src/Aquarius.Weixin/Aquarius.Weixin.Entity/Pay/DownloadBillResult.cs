using System;
using System.Collections.Generic;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 下载对账单结果
    /// </summary>
    public class DownloadBillResult
    {
        /// <summary>
        /// 详情
        /// </summary>
        public List<DownloadBillResultDetail> DownloadBillResultDetails { get; set; }

        /// <summary>
        /// 统计
        /// </summary>
        public DownloadBillResultStatistics DownloadBillResultStatistics { get; set; }
    }

    /// <summary>
    /// 下载对账单统计
    /// </summary>
    public class DownloadBillResultStatistics
    {
        /// <summary>
        /// 总交易单数
        /// </summary>
        public int TotalTradeCount { get; set; }

        /// <summary>
        /// 总交易额
        /// </summary>
        public decimal TotalTradeFee { get; set; }

        /// <summary>
        /// 总退款金额
        /// </summary>
        public decimal TotalRefundFee { get; set; }

        /// <summary>
        /// 总代金券或立减优惠退款金额
        /// </summary>
        public decimal TotalCouponRefundFee { get; set; }

        /// <summary>
        /// 手续费总金额
        /// </summary>
        public decimal TotalTariffFee { get; set; }
    }

    /// <summary>
    /// 下载对账单详情
    /// </summary>
    public class DownloadBillResultDetail
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime? TradeTime { get; set; }

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TradeType? TradeType { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        public TradeState? TradeState { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>
        public BankType? BankType { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public FeeType? FeeType { get; set; }

        /// <summary>
        /// 总金额（单位：元）
        /// </summary>
        public decimal? TotalFee { get; set; }

        /// <summary>
        /// 代金券或立减优惠金额
        /// </summary>
        public decimal? CouponFee { get; set; }

        /// <summary>
        /// 退款申请时间
        /// </summary>
        public DateTime? RefundTime { get; set; }

        /// <summary>
        /// 退款成功时间
        /// </summary>
        public DateTime? RefundSuccessTime { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal? RefundFee { get; set; }

        /// <summary>
        /// 代金券或立减优惠退款金额
        /// </summary>
        public decimal? CouponRefundFee { get; set; }

        /// <summary>
        /// 退款类型
        /// todo:退款类型是否是退款渠道？
        /// </summary>
        public string RefundType { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        public RefundState? RefundState { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 商户数据包
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal? ServiceCharge { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        public string Tariff { get; set; }

        /// <summary>
        /// 返还手续费
        /// </summary>
        public decimal? RefundServiceCharge { get; set; }
    }
}
