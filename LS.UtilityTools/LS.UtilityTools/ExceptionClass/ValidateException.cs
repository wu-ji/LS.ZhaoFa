using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools.ExceptionClass
{
    /// <summary>
    /// 验证错误
    /// </summary>
    public class ValidateException : System.Exception
    {
        public ValidateException(string msg) : base(msg)
        {

        }
    }
}
