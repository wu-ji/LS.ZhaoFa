using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools.ApiTools
{
    public class ApiFileDirectoryPara
    {
        /// <summary>
        /// api 发生错误 日志根目录 文件夹名称
        /// </summary>
        public static readonly string ApiErrorDir = "ApiError";

        /// <summary>
        /// 全局锁创建 日志根目录 文件夹名称
        /// </summary>
        public static readonly string LockDir = "LockLog";

        /// <summary>
        /// 微信业务 日志根目录 文件夹名称
        /// </summary>
        public static readonly string WeiXinBusinessLog = "WeiXinBusinessLog";

        /// <summary>
        /// 微信错误 日志根目录 文件夹名称
        /// </summary>
        public static readonly string WeiXinErrorLog = "WeiXinErrorLog";
    }
}
