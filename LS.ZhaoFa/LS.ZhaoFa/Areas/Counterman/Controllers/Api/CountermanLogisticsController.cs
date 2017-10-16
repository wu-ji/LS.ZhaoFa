using LS.BusinessServer.Business;
using LS.BusinessServer.Business.Order;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.EF;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Areas.Counterman.Models.Logistics;
using LS.ZhaoFa.Models.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.ZhaoFa.Areas.Counterman.Controllers.Api
{
    /// <summary>
    /// 业务员 物流业务接口模块
    /// </summary>
    [CountermanAuthentication]
    public class CountermanLogisticsController : CountermanBaseController
    {
        UserLogisticsBusiness userLogisticsBusiness = BusinessFactory.GetBusiness<UserLogisticsBusiness>();
        UserOrderBusiness userOrderBusiness = BusinessFactory.GetBusiness<UserOrderBusiness>();
        /// <summary>
        /// 业务员新增 物流数据
        /// </summary>
        /// <param name="apiCountermanLogisticsModel"></param>
        /// <returns></returns>
        public object AddLogistics([FromBody]ApiCountermanLogisticsModel apiCountermanLogisticsModel)
        {
            var userInfo = GetCurrentUserInfo();
            var orderInfo = userOrderBusiness.GetItemById(apiCountermanLogisticsModel.OrderId);
            UserLogistics userLogistics = new UserLogistics()
            {
                Id = Guid.NewGuid(),
                OrderId = orderInfo.Id,
                UserId = orderInfo.UserId,
                CreateTime = DateTime.Now,
                LogisticsTime = apiCountermanLogisticsModel.LogisticsTime,
                DetailedInformation = apiCountermanLogisticsModel.DetailedInformation,
                AdminId = userInfo.Id
            };
       
            userLogisticsBusiness.AddItem(userLogistics);
            return ApiReturnModel.ReturnOk();
        }

        /// <summary>
        /// 修改 某个物流信息
        /// </summary>
        /// <param name="apiCountermanLogisticsModel"></param>
        /// <returns></returns>
        public ApiReturnModel UpdateLogisticsItem([FromBody]ApiCountermanLogisticsModel apiCountermanLogisticsModel)
        {
            var userInfo = GetCurrentUserInfo();
            if (userLogisticsBusiness.UpdateLogisticsById(apiCountermanLogisticsModel.Id, userInfo.Id, apiCountermanLogisticsModel.DetailedInformation, apiCountermanLogisticsModel.LogisticsTime))
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError();
        }
    }
}
