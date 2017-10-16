using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business
{
    public class UserBusiness : BaseBusiness<UserInfo>
    {
        /// <summary>
        /// 通过账号获取数据
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserInfo GetItemByUserAccount(string account)
        {
            return baseDal.GetListQuery(item => item.UserAccount == account).FirstOrDefault();
        }

        /// <summary>
        /// 通过账号 和密码 获取数据 (密码登陆业务)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserInfo GetItemByUserPwd(string account, string pwd)
        {
            return baseDal.GetListQuery(item => item.UserAccount == account && item.UserPassWord == pwd).FirstOrDefault();
        }

        /// <summary>
        /// 通过账号 和密码 获取具有对应等级的账号数据数据 (密码登陆业务)
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="lv">账号等级 1-普通用户 2-业务员 3-软文管理员 4- 系统管理员</param>
        /// <returns></returns>
        public UserInfo GetItemByUserPwd(string account, string pwd, int lv)
        {
            return baseDal.GetListQuery(item => item.UserAccount == account && item.UserPassWord == pwd && item.UserLv == lv).FirstOrDefault();
        }

        /// <summary>
        /// 修改用户状态 根据id和用户等级 （包括 软删除恢复正常等）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lv"></param>
        /// <returns></returns>
        public bool UpdateUserStateByIdAndLv(Guid id, int lv,int flag)
        {
           int count = baseDal.UpdateList(item => item.Id == id && item.UserLv == lv, updateItem => new UserInfo() { Flag = flag });
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 修改用户数据
        /// </summary>
        /// <param name="id">目标用户id</param>
        /// <param name="pwd">需要修改的 密码</param>
        /// <param name="account">需要修改的 账号</param>
        /// <param name="lv">账号等级 1-普通用户 2-业务员 3-软文管理员 4- 系统管理员</param>
        /// <returns></returns>
        public bool UpdateUserByIdAndLv(Guid id, string pwd, string account,int lv)
        {
           int count =  baseDal.UpdateList(item => item.Id == id && item.UserLv == lv, updateItem => new UserInfo() { UserPassWord = pwd, UserAccount = account });
            if (count > 0)
                return true;
            return false;
        }
    }
}
