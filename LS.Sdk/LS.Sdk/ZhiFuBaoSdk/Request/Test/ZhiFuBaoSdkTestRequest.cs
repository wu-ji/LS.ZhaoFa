using LS.Sdk.ZhiFuBaoSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.ZhiFuBaoSdk.Request.Test
{
    public  class ZhiFuBaoSdkTestRequest:ZhiFuBaoSdkBaseRequest<ZhiFuBaoSdkBaseResponse>
    {
        public override string Url()
        {
            return "alipay.offline.market.shop.querydetail";
        }
        public override Encoding GetEncoding()
        {
            return Encoding.UTF8;
        }
        public override string GetContentType()
        {
            return "application/x-www-form-urlencoded";
        }
        /// <summary>
        /// 门店id
        /// </summary>
        public string shop_id { get; set; }


        /// <summary>
        /// 服务商角色
        /// </summary>
        public string op_role { get; set; }



    }
}
