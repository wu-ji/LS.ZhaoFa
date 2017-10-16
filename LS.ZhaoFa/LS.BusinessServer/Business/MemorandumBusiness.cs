using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business
{
    public class MemorandumBusiness : BaseBusiness<UserMemorandum>
    {
        /// <summary>
        /// 根据id 和用户id查找对应的 备忘数据
        /// </summary>
        /// <param name="id">备忘表id</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public UserMemorandum GetItemByUserAndId(Guid id, Guid userId)
        {
            return baseDal.GetListQuery(item => item.Id == id && item.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// 获取备忘消息集合
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<UserMemorandum> GetListByUserIdAndTime(Guid userId, DateTime date)
        {
            return baseDal.GetListQuery(list => list.UserId == userId && list.AffairsTime.Year == date.Year && list.AffairsTime.Month == date.Month && list.AffairsTime.Day == date.Day).ToList();
        }

        public bool UpdateItemByIdAndUserId(Guid id, Guid userId, string Msg, DateTime AffairsTime)
        {
          int count =  baseDal.UpdateList(item => item.Id == id && item.UserId == userId, model => new UserMemorandum() { Msg = Msg, AffairsTime = AffairsTime });
            if (count > 0)
                return true;
            else
                return false;
        }
    }
}
