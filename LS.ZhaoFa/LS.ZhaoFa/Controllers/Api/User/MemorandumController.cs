using LS.BusinessServer;
using LS.DBServer.EF;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Models.Api.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LS.BusinessServer.BusinessFactory;
using LS.ZhaoFa.Models.Api.Common;
using LS.BusinessServer.Business;

namespace LS.ZhaoFa.Controllers.Api.User
{
    /// <summary>
    /// 用户备忘录业务
    /// </summary>
    [UserAuthenticationAttribute]
    public class MemorandumController : UserBaseController
    {
        MemorandumBusiness UserMemorandumBusiness = BusinessFactory.GetBusiness<MemorandumBusiness>();

        /// <summary>
        /// 用户添加备忘录
        /// </summary>
        /// <param name="apiMemorandumModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object AddMemorandum([FromBody]ApiMemorandumModel apiMemorandumModel)
        {
            var userInfo = HttpContext.Current.Items[ApiCachePara.CacheUserKey] as UserInfo;

            UserMemorandum userMemorandum = new UserMemorandum()
            {
                Id = Guid.NewGuid(),
                Msg = apiMemorandumModel.Msg,
                UserId = userInfo.Id,
                CreateTime = DateTime.Now,
                AffairsTime = apiMemorandumModel.AffairsTime
            };

            try
            {
                UserMemorandumBusiness.AddItem(userMemorandum);
                return ApiReturnModel.ReturnOk("添加备忘录成功");
            }
            catch (Exception ex)
            {
                //记录日志

                return ApiReturnModel.ReturnError("添加备忘录失败 :" + ex.Message);
            }
        }

        /// <summary>
        /// 删除 用户某个备忘记录
        /// </summary>
        /// <param name="apiMemorandumModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object DeleteMemorandum([FromBody]ApiMemorandumModel apiMemorandumModel)
        {
            var userInfo = HttpContext.Current.Items[ApiCachePara.CacheUserKey] as UserInfo;
            var memModel = UserMemorandumBusiness.GetItemByUserAndId(apiMemorandumModel.Id, userInfo.Id);
            if (memModel != null)
            {
                try
                {
                    if (UserMemorandumBusiness.DeleteItemById(memModel))
                    {
                        return ApiReturnModel.ReturnOk("删除成功");
                    }
                }
                catch (Exception ex)
                {
                    //记录日志

                    return ApiReturnModel.ReturnError("删除失败:");
                }
            }

            return ApiReturnModel.ReturnError("未找到对应数据 请刷新重试");
        }

        /// <summary>
        /// 修改某个 备忘记录
        /// </summary>
        /// <param name="apiMemorandumModel"></param>
        /// <returns></returns>
        public object UpdateMemorandumById([FromBody]ApiMemorandumModel apiMemorandumModel)
        {
            var userInfo = HttpContext.Current.Items[ApiCachePara.CacheUserKey] as UserInfo;

            if (UserMemorandumBusiness.UpdateItemByIdAndUserId(apiMemorandumModel.Id, userInfo.Id, apiMemorandumModel.Msg, apiMemorandumModel.AffairsTime))
            {
                return ApiReturnModel.ReturnOk("修改成功");
            }
            return ApiReturnModel.ReturnError("修改失败");
        }

        /// <summary>
        /// 根据时间 查找用户的备忘数据
        /// </summary>
        /// <param name="apiMemorandumModel"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetMemorandumByTime([FromBody]ApiMemorandumModel apiMemorandumModel)
        {
            var userInfo = HttpContext.Current.Items[ApiCachePara.CacheUserKey] as UserInfo;

            try
            {
                var memList = UserMemorandumBusiness.GetListByUserIdAndTime(userInfo.Id, apiMemorandumModel.AffairsTime);

                return ApiReturnModel.ReturnOk("查找成功", memList);
            }
            catch (Exception e)
            {
                return ApiReturnModel.ReturnError("查找失败 :" + e.Message);
            }
        }
    }
}
