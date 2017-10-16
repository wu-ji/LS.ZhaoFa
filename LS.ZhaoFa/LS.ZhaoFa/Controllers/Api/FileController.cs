using LS.UtilityTools;
using LS.ZhaoFa.ActionFilte.Authentication;
using LS.ZhaoFa.Models.Api.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LS.ZhaoFa.Controllers.Api
{
    /// <summary>
    /// 文件 上传业务模块
    /// </summary>
    public class FileController : ApiController
    {
        /// <summary>
        /// 用户上传 询价产品图片
        /// </summary>
        /// <returns></returns>
        [UserAuthentication]
        [HttpPost]
        public Task<ApiReturnModel> InquiryProductImgUpload()
        {
            // 检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //文件保存目录路径 
            string SaveTempPath = ConfigHelp.GetValueByNameInAppSettings("InquiryProductImg");
            SaveTempPath += DateTime.Now.ToString("yyyy") + '/';
            SaveTempPath += DateTime.Now.ToString("MM") + '/';
            String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);

            if (!System.IO.Directory.Exists(dirTempPath))
            {
                System.IO.Directory.CreateDirectory(dirTempPath);
            }

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<ApiReturnModel>(o =>
                {

                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        return ApiReturnModel.ReturnError("请上传文件");
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        return ApiReturnModel.ReturnError("文件大小超过限制");
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        String fileTypes = "gif,jpg,jpeg,png,bmp";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            return ApiReturnModel.ReturnError("只能上传 图片");
                        }
                        else
                        {
                            String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            fileinfo.Delete();
                            return ApiReturnModel.ReturnOk("上传 成功", SaveTempPath + newFileName + fileExt);
                        }
                    }
                });
            return task;
        }

        /// <summary>
        /// 报价 图片上传
        /// </summary>
        /// <returns></returns>
        public Task<ApiReturnModel> QuotationProductImgUpload()
        {
            // 检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //文件保存目录路径 
            string SaveTempPath = ConfigHelp.GetValueByNameInAppSettings("QuotationProductImg");
            SaveTempPath += DateTime.Now.ToString("yyyy") + '/';
            SaveTempPath += DateTime.Now.ToString("MM") + '/';
            String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);

            if (!System.IO.Directory.Exists(dirTempPath))
            {
                System.IO.Directory.CreateDirectory(dirTempPath);
            }

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<ApiReturnModel>(o =>
                {

                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        return ApiReturnModel.ReturnError("请上传文件");
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        return ApiReturnModel.ReturnError("文件大小超过限制");
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        String fileTypes = "gif,jpg,jpeg,png,bmp";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            return ApiReturnModel.ReturnError("只能上传 图片");
                        }
                        else
                        {
                            String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            fileinfo.Delete();
                            return ApiReturnModel.ReturnOk("上传 成功", SaveTempPath + newFileName + fileExt);
                        }
                    }
                });
            return task;
        }
    }
}
