using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.ZhiFuBaoSdk.Response
{
    /// <summary>
    /// 支付宝 订单支付回调值 接收模型
    /// </summary>
    public class ZhiFuBaoSdkNotifyOrderResponse
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        public string notify_time { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string notify_type { get; set; }

        /// <summary>
        /// 通知校验ID	
        /// </summary>
        public string notify_id { get; set; }

        /// <summary>
        /// 开发者的app_id
        /// </summary>

        public string app_id { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>

        public string charset { get; set; }

        /// <summary>
        ///接口版本
        /// </summary>

        public string version { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>

        public string sign_type { get; set; }

        /// <summary>
        /// 签名
        /// </summary>

        public string sign { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>

        public string trade_no { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户业务号
        /// </summary>
        public string out_biz_no { get; set; }

        /// <summary>
        /// 买家支付宝用户号
        /// </summary>
        public string buyer_id { get; set; }

        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        public string buyer_logon_id { get; set; }

        /// <summary>
        /// 卖家支付宝用户号
        /// </summary>

        public string seller_id { get; set; }

        /// <summary>
        /// 卖家支付宝账号
        /// </summary>

        public string seller_email { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>

        public string trade_status { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public string total_amount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string receipt_amount { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public string invoice_amount { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public string buyer_pay_amount { get; set; }


        /// <summary>
        /// 集分宝金额
        /// </summary>
        public string point_amount { get; set; }

        /// <summary>
        /// 总退款金额
        /// </summary>
        public string refund_fee { get; set; }


        /// <summary>
        /// 订单标题
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 交易创建时间
        /// </summary>
        public string gmt_create { get; set; }

        /// <summary>
        /// 交易付款时间
        /// </summary>
        public string gmt_payment { get; set; }


        /// <summary>
        ///交易退款时间
        /// </summary>
        public string gmt_refund { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        public string gmt_close { get; set; }

        /// <summary>
        /// 支付金额信息
        /// </summary>
        public string fund_bill_list { get; set; }

        /// <summary>
        /// 回传参数
        /// </summary>
        public string passback_params { get; set; }

        /// <summary>
        /// 优惠券信息	
        /// </summary>
        public string voucher_detail_list { get; set; }

    }
}
