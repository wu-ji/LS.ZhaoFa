using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LS.UtilityTools
{
    /// <summary>
    /// 订单帮助类
    /// </summary>
    public class OrderHelp
    {
        /// <summary>
        /// 为订单末尾生成随机诺干位数字
        /// </summary>
        /// <returns>返回 年月日时分秒和指定长度的随机数</returns>
        public static string GetOrderIdOne(int length) 
        {
            string start = DateHelp.GetDateToString(DateTime.Now,"yyyyMMddhhmmss");
            string result =start + OtherHelper.GetRadomQuantity(length);
            return result;
        }

    }
}
