using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Response
{
     public class WeiXinSdkUnifiedorderResponse: WeiXinSdkBaseResponse
    {
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
        /// 微信响应的签名 用来检测 回调是否是真实的微信服务器
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时,针对H5支付此参数无特殊用途
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// 支付跳转链接	mweb_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟
        /// </summary>
        public string mweb_url { get; set; }
    }
}
