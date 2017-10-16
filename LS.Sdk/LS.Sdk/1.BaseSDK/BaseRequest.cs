using LS.Sdk._0.ISDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LS.Sdk._1.BaseSDK
{
    public abstract class BaseRequest<T> : IBaseRequest<T> where T : IBaseResponse,new()
    {

        /// <summary>
        /// 设置参数格式 数据
        /// </summary>
        /// <returns></returns>
        public virtual string GetRequestData()
        {
            string data = string.Empty;

            return data;
        }

        public virtual HttpMethod GetHttpMethod()
        {
            return HttpMethod.Post;
        }

        public virtual string Url()
        {
            return "baseUrl";
        }

        public virtual string GetContentType()
        {
            return "application/x-www-form-urlencoded";
        }

        public virtual Encoding GetEncoding()
        {
            return Encoding.UTF8;
        }
    }
}
