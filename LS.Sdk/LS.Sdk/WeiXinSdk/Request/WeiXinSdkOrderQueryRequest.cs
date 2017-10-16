using LS.Sdk._1.BaseSDK;
using LS.Sdk.WeiXinSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Request
{
    public class WeiXinSdkOrderQueryRequest : BaseRequest<WeiXinSdkOrderQueryResponse>
    {
        public override string Url()
        {
            return "/pay/orderquery";
        }
        /// <summary>
        /// appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商家id
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
