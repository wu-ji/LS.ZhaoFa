using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Model.Product
{
    /// <summary>
    /// 业务层 成品详情模型
    /// </summary>
    public class BProductDetailModel
    {
        /// <summary>
        /// 产品模型
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 单品备注
        /// </summary>
        public string Artmemo { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specifications { get; set; }

        /// <summary>
        /// 厂家
        /// </summary>
        public string Manufactor { get; set; }

        /// <summary>
        /// 单个价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 图片url地址
        /// </summary>
        public List<string> ImgUrl { get; set; }
    }
}
