
using LS.UtilityTools.ExceptionClass;
using LS.ZhaoFa.Models.Api.Order;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LS.ZhaoFa.Controllers.Api
{
    /// <summary>
    /// 测试 接口入口
    /// </summary>
    public class TestController : ApiController
    {
        /// <summary>
        /// 错误测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object TestError()
        {
            throw new ValidateException("验证错误ValidateException测试");
        }

        /// <summary>
        /// 并发堵塞测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object TestConcurrent()
        {
            Thread.Sleep(5000); //当前线程堵塞5s
            return "ok";
        }

        /// <summary>
        /// 提交文件测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<Hashtable> TestFileUpload()
        {
            // 检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //文件保存目录路径 
            string SaveTempPath = "~/File/Test/Img/";
            String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<Hashtable>(o =>
                {
                    Hashtable hash = new Hashtable();
                    hash["error"] = 1;
                    hash["errmsg"] = "上传出错";
                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        hash["error"] = 1;
                        hash["errmsg"] = "请选择上传文件。";
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        hash["error"] = 1;
                        hash["errmsg"] = "上传文件大小超过限制。";
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        String fileTypes = "gif,jpg,jpeg,png,bmp";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            hash["error"] = 1;
                            hash["errmsg"] = "上传文件扩展名是不允许的扩展名。";
                        }
                        else
                        {
                            String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            fileinfo.Delete();
                            hash["error"] = 0;
                            hash["errmsg"] = "上传成功";
                        }
                    }
                    return hash;
                });
            return task;
        }
    }
}
