using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.Model.Config
{
    /// <summary>
    /// 用户订单 属性值 常量类
    /// </summary>
    public class UserOrderPropertiesConfig
    {
        /// <summary>
        /// Flag
        /// </summary>
        public readonly static string Flag = "Flag";

        /// <summary>
        /// Cancel
        /// </summary>
        public readonly static string CancelTime = "CancelTime";

        /// <summary>
        /// CurrentStateTime
        /// </summary>
        public readonly static string CurrentStateTime = "CurrentStateTime";


        /// <summary>
        /// 意向订单接收人
        /// </summary>
        public readonly static string IntentionReceiveUserId = "IntentionReceiveUserId";

        /// <summary>
        /// 接收时间
        /// </summary>
        public readonly static string ReceiveTime = "ReceiveTime";

        /// <summary>
        /// 订单接收备注
        /// </summary>
        public readonly static string ReceiveRemarks = "ReceiveRemarks";

        /// <summary>
        /// 订单最后处理人id
        /// </summary>
        public readonly static string FinalRecordUserId = "FinalRecordUserId";

        /// <summary>
        /// 订单最后处理备注
        /// </summary>
        public readonly static string FinalRecordRemarks = "FinalRecordRemarks";

        /// <summary>
        /// 最后处理时间
        /// </summary>
        public readonly static string FinalRecordTime = "FinalRecordTime";


        /// <summary>
        /// OriginalPrice 原价
        /// </summary>
        public readonly static string OriginalPrice = "OriginalPrice";

        /// <summary>
        /// RealisticPrice 现价
        /// </summary>
        public readonly static string RealisticPrice = "RealisticPrice";

    }
}
