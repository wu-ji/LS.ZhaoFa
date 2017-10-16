using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Product
{
    /// <summary>
    /// api层 用户产品详情通用模型
    /// </summary>
    public class ApiUserProductModel
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格(询价时 不需要提交)
        /// </summary>
        public string Specifications { get; set; }

        /// <summary>
        /// 厂家 询价时不需要提交
        /// </summary>
        public string Manufactor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 单个价格 (不需要时 提交0)
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 图片url地址 (数据为上传文件接口返回的相对路径)
        /// </summary>
        public List<string> ImgUrl { get; set; }
    }
}