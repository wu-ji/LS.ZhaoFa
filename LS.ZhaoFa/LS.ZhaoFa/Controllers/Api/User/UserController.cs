using LS.BusinessServer;
using LS.BusinessServer.Business;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.EF;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.User;
using LS.ZhaoFa.Models.Api.UserCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.ZhaoFa.Controllers.Api.User
{
    /// <summary>
    /// 用户业务
    /// </summary>
    [UserAuthentication]
    public class UserController : UserBaseController
    {
        UserBusiness UserBusiness = BusinessFactory.GetBusiness<UserBusiness>();
        UserAddressBusiness UserAddressBusiness = BusinessFactory.GetBusiness<UserAddressBusiness>();

        BaseBusiness<UserRole> UserRoleBusiness = BusinessFactory.GetBusiness<BaseBusiness<UserRole>>();

        /// <summary>
        /// 用户基础信息修改
        /// </summary>
        /// <param name="apiUserInfoModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel UpdateUserBasicsData(ApiUserInfoModel apiUserInfoModel)
        {
            var userInfo = GetCurrentUserInfo();

            userInfo.CompanyName = apiUserInfoModel.CompanyName;
            userInfo.UserImg = apiUserInfoModel.UserImg;
            userInfo.UserPassWord = apiUserInfoModel.UserPwd;

            UserBusiness.UpdateItem(userInfo);

            return ApiReturnModel.ReturnOk("修改成功");
        }

        /// <summary>
        /// 获取当前登陆用户的基本信息 token存放在请求头中的Authorization里
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel GetUserBasicsData()
        {
            var userInfo = GetCurrentUserInfo();

            userInfo = UserBusiness.GetItemById(userInfo.Id);

            var token = Request.Headers.Authorization.Parameter;
            CacheHelper.TryAddCache($"{token}-{AuthenticationPara.UserAuthentication}", userInfo, DateTime.Now.AddDays(30)); //更新缓存 

            ApiUserInfoModel apiUserInfoModel = new ApiUserInfoModel()
            {
                CompanyName = userInfo.CompanyName,
                UserName = userInfo.UserName,
                UserLv = UserRoleBusiness.GetItemById(userInfo.UserLv).RoleName
            };
            return ApiReturnModel.ReturnOk("查询成功", apiUserInfoModel);
        }

        /// <summary>
        /// 增加用户收货地址
        /// </summary>
        /// <param name="apiUserAddressModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel AddUserAddress([FromBody]ApiUserAddressModel apiUserAddressModel)
        {
            var userInfo = GetCurrentUserInfo();
            UserAddress userAddress = new UserAddress()
            {
                Id = Guid.NewGuid(),
                UserId = userInfo.Id,
                City = apiUserAddressModel.City,
                Province = apiUserAddressModel.Province,
                DetailedAddress = apiUserAddressModel.DetailedAddress,
                ConsigneeName = apiUserAddressModel.ConsigneeName,
                Flag = 0,
                Phone = apiUserAddressModel.Phone
            };

            userAddress = UserAddressBusiness.AddItem(userAddress);
            return ApiReturnModel.ReturnOk("增加成功", userAddress);
        }

        /// <summary>
        /// 删除用户地址 (目前为物理删除)
        /// </summary>
        /// <param name="apiUserAddressModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel DeleteUserAddress([FromBody] ApiUserAddressModel apiUserAddressModel)
        {
            var userInfo = GetCurrentUserInfo();


            if (UserAddressBusiness.DeleteUserAddress(apiUserAddressModel.Id, userInfo.Id))
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError();
        }

        /// <summary>
        /// 修改用户地址
        /// </summary>
        /// <param name="apiUserAddressModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel UpdateUserAddress([FromBody] ApiUserAddressModel apiUserAddressModel)
        {
            var userInfo = GetCurrentUserInfo();
            UserAddress userAddress = new UserAddress()
            {
                Id = apiUserAddressModel.Id,
                UserId = userInfo.Id,
                City = apiUserAddressModel.City,
                Province = apiUserAddressModel.Province,
                DetailedAddress = apiUserAddressModel.DetailedAddress,
                ConsigneeName = apiUserAddressModel.ConsigneeName,
                Flag = 0,
                Phone = apiUserAddressModel.Phone
            };

            if (UserAddressBusiness.UpdateUserAddress(userAddress))
                return ApiReturnModel.ReturnOk();
            return ApiReturnModel.ReturnError();
        }

        /// <summary>
        /// 获取 用户地址列表 （不分页获取所有数据 参数全部传0）
        /// </summary>
        /// <param name="pageIndex">分页索引 标志当前第几页</param>
        /// <param name="pageSize">每页的条数 默认值为5</param>
        /// <returns></returns>
        [HttpGet]
        public ApiReturnModel GetUserAddressList(int pageIndex, int pageSize = 5)
        {
            var userInfo = GetCurrentUserInfo();
            if (pageIndex < 0 || pageSize < 0)
            {
                return ApiReturnModel.ReturnError("参数值不能小于0");
            }

            if (pageSize == 0)
            {
                var allAddress = UserAddressBusiness.GetAllUserAddress(userInfo.Id);

                return ApiReturnModel.ReturnOk("获取成功", allAddress);
            }
            else
            {
                var allAddress = UserAddressBusiness.GetUserAddressPaging(pageIndex, pageSize, userInfo.Id);
                return ApiReturnModel.ReturnOk("分页获取成功", allAddress);
            }
        }
    }
}
