using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.User
{
    /// <summary>
    /// api层 用户意见模型
    /// </summary>
    public class ApiSuggestionModel
    {
        /// <summary>
        /// 意见内容 用户提交时必须
        /// </summary>
        public string Msg { get; set; }

       
    }
}