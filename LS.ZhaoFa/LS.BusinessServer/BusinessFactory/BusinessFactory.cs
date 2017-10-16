using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LS.BusinessServer.BusinessFactory
{
    public class BusinessFactory
    {
       /// <summary>
       /// 反射创建 现在看着和DAL工厂一样 但是还是要和业务层对象分开来 为后期需要改变时方便一点做准备
       /// 正常需要在config中 写加载的DLL路径 这里没做 只是个简单工厂 
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <returns></returns>
        public static T GetBusiness<T>() where T:class,new()
        {
            Type t = typeof(T);
            if (t.IsInterface)
            {
               throw new Exception("当前实例化的对象为接口");
            }
           return  Activator.CreateInstance(t) as T;
        }
    }
}
