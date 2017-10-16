using LS.Sdk._1.BaseSDK;
using LS.Sdk.WeiXinSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Request
{
    /// <summary>
    /// 统一下单
    /// </summary>
    public class WeiXinSdkUnifiedorderRequest :BaseRequest<WeiXinSdkUnifiedorderResponse>
    {
        /// <summary>
        /// 统一下单url地址
        /// </summary>
        /// <returns></returns>
        public override string Url()
        {
            return "/pay/unifiedorder";
        }
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
        /// 商品描述
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 总金额 单位为 分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 发起支付的 客户端ip
        /// </summary>
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 支付结果回调地址 (不能携带参数)
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 交易类型 H5支付的交易类型为MWEB
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 支付场景  {"h5_info": {"type":"Wap","wap_url": "https://pay.qq.com","wap_name": "腾讯充值"}}//wap_url :WAP网站URL地址 wap_name :网站名称
        /// </summary>
        public string scene_info { get; set; }

    }
}
