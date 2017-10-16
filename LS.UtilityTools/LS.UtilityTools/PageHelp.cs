using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools
{
    /// <summary>
    /// 页面帮助类 用来封装html的拼接 也可以写成js 放在前端拼接
    /// </summary>
    public class PageHelp
    {
        /// <summary>
        /// 分页 html模式一  外部ul样式 为pagination    li 样式为prev 
        /// </summary>
        /// <param name="url">跳转地址 注意需要将 url上的页码参数的值替换成“{0}” 如/index?pageIndex={0}</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageCount">总页数</param>
        /// <returns></returns>
        public static string GetPagingHtml(string url, int PageIndex, int PageCount)
        {
            int startPage = PageIndex - 2;
            int endPage = PageIndex + 2;
            if (startPage <= 0)
            {
                startPage = 1;
            }
            if (endPage > PageCount)
            {
                endPage = PageCount;
            }

            //StringBuilder
            StringBuilder html = new StringBuilder();

            html.Append(@"<ul class='pagination'>
            <li class='prev'>
                <a href='" + String.Format(url, 1) + @"'>首页</a>
           </li>
            <li class='prev'>
                <a href='" + String.Format(url, 1 == PageIndex ? 1 : PageIndex - 1) + @"'>上一页</a>
            </li>");
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == PageIndex)
                {
                    html.Append("<li><a style='color:blue' href='" + String.Format(url, i) + @"'>" + i + "</a></li>");
                }
                else
                {
                    html.Append(" <li><a href='" + String.Format(url, i) + @"'>" + i + "</a></li>");
                }
            }
            html.Append(
            @"<li class='next'>
                <a href='" + String.Format(url, PageCount == PageIndex ? PageCount : PageIndex + 1) + @"'>下一页</a>
            </li>
          <li class='prev'>
                <a href='" + String.Format(url, PageCount) + @"'>尾页(" + PageCount + @")</a>
           </li></ul>");

            return html.ToString(); ;
        }
    }
}
