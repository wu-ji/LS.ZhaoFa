using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.ZhiFuBaoSdk.Response
{
    public class ZhiFuBaoSdkBaseResponse : BaseResponse
    {
        public Alipay_trade_query_response alipay_trade_query_response { get; set; }
        public string sign { get; set; }
    }

    /// <summary>
    /// 支付宝返回通用类
    /// </summary>
    public class Alipay_trade_query_response
    {
        /// <summary>
        /// 网关返回码
        /// </summary>
        public string code { get; set; }

        public string msg { get; set; }

        /// <summary>
        /// 业务返回码
        /// </summary>
        public string sub_code { get; set; }

        /// <summary>
        /// 业务消息
        /// </summary>
        public string sub_msg { get; set; }

    }
}
