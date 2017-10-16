using LS.BusinessServer.Business;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.EF;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.ZhaoFa.Controllers.Api.User
{
    /// <summary>
    /// 用户 意见业务入口
    /// </summary>
    [UserAuthentication]
    public class UserSuggestionController : UserBaseController
    {
        SuggestionBusiness SuggestionBusiness = BusinessFactory.GetBusiness<SuggestionBusiness>();
        /// <summary>
        /// 用户提交意见
        /// </summary>
        /// <param name="apiSuggestionModel"></param>
        /// <returns></returns>
        public object AddSuggestion([FromBody]ApiSuggestionModel apiSuggestionModel)
        {
            var userInfo = GetCurrentUserInfo();
            UserSuggestion userSuggestion = new UserSuggestion()
            {
                Id = Guid.NewGuid(),
                Msg = apiSuggestionModel.Msg,
                UserId = userInfo.Id,
                CreateTime = DateTime.Now
            };

            try
            {
                SuggestionBusiness.AddItem(userSuggestion);

                return ApiReturnModel.ReturnOk("提交成功");
            }

            catch (Exception e)
            {
                //记录日志
                LogHelp.WriteLog(e.Message, ApiFileDirectoryPara.ApiErrorDir);

                return ApiReturnModel.ReturnError("提交错误");
            }
        }
    }
}
