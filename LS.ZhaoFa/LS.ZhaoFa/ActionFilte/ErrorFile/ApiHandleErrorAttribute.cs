using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.Models.Api.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace LS.ZhaoFa.ActionFilte.ErrorFile
{
    /// <summary>
    /// 全局错误 捕捉类
    /// </summary>
    public class ApiHandleErrorAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 发生错误 处理方式
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogQueueModel logQueueModel = new LogQueueModel()
            {
                FileName = ApiFileDirectoryPara.ApiErrorDir,
                Msg = actionExecutedContext.Exception.Message
            };

            LogHelp.AddLogQueue(logQueueModel);

            var returnModel = ApiReturnModel.ReturnError(actionExecutedContext.Exception.Message);

            actionExecutedContext.Response = new HttpResponseMessage();
            actionExecutedContext.Response.Content = new StringContent(JsonConvert.SerializeObject(returnModel));

            return;
        }
    }
}