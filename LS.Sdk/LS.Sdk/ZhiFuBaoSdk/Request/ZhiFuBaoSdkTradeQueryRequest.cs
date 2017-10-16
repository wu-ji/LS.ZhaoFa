using LS.Sdk._1.BaseSDK;
using LS.Sdk.ZhiFuBaoSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LS.Sdk.ZhiFuBaoSdk.Request
{
    public  class ZhiFuBaoSdkTradeQueryRequest: ZhiFuBaoSdkBaseRequest<ZhiFuBaoSdkTradeQueryResponse>
    {
        /// <summary>
        /// 订单号码
        /// </summary>
        public string out_trade_no { get; set; }

        public override string Url()
        {
            return "alipay.trade.query";
        }

        public override HttpMethod GetHttpMethod()
        {
            return HttpMethod.Post;
        }
    }
}
