using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LS.DBServer.EF.DBFactory
{
    public class DBContentFacoty
    {
       

        public static T GetDal<T>() where T : class,new()
        {
            Type t = typeof(T);
            if (t.IsInterface)
            {
                throw new Exception("当前实例化的对象为接口");
            }
            return Activator.CreateInstance(t) as T;
        }
    }
}
