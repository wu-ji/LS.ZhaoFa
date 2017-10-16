using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 用户地址标记
    /// </summary>
    public enum UserAddressFlag
    { 
        /// <summary>
        /// 普通地址
        /// </summary>
        Ordinary = 0,

        /// <summary>
        /// 默认地址
        /// </summary>
        @Default = 1
    }
}