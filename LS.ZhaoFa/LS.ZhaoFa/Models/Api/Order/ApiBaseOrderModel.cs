using LS.ZhaoFa.Models.Api.Product;
using LS.ZhaoFa.Models.Api.UserCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 多有订单模型 基类
    /// </summary>
    public class ApiBaseOrderModel
    {
        /// <summary>
        /// 订单主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 关联的流程订单
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 生成时间(不需要提交 用来接收)
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 订单关联的 产品模型
        /// </summary>
        public IList<ApiUserProductModel> apiOrderProductModel { get; set; }

        /// <summary>
        /// 订单关联的用户(部分敏感字段数据不提供 请忽略)
        /// </summary>
        public ApiUserInfoModel apiUserInfoModel { get; set; }
    }
}