using LS.UtilityTools.ApiTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Common
{
    /// <summary>
    /// 本api平台 统一返回模型
    /// </summary>
    public class ApiReturnModel
    {
        /// <summary>
        /// 请通过 对应静态方法创建实例
        /// </summary>
        private ApiReturnModel()
        { }
        /// <summary>
        /// 响应的 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应的消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 本次响应的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 返回成功的标识
        /// </summary>
        /// <returns></returns>
        public static ApiReturnModel ReturnOk()
        {
            return new ApiReturnModel() { Code = ApiCodePara.Ok };
        }

        /// <summary>
        /// 返回成功的标识 和消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiReturnModel ReturnOk(string msg)
        {
            return new ApiReturnModel() { Code = ApiCodePara.Ok, Msg = msg };
        }

        /// <summary>
        /// 返回成功的标识 消息 数据对象
        /// </summary>
        /// <typeparam name="T">返回对象对的类型</typeparam>
        /// <param name="msg">消息</param>
        /// <param name="obj">对象变量</param>
        /// <returns></returns>
        public static ApiReturnModel ReturnOk<T>(string msg, T obj)
        {
            return new ApiReturnModel() { Code = ApiCodePara.Ok, Msg = msg, Data = obj };
        }

        /// <summary>
        /// 返回错误的 标识
        /// </summary>
        /// <returns></returns>
        public static ApiReturnModel ReturnError()
        {
            return new ApiReturnModel() { Code = ApiCodePara.Error };
        }

        /// <summary>
        /// 返回错误 的标识和消息
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public static ApiReturnModel ReturnError(string Msg)
        {
            return new ApiReturnModel() { Code = ApiCodePara.Error, Msg = Msg };
        }

        /// <summary>
        /// 返回错误的 标识 消息 和数据对象
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="Msg">消息</param>
        /// <param name="obj">数据对象</param>
        /// <returns></returns>
        public static ApiReturnModel ReturnError<T>(string Msg, T obj)
        {
            return new ApiReturnModel() { Code = ApiCodePara.Error, Msg = Msg, Data = obj };
        }

        /// <summary>
        /// 身份凭证失效或者未找到
        /// </summary>
        /// <returns></returns>
        public static ApiReturnModel ReturnIdentityInvalid()
        {
            return new ApiReturnModel() { Code = ApiCodePara.IdentityInvalid };
        }

        /// <summary>
        /// 身份凭证失效或者未找到
        /// </summary>
        /// <returns></returns>
        /// <param name="msg">消息</param>
        public static ApiReturnModel ReturnIdentityInvalid(string msg)
        {
            return new ApiReturnModel() { Code = ApiCodePara.IdentityInvalid, Msg = msg };
        }
    }
}