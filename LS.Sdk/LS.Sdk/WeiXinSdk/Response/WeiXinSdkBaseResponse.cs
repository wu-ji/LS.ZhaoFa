using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.WeiXinSdk.Response
{
    public class WeiXinSdkBaseResponse:BaseResponse
    {
        /// <summary>
        /// 返回状态码 SUCCESS/FAIL 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }


        /// <summary>
        /// 返回信息 返回信息，如非空，为错误原因 签名失败 参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }
    }
}
