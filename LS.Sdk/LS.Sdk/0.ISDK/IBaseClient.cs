using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LS.Sdk._0.ISDK
{
    /// <summary>
    /// 所有 SDK（如美团 微信等）通用规范
    /// </summary>
    public interface IBaseClient
    {
        /// <summary>
        /// api平台对应的 appid
        /// </summary>
        string AppId { get; set; }

        /// <summary>
        /// api平台对应的 Secret
        /// </summary>
        string Secret { get; set; }

        /// <summary>
        /// 当前api接口对应的回调地址
        /// </summary>
        string ReturnUrl { get; set; }

        /// <summary>
        /// 请求对应的Token凭证
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// api平台 主域名
        /// </summary>
        string DoMain { get; set; }

        /// <summary>
        /// 发送 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        T Send<T>(IBaseRequest<T> request) where T : IBaseResponse,new();

        /// <summary>
        /// 异步发送 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
         T AsyncSend<T>(IBaseRequest<T> request) where T : IBaseResponse, new();

        /// <summary>
        /// 处理回调 方法
        /// </summary>
        /// <typeparam name="T">返回的类型</typeparam>
        /// <param name="requestContent">接收 对方的文本数据</param>
        /// <param name="deserializeType">反序列化 方式类型 用来区分不同的 响应数据格式对应的处理方法</param>
        /// <returns></returns>
        T Callback<T>(string requestContent ,int deserializeType) where T : new();

        /// <summary>
        /// 设置http请求报文等参数
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request) where T : IBaseResponse, new();

        HttpContent SetHttpContent<T>(IBaseRequest<T> request) where T : IBaseResponse, new();

        /// <summary>
        /// api平台对应的计算方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        string Sign<T>(IBaseRequest<T> request) where T : IBaseResponse, new();
    }
}
