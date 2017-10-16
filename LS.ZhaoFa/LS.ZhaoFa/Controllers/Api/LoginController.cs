using LS.BusinessServer;
using LS.DBServer.EF;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.UserCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LS.BusinessServer.BusinessFactory;
using LS.BusinessServer.Business;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.Models.Api.Enum;
using LS.DBServer.Model.Enum;

namespace LS.ZhaoFa.Controllers.Api
{
    /// <summary>
    /// 所有身份登录 业务入口
    /// </summary>
    public class LoginController : ApiController
    {
        UserBusiness UserInfo = BusinessFactory.GetBusiness<UserBusiness>();
        EmployeeBusiness employeeBusiness = BusinessFactory.GetBusiness<EmployeeBusiness>();
        /// <summary>
        /// 本站点 登陆获取授权token入口 (不包括第三方授权)
        /// </summary>
        /// <param name="apiLoginUserModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object UserLogin([FromBody]ApiLoginUserModel apiLoginUserModel)
        {
            var userInfo = UserInfo.GetItemByUserPwd(apiLoginUserModel.UserId, apiLoginUserModel.Pwd, (int)UserLvModel.User);
            if (userInfo != null) //密码登陆成功
            {
                var token = Guid.NewGuid();
                CacheHelper.TryAddCache($"{token}-{AuthenticationPara.UserAuthentication}", userInfo, DateTime.Now.AddDays(30)); //缓存 
                ApiUserInfoModel apiUserInfo = new ApiUserInfoModel()
                {
                    Token = token.ToString(),
                    UserName = userInfo.UserName,
                    UserLv = EnumHelper.GetDescriptionByValue<UserLvModel>(userInfo.UserLv)
                };
                return ApiReturnModel.ReturnOk("登陆成功", apiUserInfo);
            }


            return ApiReturnModel.ReturnError("用户名或者密码错误");

        }

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="apiLoginUserModel"></param>
        /// <returns></returns>
        public object AdminLogin([FromBody]ApiLoginUserModel apiLoginUserModel)
        {
            var userInfo = employeeBusiness.GetItemByEmployeePwd(apiLoginUserModel.UserId, apiLoginUserModel.Pwd, (int)UserLvModel.Admin);
            if (userInfo != null) //密码登陆成功
            {
                var token = Guid.NewGuid();
                CacheHelper.TryAddCache($"{token}-{AuthenticationPara.AdminAuthentication}", userInfo, DateTime.Now.AddDays(30)); //缓存 
                ApiUserInfoModel apiUserInfo = new ApiUserInfoModel()
                {
                    Token = token.ToString(),
                    UserName = userInfo.EmployeeName,
                    UserLv = EnumHelper.GetDescriptionByValue<UserLvModel>(userInfo.RoleLv) // .ToString()
                };
                return ApiReturnModel.ReturnOk("登陆成功", apiUserInfo);
            }


            return ApiReturnModel.ReturnError("用户名或者密码错误");
        }

        /// <summary>
        /// 业务员登陆
        /// </summary>
        /// <param name="apiLoginUserModel"></param>
        /// <returns></returns>
        public object CountermanLogin([FromBody]ApiLoginUserModel apiLoginUserModel)
        {
            var userInfo = employeeBusiness.GetItemByEmployeePwd(apiLoginUserModel.UserId, apiLoginUserModel.Pwd, (int)UserLvModel.Counterman);
            if (userInfo != null) //密码登陆成功
            {
                var token = Guid.NewGuid();
                CacheHelper.TryAddCache($"{token}-{AuthenticationPara.CountermanAuthentication}", userInfo, DateTime.Now.AddDays(30)); //缓存 
                ApiUserInfoModel apiUserInfo = new ApiUserInfoModel()
                {
                    Token = token.ToString(),
                    UserName = userInfo.EmployeeName,
                    UserLv = EnumHelper.GetDescriptionByValue<UserLvModel>(userInfo.RoleLv)
                };
                return ApiReturnModel.ReturnOk("登陆成功", apiUserInfo);
            }

            return ApiReturnModel.ReturnError("用户名或者密码错误");
        }

        /// <summary>
        /// 软文管理员 登陆
        /// </summary>
        /// <param name="apiLoginUserModel"></param>
        /// <returns></returns>
        public object SoftWenAdminLogin([FromBody] ApiLoginUserModel apiLoginUserModel)
        {
            var userInfo = employeeBusiness.GetItemByEmployeePwd(apiLoginUserModel.UserId, apiLoginUserModel.Pwd, (int)UserLvModel.SoftWenAdmin);
            if (userInfo != null) //密码登陆成功
            {
                var token = Guid.NewGuid();
                CacheHelper.TryAddCache($"{token}-{AuthenticationPara.SoftWenAdminAuthentication}", userInfo, DateTime.Now.AddDays(30)); //缓存 
                ApiUserInfoModel apiUserInfo = new ApiUserInfoModel()
                {
                    Token = token.ToString(),
                    UserName = userInfo.EmployeeName,
                    UserLv = EnumHelper.GetDescriptionByValue<UserLvModel>(userInfo.RoleLv)
                };
                return ApiReturnModel.ReturnOk("登陆成功", apiUserInfo);
            }

            return ApiReturnModel.ReturnError("用户名或者密码错误");
        }

    }
}
