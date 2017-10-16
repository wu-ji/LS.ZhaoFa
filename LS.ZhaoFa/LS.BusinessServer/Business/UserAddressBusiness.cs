using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business
{
    public class UserAddressBusiness : BaseBusiness<UserAddress>
    {
        /// <summary>
        /// 用户设置 地址的属性(如 设置为默认地址 置顶等)
        /// </summary>
        /// <param name="id">地址id</param>
        /// <param name="userId">当前登录的用户id</param>
        /// <param name="flag">属性值</param>
        /// <returns></returns>
        public bool UpdateUserAddress(Guid id, Guid userId, int flag)
        {
            int count = baseDal.UpdateList(item => item.Id == id && item.UserId == userId, updateItem => new UserAddress() { Flag = flag });
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 修改用户地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserAddress(UserAddress model)
        {
            var userAddress = baseDal.GetListQuery(item => item.Id == model.Id && item.UserId == model.UserId).FirstOrDefault(); //检查该地址是否属于 当前登陆的用户
            if (userAddress != null)
            {
                model.Id = userAddress.Id; //限定id
                model.UserId = userAddress.UserId; //限定用户id

                baseDal.UpdateItem(model);

                int count = DBContent.SaveChanges();
                if (count > 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 删除用户地址 
        /// </summary>
        /// <param name="id">地址id</param>
        /// <param name="userId">当前登录的用户id</param>
        /// <returns></returns>
        public bool DeleteUserAddress(Guid id, Guid userId)
        {
            int count = baseDal.DeleteQuery(item => item.Id == id && item.UserId == userId);
            if (count > 0)
                return true;
            return false;
        }

       /// <summary>
       /// 用户地址 分页获取
       /// </summary>
       /// <param name="pageIndex">分页索引 0为第一页</param>
       /// <param name="pageSize">每页条数</param>
       /// <param name="userId">当前登陆的用户地址</param>
       /// <returns></returns>
        public IList<UserAddress> GetUserAddressPaging(int pageIndex, int pageSize,Guid userId)
        {
            return  baseDal.GetListQuery(list => list.UserId == userId).Skip(pageSize * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 根据用户id 获取所有地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserAddress> GetAllUserAddress(Guid userId)
        {
            return baseDal.GetListQuery(list => list.UserId == userId).ToList();
        }
    }
}
