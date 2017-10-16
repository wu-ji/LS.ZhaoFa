using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business
{
    /// <summary>
    /// 员工业务模型
    /// </summary>
     public class EmployeeBusiness:BaseBusiness<Employee>
    {
        // <summary>
        /// 通过账号 和密码 获取具有对应等级的账号数据数据 (密码登陆业务)
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="lv">账号等级 1-普通用户 2-业务员 3-软文管理员 4- 系统管理员</param>
        /// <returns></returns>
        public Employee GetItemByEmployeePwd(string account, string pwd, int lv)
        {
            return baseDal.GetListQuery(item => item.Account == account && item.PassWord == pwd && item.RoleLv == lv).FirstOrDefault();
        }

    }
}
