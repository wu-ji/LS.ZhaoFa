using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Response
{
    /// <summary>
    /// 微信回调
    /// </summary>
    public class WeiXinSdkNotifyOrderResponse : WeiXinSdkBaseResponse
    {
        /// <summary>
        /// appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 商家数据包
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>

        public string bank_type { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 用户是否关注公众账号
        /// </summary>

        public string is_subscribe { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 用户在 appid下的 唯一标识 类似公众号openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 商家订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string sign_type { get; set; }


        public string sub_mch_id { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string time_end { get; set; }

        /// <summary>
        /// 付款金额 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额。
        /// </summary>
        public int settlement_total_fee { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 总代金券金额
        /// </summary>
        public int coupon_fee { get; set; }

        /// <summary>
        /// 代金券使用数量	目前只支持 使用一样
        /// </summary>
        public int coupon_count { get; set; }

        /// <summary>
        /// 代金券类型 
        /// </summary>
        public string coupon_type_0 { get; set; }

        /// <summary>
        /// 代金券id
        /// </summary>
        public string coupon_id_0 { get; set; }

        /// <summary>
        /// 单个代金券 使用金额
        /// </summary>
        public int coupon_fee_0 { get; set; }

    }

}
