using LS.BusinessServer.Business.Order;
using LS.BusinessServer.BusinessFactory;
using LS.BusinessServer.Model.Product;
using LS.BusinessServer.Model.Response;
using LS.DBServer.Model.Enum;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Common;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.ZhaoFa.Areas.Counterman.Controllers.Api
{
    /// <summary>
    /// 业务员 订单业务模块
    /// </summary>
    [CountermanAuthentication]
    public class CountermanOrderController : CountermanBaseController
    {
        /// <summary>
        /// 报价单业务层对象
        /// </summary>
        QuotationOrderBusiness quotationOrderBusiness = BusinessFactory.GetBusiness<QuotationOrderBusiness>();
        /// <summary>
        /// 询价单业务对象
        /// </summary>
        InquiryOrderBusiness inquiryOrderBusiness = BusinessFactory.GetBusiness<InquiryOrderBusiness>();

        /// <summary>
        /// 意向单 业务对象
        /// </summary>
        IntentionOrderBusiness intentionOrderBusiness = BusinessFactory.GetBusiness<IntentionOrderBusiness>();

        /// <summary>
        /// 合同单 业务对象
        /// </summary>
        ContractOrderBusiness contractOrderBusiness = BusinessFactory.GetBusiness<ContractOrderBusiness>();


        /// <summary>
        /// 业务员 提交报价单
        /// </summary>
        /// <param name="apiUserQuotationOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel UploadQuotationOrder([FromBody]ApiUserQuotationOrderModel apiUserQuotationOrderModel)
        {
            var userInfo = GetCurrentUserInfo();
            IList<BProductDetailModel> bProductDetailModelList = new List<BProductDetailModel>();
            foreach (var item in apiUserQuotationOrderModel.apiOrderProductModel)
            {
                var productModel = ApiToBusinessModelMapping.GetBProductDetailModelByApiUserProductModel(item);
                bProductDetailModelList.Add(productModel);
            }

            var msg = quotationOrderBusiness.AddQuotationOrder(apiUserQuotationOrderModel.OrderId, apiUserQuotationOrderModel.Remarks, userInfo.Id, apiUserQuotationOrderModel.ExpireTime, bProductDetailModelList);
            if (msg.Msg == BReturnModel.IsOk)
                return ApiReturnModel.ReturnOk();
            else
                return ApiReturnModel.ReturnError(msg.Msg);
        }

        /// <summary>
        /// 业务员 确认询价订单
        /// </summary>
        /// <param name="apiInquiryOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel ConfirmInquiryOrder([FromBody]ApiInquiryOrderModel apiInquiryOrderModel)
        {
            var userInfo = GetCurrentUserInfo();
            var msg = inquiryOrderBusiness.UpdateInquiryOrderFlag(apiInquiryOrderModel.Id, userInfo.Id, apiInquiryOrderModel.Remarks, BusinessOrderFlag.Effective, DateTime.Now);
            if (msg == BReturnModel.IsOk)
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError(msg);
        }

        /// <summary>
        /// 业务员 取消询价单
        /// </summary>
        /// <param name="apiIntentionOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel CancelIntentionOrder([FromBody]ApiInquiryOrderModel apiIntentionOrderModel)
        {
            var userInfo = GetCurrentUserInfo();
            var msg = inquiryOrderBusiness.UpdateInquiryOrderFlag(apiIntentionOrderModel.Id, userInfo.Id, apiIntentionOrderModel.Remarks, BusinessOrderFlag.Invalid);
            if (msg == BReturnModel.IsOk)
            {
                return ApiReturnModel.ReturnOk();
            }
            return ApiReturnModel.ReturnError(msg);
        }

        /// <summary>
        /// 业务员 确认意向订单
        /// </summary>
        /// <param name="apiIntentionOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel ConfirmIntentionOrder([FromBody]ApiIntentionOrderModel apiIntentionOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            var msg = intentionOrderBusiness.UpdateIntentionOrderFlag(apiIntentionOrderModel.Id, userInfo.Id, apiIntentionOrderModel.ReceiveRemarks, BusinessOrderFlag.Effective);

            if (msg == BReturnModel.IsOk)
                return ApiReturnModel.ReturnOk();
            else
                return ApiReturnModel.ReturnError(msg);
        }

        /// <summary>
        /// 业务员 判定意向单无效
        /// </summary>
        /// <param name="apiIntentionOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel CancelIntentionOrder([FromBody]ApiIntentionOrderModel apiIntentionOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            var msg = intentionOrderBusiness.UpdateIntentionOrderFlag(apiIntentionOrderModel.Id, userInfo.Id, apiIntentionOrderModel.ReceiveRemarks, BusinessOrderFlag.Effective);

            if (msg == BReturnModel.IsOk)
                return ApiReturnModel.ReturnOk();
            else
                return ApiReturnModel.ReturnError(msg);
        }


        /// <summary>
        /// 业务员 提交合同订单
        /// </summary>
        /// <param name="apiContractOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel UploadContractOrder([FromBody]ApiContractOrderModel apiContractOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            IList<BProductDetailModel> bProductDetailModelList = new List<BProductDetailModel>();
            foreach (var item in apiContractOrderModel.apiOrderProductModel)
            {
                var productModel = ApiToBusinessModelMapping.GetBProductDetailModelByApiUserProductModel(item);
                bProductDetailModelList.Add(productModel);
            }

            var BReturnModel =  contractOrderBusiness.AddContractOrder(apiContractOrderModel.OrderId, userInfo.Id, apiContractOrderModel.ExpireTime, bProductDetailModelList);

            if (BReturnModel.Code == BReturnModel.IsOk)

                return ApiReturnModel.ReturnOk();

            else
                return ApiReturnModel.ReturnError(BReturnModel.Msg);
        }

        /// <summary>
        /// 业务员 取消合同单
        /// </summary>
        /// <param name="apiContractOrderModel"></param>
        /// <returns></returns>
        public ApiReturnModel CancelContractOrder([FromBody]ApiContractOrderModel apiContractOrderModel)
        {
            var userInfo = GetCurrentUserInfo();

            var BReturnModel = contractOrderBusiness.UpdateContractOrderFlag(apiContractOrderModel.Id, userInfo.Id, apiContractOrderModel.Remarks, BusinessOrderFlag.Invalid);
            if (BReturnModel.IsOk == BReturnModel.Code)
                return ApiReturnModel.ReturnOk();
            return ApiReturnModel.ReturnError(BReturnModel.Msg);
        }
    }
}
