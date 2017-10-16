using LS.BusinessServer.Business;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.Enum;
using LS.ZhaoFa.Models.Api.UserCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.EF;
using LS.ZhaoFa.Common;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.DBServer.Model.Enum;

namespace LS.ZhaoFa.Controllers.Api
{
    /// <summary>
    /// 注册 业务入口
    /// </summary>
    public class RegisterController : ApiController
    {
        UserBusiness UserBusiness = BusinessFactory.GetBusiness<UserBusiness>();
       /// <summary>
       /// 用户注册
       /// </summary>
       /// <param name="register"></param>
       /// <returns></returns>
       [HttpPost]
        public object UserRegister(ApiUserRegisterModel register)
        {
            if (register.Type == ApiRegisterFlag.ByPwd) //通过密码
            {
                var isHave = UserBusiness.GetItemByUserAccount(register.UserAccount);
                if (isHave != null)
                {
                    return ApiReturnModel.ReturnError("当前账号已经注册");
                }
                var userInfo = ApiToDalModelMapping.UserRegisterModelToUserInfo(register,(int)UserLvModel.User);

                userInfo = UserBusiness.AddItem(userInfo);

                #region 开始写入缓存

                var token = Guid.NewGuid();
                CacheHelper.TryAddCache($"{token}-{AuthenticationPara.UserAuthentication}", userInfo, DateTime.Now.AddDays(30)); //缓存 

                ApiUserInfoModel apiUserInfo = new ApiUserInfoModel()
                {
                    Token = token.ToString(),
                    UserAccount = userInfo.UserAccount,
                    UserLv = userInfo.UserLv.ToString()
                };

                #endregion
                
                ApiReturnModel.ReturnOk("注册成功", apiUserInfo);
            }
            else if (register.Type == ApiRegisterFlag.ByValidate) //通过验证码注册
            {
                
            }

            return ApiReturnModel.ReturnOk();
        }
    }
}
