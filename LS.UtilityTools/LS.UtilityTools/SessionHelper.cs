using System.Web;

namespace LS.UtilityTools
{
    public class SessionHelper
    {
      
    
        /// <summary>
        /// 添加Session，调动有效期为20分钟
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <param name="value">Session值</param>
        public static void Add(string name, object value)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, value);
            HttpContext.Current.Session.Timeout = 20;
        }
 
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <param name="value">Session值</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(string name, object value, int iExpires)
        {
            HttpContext.Current.Session[name] = value;
            HttpContext.Current.Session.Timeout = iExpires;
        }

     

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="name">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string name)
        {
            if (HttpContext.Current.Session[name] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[name];
            }
        }

        

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="name">Session对象名称</param>
        public static void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        } 
    }
}