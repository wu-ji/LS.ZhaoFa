
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using LS.DBServer.IDAL;
using LS.DBServer.EF.DBFactory;
using LS.DBServer.EF;

namespace LS.DBServer.DALToEF
{
    public class BaseDal<T> : IBaseDal<T> where T : class,new()
    {
        public LS_ZhaoFaEntities dbContent = DbFirstFactory.GetDBcontent();

        /// <summary>
        /// 添加 模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Add(T model)
        {
            return dbContent.Set<T>().Add(model);
        }

        /// <summary>
        /// 修改单个对象模型
        /// </summary>
        /// <param name="model"></param>
        public void UpdateItem(T model)
        {
            dbContent.Entry(model).State = EntityState.Modified;
        }
        public void UpdataItemDetached(T model, T deleteModel)
        {
            dbContent.Entry<T>(deleteModel).State = EntityState.Detached;
            dbContent.Entry(model).State = EntityState.Modified;
        }

        public void UpdateItemSelect(T model, string[] properties)
        {
            var entry = dbContent.Entry(model);
            if (entry.State == EntityState.Detached)
            {
                dbContent.Set<T>().Attach(model);
            }
            foreach (var propertyName in properties)
            {
                entry.Property(propertyName).IsModified = true;
            }
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public int UpdateList(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, T>> update)
        {
            return dbContent.Set<T>().Where(where).Update<T>(update);
        }

        //public int UpdateList(IQueryable<T> where, System.Linq.Expressions.Expression<Func<T, T>> update)
        //{
        //    return dbContent.Set<T>().Update<T>(where, update);
        //}

        public int DeleteQuery(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return dbContent.Set<T>().Where(where).Delete();
        }



        public T GetItemById(object id)
        {
            return dbContent.Set<T>().Find(id);
        }




        public IQueryable<T> GetListQuery(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return dbContent.Set<T>().Where(where);
        }


        public IQueryable<T> GetAllList()
        {
            return dbContent.Set<T>().Where(item => true);
        }





        public void DeleteItemById(T Model)
        {
            dbContent.Entry(Model).State = EntityState.Deleted;
        }
    }
}
