using LS.DBServer.EF;
using LS.DBServer.Model.Enum;
using LS.ZhaoFa.Models.Api.Enum;
using LS.ZhaoFa.Models.Api.UserCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Common
{
    /// <summary>
    /// api层模型 和数据层模型 属性值转换 !!(后期换成 AutoMapper)
    /// </summary>
    public class ApiToDalModelMapping
    {
        /// <summary>
        /// 转换方式
        /// </summary>
        private static readonly int mappingType = 0;
        /// <summary>
        /// 将api 用户注册模型与数据层用户模型 转换
        /// </summary>
        /// <param name="apiUserRegisterModel"></param>
        /// <param name="userLv"></param>
        /// <returns></returns>
        public static UserInfo UserRegisterModelToUserInfo(ApiUserRegisterModel apiUserRegisterModel,int userLv)
        {
            UserInfo userInfo = new UserInfo();
               
            if (mappingType == 0)
            {
                userInfo.Id = Guid.NewGuid();
                userInfo.UserAccount = apiUserRegisterModel.UserAccount;
                userInfo.UserName = apiUserRegisterModel.UserName;
                userInfo.UserPassWord = apiUserRegisterModel.UserPwd;
                userInfo.Flag = (int)UserFlag.Normal;
                userInfo.UserLv = userLv;
            }

            return userInfo;
        }
    }
}