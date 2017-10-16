using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace LS.UtilityTools
{
    public class LogHelp
    {
        /// <summary>
        /// 全局日志锁
        /// </summary>
        private static object LogLock = new object();
        /// <summary>
        /// 错误日志队列
        /// </summary>
        private static Queue<LogQueueModel> LogQueue = new Queue<LogQueueModel>();

        /// <summary>
        /// 向队列添加错误
        /// </summary>
        /// <param name="logModel"></param>
        public static void AddLogQueue(LogQueueModel logModel)
        {
            lock (LogLock) //入队防止并发 这个对象貌似不是线程安全
            {
                LogQueue.Enqueue(logModel);
            }
        }

        /// <summary>
        /// 异步日志 处理 解决lock锁 阻塞线程
        /// </summary>
        public static void WriteLog()
        {
            #region 测试错误
            //AddLogQueue(new LogQueueModel()
            //{
            //    Msg = "测试错误",
            //    FileName = "ApiError"
            //});
            #endregion

            ThreadPool.QueueUserWorkItem(s =>
            {
                while (true)
                {
                    if (LogQueue.Count > 0)
                    {
                        //从队列里去读取信息
                        LogQueueModel contentTxt = LogQueue.Dequeue();
                        string upLoadPath = $"{ AppDomain.CurrentDomain.BaseDirectory}/{contentTxt.FileName}/";
                        upLoadPath += DateTime.Now.ToString("yyyy") + '/';
                        upLoadPath += DateTime.Now.ToString("MM") + '/';
                        if (!System.IO.Directory.Exists(upLoadPath))
                        {
                            System.IO.Directory.CreateDirectory(upLoadPath);
                        }
                        StringBuilder content = new StringBuilder();
                        content.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
                        content.Append("\r\n\t content:" + contentTxt.Msg);
                        content.Append("\r\n\t 本条记录由 日志队列 处理线程产生");
                        content.Append("\r\n--------------------------------------------------------------------------------------------------");
                        System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", content.ToString(), System.Text.Encoding.UTF8);
                    }
                    Thread.Sleep(5000);//5秒读一次
                }
            });

        }

        public static void WriteLog(HttpRequestBase Request, HttpResponseBase Response, HttpServerUtilityBase Server, Exception ex)
        {
            //创建路径 
            string upLoadPath = Server.MapPath("~/log/");
            upLoadPath += DateTime.Now.ToString("yyyy") + '/';
            upLoadPath += DateTime.Now.ToString("MM") + '/';
            if (!System.IO.Directory.Exists(upLoadPath))
            {
                System.IO.Directory.CreateDirectory(upLoadPath);
            }
            StringBuilder content = new StringBuilder();
            content.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            content.Append("\r\n.客户信息：");


            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            content.Append("\r\n\tIp:" + ip);
            content.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
            content.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            content.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
            content.Append("\r\n.错误信息：");
            content.Append("\r\n\t页面：" + Request.Url.ToString());
            content.Append("\r\n\t错误信息：" + ex.Message);
            content.Append("\r\n\t错误源：" + ex.Source);
            content.Append("\r\n\t异常方法：" + ex.TargetSite);
            content.Append("\r\n\t堆栈信息：" + ex.StackTrace);
            content.Append("\r\n--------------------------------------------------------------------------------------------------");
            //创建文件 写入错误 
            lock (LogLock)
            {
                System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", content.ToString(), System.Text.Encoding.UTF8);
            }


            Server.ClearError();

            //跳转至出错页面 
            //Response.Redirect("~/WeiXin/Error");
        }

        public static void WriteLogHost(string contentTxt, HttpServerUtilityBase Server)
        {
            string upLoadPath = Server.MapPath("~/log/");
            upLoadPath += DateTime.Now.ToString("yyyy") + '/';
            upLoadPath += DateTime.Now.ToString("MM") + '/';
            if (!System.IO.Directory.Exists(upLoadPath))
            {
                System.IO.Directory.CreateDirectory(upLoadPath);
            }
            StringBuilder content = new StringBuilder();
            content.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            content.Append("\r\n\t content:" + contentTxt);
            content.Append("\r\n\t 来自以上站点的 用户操作");
            content.Append("\r\n--------------------------------------------------------------------------------------------------");
            System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", content.ToString(), System.Text.Encoding.UTF8);
            Server.ClearError();
        }


        public static void WriteLogWebSocket(string contentTxt, string baseDir)
        {
            string upLoadPath = baseDir + "/SocketLog/";
            upLoadPath += DateTime.Now.ToString("yyyy") + '/';
            upLoadPath += DateTime.Now.ToString("MM") + '/';
            if (!System.IO.Directory.Exists(upLoadPath))
            {
                System.IO.Directory.CreateDirectory(upLoadPath);
            }
            StringBuilder content = new StringBuilder();
            content.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            content.Append("\r\n\t content:" + contentTxt);
            content.Append("\r\n\t 来自以上站点的 用户操作");
            content.Append("\r\n--------------------------------------------------------------------------------------------------");
            System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", content.ToString(), System.Text.Encoding.UTF8);
            //Server.ClearError();
        }

        /// <summary>
        /// 记录日志 
        /// </summary>
        /// <param name="contentTxt"></param>
        /// <param name="FileName">根文件夹名称 内部用日期区分</param>
        public static void WriteLog(string contentTxt, string FileName)
        {
            string upLoadPath = $"{ AppDomain.CurrentDomain.BaseDirectory}/{FileName}/";
            upLoadPath += DateTime.Now.ToString("yyyy") + '/';
            upLoadPath += DateTime.Now.ToString("MM") + '/';
            if (!System.IO.Directory.Exists(upLoadPath))
            {
                System.IO.Directory.CreateDirectory(upLoadPath);
            }
            StringBuilder content = new StringBuilder();
            content.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            content.Append("\r\n\t content:" + contentTxt);
            content.Append("\r\n\t 来自以上站点的 用户操作");
            content.Append("\r\n--------------------------------------------------------------------------------------------------");
            lock (LogLock)
            {
                System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", content.ToString(), System.Text.Encoding.UTF8);
            }

        }
    }

    /// <summary>
    /// 日志队列 对象
    /// </summary>
    public class LogQueueModel
    {
        /// <summary>
        /// 日志消息
        /// </summary>
        public string Msg { get; set; }

        public string FileName { get; set; }
    }
}
