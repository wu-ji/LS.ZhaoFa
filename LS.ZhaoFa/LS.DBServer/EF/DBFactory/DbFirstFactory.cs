
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LS.DBServer.EF.DBFactory
{
    public class DbFirstFactory
    {
        /// <summary>
        /// 测试 使用的 全局单例(正式环境中 必须保证单一请求线程内 数据对象LS_ZhaoFaEntities的单一实例 放在HttpContext.Current.Items中 即可)
        /// </summary>
        private static LS_ZhaoFaEntities Db = new LS_ZhaoFaEntities();
        /// <summary>
        /// 数据上下文对象产生方法
        /// </summary>
        /// <returns></returns>
        public static LS_ZhaoFaEntities GetDBcontent()
        {
            if (HttpContext.Current == null) //给单元测试使用
            {
                return Db;
            }
            var obj = HttpContext.Current.Items["DBContent"];
            if (obj != null)
            {
                return obj as LS_ZhaoFaEntities;
            }
            else
            {
                HttpContext.Current.Items["DBContent"] = new LS_ZhaoFaEntities();
                return HttpContext.Current.Items["DBContent"] as LS_ZhaoFaEntities;
            }
        }
    }
}
