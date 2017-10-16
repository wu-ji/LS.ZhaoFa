using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.User
{
    /// <summary>
    /// 用户备忘模型
    /// </summary>
    public class ApiMemorandumModel
    {
        /// <summary>
        /// 备忘消息 添加时必须
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 备忘事务日期 添加修改时必须 
        /// </summary>
        public DateTime AffairsTime { get; set; }

        /// <summary>
        /// 备忘表id 修改删除和 按id查询时必须
        /// </summary>
        public Guid Id { get; set; }
    }
}