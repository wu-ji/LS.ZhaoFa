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
     public class ZhiFuBaoSdkWapPayRequest: BaseRequest<ZhiFuBaoSdkWapPayResponse>
    {
        public override HttpMethod GetHttpMethod()
        {
            return HttpMethod.Post;
        }
        public override string Url()
        {
            return "alipay.trade.wap.pay";
        }
        /// <summary>
        /// 商品的标题/交易标题/订单标题/订单关键字等。
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 商户网站唯一订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]
        /// </summary>
        public decimal total_amount { get; set; }


        /// <summary>
        /// 销售产品码，商家和支付宝签约的产品码。该产品请填写固定值：QUICK_WAP_WAY
        /// </summary>
        public string product_code { get; set; }


        /// <summary>
        /// 添加该参数后在h5支付收银台会出现返回按钮，可用于用户付款中途退出并返回到该参数指定的商户网站地址。注：该参数对支付宝钱包标准收银台下的跳转不生效。
        /// </summary>
        public string quit_url { get; set; }
    }
}
