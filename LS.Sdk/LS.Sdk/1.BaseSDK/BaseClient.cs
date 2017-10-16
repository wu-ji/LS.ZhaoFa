using LS.Sdk._0.ISDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace LS.Sdk._1.BaseSDK
{
    public abstract class BaseClient : IBaseClient
    {
        public abstract string AppId { get; set; }
        public abstract string Secret { get; set; }
        public abstract string ReturnUrl { get; set; }
        public abstract string DoMain { get; set; }
        public abstract string Token { get; set; }

        public abstract T AsyncSend<T>(IBaseRequest<T> request) where T : IBaseResponse,new();
        public abstract T Callback<T>(string requestContent, int deserializeType) where T : new();

        /// <summary>
        /// 发送请求 基类方法 接收参数格式为json 如需转化xml 请在上层实例类中 重写该方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual T Send<T>(IBaseRequest<T> request) where T : IBaseResponse, new()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMsg = SetHttpRequest(httpClient, request);

            try
            {
                string json = httpClient.SendAsync(requestMsg).Result.Content.ReadAsStringAsync().Result;

                var response = JsonConvert.DeserializeObject<T>(json);
                return response;
            }
            catch (Exception e)
            {
                //记录日志 e

                return default(T);
            }
        }

        public abstract HttpContent SetHttpContent<T>(IBaseRequest<T> request) where T : IBaseResponse, new();
        public abstract HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request) where T : IBaseResponse, new();
        public abstract string Sign<T>(IBaseRequest<T> request) where T : IBaseResponse, new();
    }
}
