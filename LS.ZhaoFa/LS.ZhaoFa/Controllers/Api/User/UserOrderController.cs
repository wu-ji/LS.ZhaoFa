using LS.BusinessServer.Business;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LS.BusinessServer.BusinessFactory;
using LS.ZhaoFa.Models.Api.Product;
using LS.BusinessServer.Business.Order;
using LS.BusinessServer.Model.Product;
using LS.ZhaoFa.Common;
using LS.BusinessServer.Model.Response;
using LS.DBServer.Model.Enum;

namespace LS.ZhaoFa.Controllers.Api.User
{
    /// <summary>
    /// 用户 订单业务模块
    /// </summary>
    [UserAuthentication]
    public class UserOrderController : UserBaseController
    {
        UserOrderBusiness userOrderBusiness = BusinessFactory.GetBusiness<UserOrderBusiness>();

        InquiryOrderBusiness inquiryOrderBusiness = BusinessFactory.GetBusiness<InquiryOrderBusiness>();

        IntentionOrderBusiness intentionOrderBusiness = BusinessFactory.GetBusiness<IntentionOrderBusiness>();

        ContractOrderBusiness contractOrderBusiness = BusinessFactory.GetBusiness<ContractOrderBusiness>();
        /// <summary>
        /// 用户提交 询价单
        /// </summary>
        /// <param name="apiUserProductModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel UploadInquiryOrder([FromBody]ApiUserProductModel apiUserProductModel)
        {
            var userInfo = GetCurrentUserInfo();
            if (inquiryOrderBusiness.AddUserInquiryOrder(userInfo.Id, apiUserProductModel.Remarks, apiUserProductModel.Price, apiUserProductModel.ImgUrl))
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError();
        }

        /// <summary>
        /// 用户提交 意向订单
        /// </summary>
        /// <param name="apiIntentionOrderModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel UploadIntentionOrder([FromBody]ApiIntentionOrderModel apiIntentionOrderModel)
        {
            var userInfo = GetCurrentUserInfo();
            IList<BProductDetailModel> bProductDetailModelList = new List<BProductDetailModel>();
            foreach (var item in apiIntentionOrderModel.apiOrderProductModel)
            {
                var productModel = ApiToBusinessModelMapping.GetBProductDetailModelByApiUserProductModel(item);
                bProductDetailModelList.Add(productModel);
            }
            var msg = intentionOrderBusiness.AddIntentionOrder(apiIntentionOrderModel.OrderId, userInfo.Id, apiIntentionOrderModel.Remarks, bProductDetailModelList);
            if (msg == BReturnModel.IsOk)
                return ApiReturnModel.ReturnOk();
            else
                return ApiReturnModel.ReturnError(msg);
        }

        /// <summary>
        /// 用户 取消意向单
        /// </summary>
        /// <param name="apiIntentionOrderModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel CancelIntentionOrder([FromBody]ApiIntentionOrderModel apiIntentionOrderModel)
        {
            var userInfo = GetCurrentUserInfo();
            var msg = intentionOrderBusiness.UpdateIntentionOrderFlag(apiIntentionOrderModel.Id, userInfo.Id, apiIntentionOrderModel.ReceiveRemarks, BusinessOrderFlag.Invalid, true);

            if (msg == BReturnModel.IsOk)
                return ApiReturnModel.ReturnOk();
            else
                return ApiReturnModel.ReturnError(msg);
        }

        /// <summary>
        /// 用户取消合同订单
        /// </summary>
        /// <param name="apiContractOrderModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel CancelContractOrder([FromBody]ApiContractOrderModel apiContractOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            var BReturnModel = contractOrderBusiness.UpdateContractOrderFlag(apiContractOrderModel.Id, userInfo.Id, apiContractOrderModel.Remarks, BusinessOrderFlag.Invalid, true);
            if (BReturnModel.IsOk == BReturnModel.Code)
                return ApiReturnModel.ReturnOk();
            return ApiReturnModel.ReturnError(BReturnModel.Msg);
        }

        /// <summary>
        /// 用户 接受合同(签字)
        /// </summary>
        /// <param name="apiContractOrderModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel ConfirmContractOrder([FromBody]ApiContractOrderModel apiContractOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            var BReturnModel = contractOrderBusiness.UpdateContractOrderFlag(apiContractOrderModel.Id, userInfo.Id, apiContractOrderModel.Remarks, BusinessOrderFlag.Effective, true);
            if (BReturnModel.IsOk == BReturnModel.Code)
                return ApiReturnModel.ReturnOk();
            return ApiReturnModel.ReturnError(BReturnModel.Msg);
        }
    }
}
