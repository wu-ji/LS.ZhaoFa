using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.User
{
    /// <summary>
    /// api层 用户收货地址模型
    /// </summary>
    public class ApiUserAddressModel
    {
        /// <summary>
        /// 地址id 新增时忽略
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 市 非必须
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 省 非必须
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 详细地址 添加修改时必须
        /// </summary>

        public string DetailedAddress { get; set; }

        /// <summary>
        ///联系电话  添加修改时必须
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 收货人姓名 增加修改时 必须
        /// </summary>
        public string ConsigneeName { get; set; }
    }
}