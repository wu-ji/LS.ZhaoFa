
using LS.DBServer.DALToEF;
using LS.DBServer.EF;
using LS.DBServer.EF.DBFactory;
using LS.DBServer.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer
{
    /// <summary>
    /// 所有业务对象基类 如不满足具体业务需求请 单独建立class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseBusiness<T> where T : class, new()
    {
        protected IBaseDal<T> baseDal = DBContentFacoty.GetDal<BaseDal<T>>();
        protected LS_ZhaoFaEntities DBContent = DbFirstFactory.GetDBcontent();

        public T GetItemById(object id)
        {
            return baseDal.GetItemById(id);
        }

        public T AddItem(T model)
        {
            T resultModel = baseDal.Add(model);
            DBContent.SaveChanges();
            return resultModel;
        }

        public bool AddList(IList<T> model)
        {
            foreach (T item in model)
            {
                baseDal.Add(item);
            }
            DBContent.SaveChanges();
            return true;
        }

        public T UpdateItem(T model)
        {
            baseDal.UpdateItem(model);
            DBContent.SaveChanges();
            return model;
        }

        public bool DeleteItemById(T model)
        {
            baseDal.DeleteItemById(model);
            int count = DBContent.SaveChanges();
            if (count > 0)
                return true;
            return false;
        }

        public IList<T> GetAllList()
        {
            return baseDal.GetAllList().ToList();
        }
    }
}
