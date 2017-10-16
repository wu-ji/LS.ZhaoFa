using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LS.UtilityTools
{
    /// <summary>
    /// 正则帮助类
    /// </summary>
    public class RegExpHelp
    {

        /// <summary>
        /// 匹配是否符合电话号码
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool VerificationTel(string tel)
        {
            Regex rg = new Regex(@"^1[358]\d{9}$");
            return rg.IsMatch(tel);
        }

        /// <summary>
        /// 密码强度匹配
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool VerificationPwd(string pwd)
        {
            Regex rg = new Regex(@"((?!^[0-9]+$)(?!^[a-zA-Z]+$))(^[0-9a-zA-Z]{6,}$)");
            return rg.IsMatch(pwd);
        }

        /// <summary>
        /// 为匹配的 字符 替换成目标字符
        /// </summary>
        /// <param name="data">要搜索匹配项字符串</param>
        /// <param name="regex">匹配的正则</param>
        /// <param name="resultData">替换字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string data, string regex, string resultData)
        {
            Regex rg = new Regex(regex);
            return Regex.Replace(data, regex, resultData);
        }

        public static string GetRedirectHost(string redirctUrl)
        {
            Regex eg = new Regex(@"^http:\/\/(.+?)\/");
            return eg.Match(redirctUrl).Groups[1].Value;
        }
    }
}
