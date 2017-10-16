using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools.ApiTools
{
    /// <summary>
    /// 所有项目通用 -为api平台 提供具体的 响应状态码
    /// </summary>
    public class ApiCodePara
    {
        /// <summary>
        /// 标志服务端 处理成功
        /// </summary>
        public readonly static int Ok = 200;
        /// <summary>
        /// 标志 服务端处理 出错
        /// </summary>
        public readonly static int Error = 500;

        /// <summary>
        /// 标志 服务端 没有对应的 url入口
        /// </summary>
        public readonly static int NotUrl = 404;

        /// <summary>
        /// 标志 服务器身份凭证失效
        /// </summary>
        public readonly static int IdentityInvalid = 401;
    }
}
