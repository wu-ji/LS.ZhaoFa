using System.Configuration;
using System.Text;
using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LS.UtilityTools
{
    /// <summary>
    /// 邮件服务类
    /// </summary>
    /// 
    public class MailHelper
    {
        private readonly string _mailServer = ConfigurationManager.AppSettings["EmailStmp"]; //邮件的服务器如smtp.qq.com

        private readonly string _mailName = ConfigurationManager.AppSettings["EmailUserName"];

        private readonly string _mailPassword = ConfigurationManager.AppSettings["EmailPassword"];//密码

        #region 单一实例

        private static MailHelper _instance = null;
        /// <summary>
        /// 单一实例
        /// </summary>
        public static MailHelper instance
        {
            get
            {
                if (_instance == null) _instance = new MailHelper();
                return _instance;
            }
        }

        #endregion


        private MailHelper()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipients">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public void SendMail(string recipients, string subject, string body)
        {
            SmtpClient mailClient = new SmtpClient(_mailServer, 587);
            mailClient.Credentials = new NetworkCredential(_mailName, _mailPassword);
            MailMessage mailMessage = new MailMessage(_mailName, recipients, subject, body);
            mailMessage.BodyEncoding = Encoding.UTF8;
            //mailClient.Timeout = 5000;
            //mailClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
            mailMessage.IsBodyHtml = true;
            try
            {
                //mailClient.Send(mailMessage);
                mailClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    LogHelp.WriteLog(ex.InnerException.Message, "EmailLog");
                else
                    LogHelp.WriteLog(ex.Message, "EmailLog");
            }
        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {


            //string msg = e.UserState.ToString();
            SmtpClient smtp = (SmtpClient)sender;

            if (e.Error == null)//问题出现后，这里的Error并没有错误
            {
                //msg += ",1";//发送成功
            }
            else
            {
                //msg += ",0";
                smtp.SendAsyncCancel();
            }
            //if (lvMailTo.InvokeRequired)
            //{
            //    lvMailTo.BeginInvoke(new UpdateListDelegate(UpdateList), msg);
            //}
            smtp.Dispose();
            //e.Set();
        }
    }
}