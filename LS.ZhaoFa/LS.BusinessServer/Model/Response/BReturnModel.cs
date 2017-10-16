using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Model.Response
{
    /// <summary>
    /// 业务层 对api层响应的 通用模型
    /// </summary>
    public  class BReturnModel
    {
        /// <summary>
        /// 标志业务处理成功
        /// </summary>
        public static readonly string IsOk = "ok";

        /// <summary>
        /// 标志业务处理失败
        /// </summary>
        public static readonly string Error = "Error";

        /// <summary>
        /// 业务层响应码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 业务层响应消息
        /// </summary>

        public string Msg { get; set; }

        /// <summary>
        /// 业务层响应数据s
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static BReturnModel ReturnOk()
        {
            return new BReturnModel() { Code = BReturnModel.IsOk };
        }
        public static BReturnModel ReturnOk(string msg)
        {
            return new BReturnModel() { Code = BReturnModel.IsOk ,Msg = msg };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="obj">关联的数据</param>
        /// <returns></returns>
        public static BReturnModel ReturnOk<T>(string msg, T obj)
        {
            return new BReturnModel() { Code = BReturnModel.IsOk, Msg = msg ,Data = obj};
        }


        public static BReturnModel ReturnError()
        {
            return new BReturnModel() { Code = BReturnModel.Error };
        }
        public static BReturnModel ReturnError(string msg)
        {
            return new BReturnModel() { Code = BReturnModel.Error, Msg = msg };
        }

        public static BReturnModel ReturnError<T>(string msg, T obj)
        {
            return new BReturnModel() { Code = BReturnModel.Error, Msg = msg, Data = obj };
        }
    }
}
