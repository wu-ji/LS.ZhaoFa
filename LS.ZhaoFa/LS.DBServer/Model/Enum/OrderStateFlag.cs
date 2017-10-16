using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 系统订单 状态枚举
    /// </summary>
    public enum OrderStateFlag
    {
        /// <summary>
        /// 询价
        /// </summary>
        Inquiry = 1,
        /// <summary>
        /// 已经报价
        /// </summary>
        Offer = 2,
        /// <summary>
        /// 客户有意向
        /// </summary>
        Intention = 3,
        /// <summary>
        /// 合同已发出
        /// </summary>
        Contract = 4,
        /// <summary>
        /// 合同已签字 还未付款
        /// </summary>
        SignContract = 5,
        /// <summary>
        /// 合同已签字 且已付款
        /// </summary>
        PayedContract = 6,
        /// <summary>
        /// 已发货
        /// </summary>
        Delivery = 7,


        /// <summary>
        /// 已完成
        /// </summary>
        Complete = 10,

        /// <summary>
        /// 询价单失效 -1
        /// </summary>
        InquiryInvalid = -1,

        /// <summary>
        /// 报价过期
        /// </summary>
        OfferBeOverdue = -2,

        /// <summary>
        /// 客户意向终止
        /// </summary>
        UserTermination = -3,
        /// <summary>
        /// 合同过期
        /// </summary>
        ContractBeOverdue = -4,

        /// <summary>
        /// 用户取消合同
        /// </summary>
        UserCancelContract = -5


    }
}
