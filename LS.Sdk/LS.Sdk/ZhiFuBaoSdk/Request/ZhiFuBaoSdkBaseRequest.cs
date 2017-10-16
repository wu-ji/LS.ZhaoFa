using LS.Sdk._0.ISDK;
using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LS.Sdk.ZhiFuBaoSdk.Request
{
    public class ZhiFuBaoSdkBaseRequest<TResponse>:BaseRequest<TResponse> where TResponse : IBaseResponse, new()
    {
      
    }
}
