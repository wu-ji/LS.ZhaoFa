using LS.BusinessServer.Model.Product;
using LS.BusinessServer.Model.Response;
using LS.DBServer.DALToEF;
using LS.DBServer.EF;
using LS.DBServer.EF.DBFactory;
using LS.DBServer.IDAL;
using LS.DBServer.Model.Enum;
using LS.UtilityTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business.Order
{
    /// <summary>
    /// 流程订单业务
    /// </summary>
    public class UserOrderBusiness : BaseBusiness<UserOrder>
    {
        /// <summary>
        /// 询价 产品图片数据服务层对象
        /// </summary>
        IBaseDal<InquiryProductImg> inquiryProductImgDal = DBContentFacoty.GetDal<BaseDal<InquiryProductImg>>();
        /// <summary>
        /// 询价 产品数据服务层对象
        /// </summary>
        IBaseDal<InquiryOrder> inquiryOrderDal = DBContentFacoty.GetDal<BaseDal<InquiryOrder>>();
        /// <summary>
        /// 根据订单业务id和用户id 获取当前订单详细数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserOrder GetUserOrderDetailed(string orderId, Guid userId)
        {
            return baseDal.GetListQuery(item => item.BusinessOrderId == orderId && item.UserId == userId).FirstOrDefault();
        }

    }
}
