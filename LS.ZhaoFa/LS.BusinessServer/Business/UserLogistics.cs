using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business
{
    /// <summary>
    /// 用户 物流业务
    /// </summary>
    public class UserLogisticsBusiness : BaseBusiness<UserLogistics>
    {
        /// <summary>
        /// 根据订单id 查询物流 返回所有物流数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserLogistics> GetUserLogisticsByOrderId(Guid orderId, Guid userId)
        {
            return baseDal.GetListQuery(list => list.OrderId == orderId && list.UserId == userId).ToList();
        }

        /// <summary>
        /// 修改 某个物流数据 
        /// </summary>
        /// <param name="id">物流数据id</param>
        /// <param name="CountermanId">操作员（业务员）id</param>
        /// <param name="detailedInformation">物流详细数据</param>
        /// <param name="date">当前物流时间（只是物流时间 不是产生数据的时间）</param>
        /// <returns></returns>
        public bool UpdateLogisticsById(Guid id, Guid countermanId, string detailedInformation, DateTime date)
        {
            UserLogistics userLogistics = new UserLogistics()
            {
                Id = id,
                CountermanId = countermanId,
                DetailedInformation = detailedInformation,
                LogisticsTime = date
            };

            int count = baseDal.UpdateList(item => item.Id == id, updateItem => new UserLogistics() { DetailedInformation = detailedInformation, LogisticsTime = date, CountermanId = countermanId });

            #region 另一种方法 
            //DBContent.Entry<UserLogistics>(userLogistics).State = System.Data.Entity.EntityState.Unchanged;
            //DBContent.Entry<UserLogistics>(userLogistics).Property(pro => pro.DetailedInformation).IsModified = true;
            //DBContent.Entry<UserLogistics>(userLogistics).Property(pro => pro.LogisticsTime).IsModified = true;
            //DBContent.Entry<UserLogistics>(userLogistics).Property(pro => pro.CountermanId).IsModified = true;
            //int count = DBContent.SaveChanges();
            #endregion

            if (count > 0)
                return true;
            return false;
        }
    }
}
