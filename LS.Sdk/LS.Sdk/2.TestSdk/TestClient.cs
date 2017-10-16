using LS.Sdk._0.ISDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using LS.Sdk._1.BaseSDK;

namespace LS.Sdk._2.TestSdk
{
    public class TestClient : BaseClient
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

        public override HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request)
        {
            HttpRequestMessage requestMsg = new HttpRequestMessage(request.GetHttpMethod(), DoMain + request.Url());
            //设置请求头 等相关凭证
            requestMsg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("wuji", "123");
            requestMsg.Content = SetHttpContent(request);
            //根据request属性设置相关的 参数 以及格式

            return requestMsg;
        }

        public override string Sign<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }

        public override HttpContent SetHttpContent<T>(IBaseRequest<T> request)
        {
            var pros = request.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var item in pros)
            {
                dictionary.Add(item.Name, item.GetValue(request));
            }

            var para = dictionary.Select(str => $"{str.Key}={str.Value}");

            var content = string.Join("&", para);

            return new StringContent(content);
        }

        public override T Callback<T>(string requestContent, int deserializeType)
        {
            throw new NotImplementedException();
        }
    }
}
