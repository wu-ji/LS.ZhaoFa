using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Response
{
    public class WeiXinSdkOrderQueryResponse : WeiXinSdkBaseResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误 详情
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
    }
}
