using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LS.UtilityTools
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class DateHelp
    {
        public static string GetDateToString(DateTime Date, string Conversion)
        {
            return Date.ToString(Conversion);
        }
    }
}
