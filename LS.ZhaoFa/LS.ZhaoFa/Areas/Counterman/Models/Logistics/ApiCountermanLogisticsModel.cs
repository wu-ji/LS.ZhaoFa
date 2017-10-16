using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Areas.Counterman.Models.Logistics
{
    /// <summary>
    /// 业务员 api层 物流数据模型
    /// </summary>
    public class ApiCountermanLogisticsModel
    {
        /// <summary>
        /// 物流数据id 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 物流数据对应的订单主键id (添加时必须)
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 详细物流数据 (如 包裹已到达温州分发站)
        /// </summary>
        public string DetailedInformation { get; set; }

        /// <summary>
        /// 该条物流数据 真实产生时间
        /// </summary>
        public DateTime LogisticsTime { get; set; }

    }
}