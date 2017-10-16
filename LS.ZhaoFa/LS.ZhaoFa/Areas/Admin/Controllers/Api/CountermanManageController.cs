using LS.BusinessServer.Business;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.EF;
using LS.DBServer.Model.Enum;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Areas.Admin.Models.CountermanManage;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.ZhaoFa.Areas.Admin.Controllers.Api
{
    /// <summary>
    ///管理员 对 业务员管理 接口集合
    /// </summary>
    [AdminAuthentication]
    public class CountermanManageController : AdminBaseController
    {
        UserBusiness userBusiness = BusinessFactory.GetBusiness<UserBusiness>();
        /// <summary>
        /// 新增 业务员
        /// </summary>
        /// <param name="apiCountermanManagerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object AddCounterman([FromBody]ApiCountermanManagerModel apiCountermanManagerModel)
        {
            UserInfo userInfo = new UserInfo()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                UserLv = (int)UserLvModel.Counterman,
                UserAccount = apiCountermanManagerModel.UserAccount,
                UserPassWord = apiCountermanManagerModel.UserPassWord,
                UserName = apiCountermanManagerModel.UserName
            };
            userBusiness.AddItem(userInfo);
            return ApiReturnModel.ReturnOk();
        }

        /// <summary>
        /// 删除 业务员（目前为软删除(逻辑删除) 类似冻结账号）
        /// </summary>
        /// <param name="apiCountermanManagerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteCounterman([FromBody]ApiCountermanManagerModel apiCountermanManagerModel)
        {
            if (userBusiness.UpdateUserStateByIdAndLv(apiCountermanManagerModel.Id, (int)UserLvModel.Counterman, (int)UserFlag.Delete))
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError();
        }

        /// <summary>
        /// 修改业务员资料
        /// </summary>
        /// <param name="apiCountermanManagerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object UpdateCounterman([FromBody]ApiCountermanManagerModel apiCountermanManagerModel)
        {
            if (userBusiness.UpdateUserByIdAndLv(apiCountermanManagerModel.Id, apiCountermanManagerModel.UserPassWord, apiCountermanManagerModel.UserAccount, (int)UserLvModel.Counterman))
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError();
        }
    }
}
