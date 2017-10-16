using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk._0.ISDK
{
    public interface IBaseRequest<T> where T :IBaseResponse  ,new()
    {
        /// <summary>
        /// api地址对应的 具体接口
        /// </summary>
        string Url();

        /// <summary>
        /// api接口对应的请求方式 如post
        /// </summary>
        HttpMethod GetHttpMethod();

        /// <summary>
        /// 设置参数格式的方法s
        /// </summary>
        /// <returns></returns>
        string GetRequestData();

        /// <summary>
        /// 获取请求内容格式
        /// </summary>
        /// <returns></returns>
        string GetContentType();

        Encoding GetEncoding();
    }
}
