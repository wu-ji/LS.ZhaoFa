using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Sdk._0.ISDK;
using System.Net.Http;

namespace LS.Sdk.YinLianSdk
{
    /// <summary>
    /// 银联客户端 目前 银联貌似部支持申请 所以还未实现
    /// 测试(沙盒) https://gateway.test.95516.com/gateway/api/backTransReq.do
    /// 正式 https://gateway.95516.com/gateway/api/backTransReq.do
    /// </summary>
    public class YinLianClient : BaseClient
    {
        public override string AppId { get; set; }
        public override string Secret { get; set; }
        public override string ReturnUrl { get; set; }
        public override string DoMain { get; set; }
        public override string Token { get; set; }

        public override T AsyncSend<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }

        public override T Callback<T>(string requestContent, int deserializeType)
        {
            throw new NotImplementedException();
        }

        public override HttpContent SetHttpContent<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }

        public override HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }

        public override string Sign<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }
    }
}
