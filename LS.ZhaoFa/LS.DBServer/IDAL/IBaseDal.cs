using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LS.DBServer.IDAL
{
    public interface IBaseDal<T>
    {
      
        /// <summary>
        /// 添加单个模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Add(T model);

        /// <summary>
        /// 修改单个模型
        /// </summary>
        /// <param name="model">模型对象 必须有id</param>
        void UpdateItem(T model);

        /// <summary>
        /// 修改单个模型 不缓存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="deleteModel"></param>
        void UpdataItemDetached(T model,T deleteModel);

        /// <summary>
        /// 修改单个模型 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="properties">需要修改的字段</param>
        void UpdateItemSelect(T model, string[] properties);
        /// <summary>
        /// 修改 多个模型(集合)
        /// </summary>
        /// <param name="where">模型的筛选条件</param>
        /// <param name="update">x=> new x{修改字段=修改的值 } 修改后的数据</param>
        /// <returns></returns>
        int UpdateList(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, T>> update);

        /// <summary>
        /// 根据 条件删除
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <returns></returns>
        int DeleteQuery(System.Linq.Expressions.Expression<Func<T, bool>> where);

        /// <summary>
        /// 删除单个模型
        /// </summary>
        /// <param name="Model"></param>
        void DeleteItemById(T Model);

        /// <summary>
        /// 根据id获得 单个数据模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetItemById(object id);

        /// <summary>
        /// 根据条件查找 对应的数据集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> GetListQuery(System.Linq.Expressions.Expression<Func<T, bool>> where);

        /// <summary>
        /// 获得所有数据(数据太多 服务器内存会炸)
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllList();

        
    }
}
