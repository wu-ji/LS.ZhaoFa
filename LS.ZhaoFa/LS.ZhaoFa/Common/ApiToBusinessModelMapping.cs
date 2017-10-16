using LS.BusinessServer.Model.Product;
using LS.ZhaoFa.Models.Api.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Common
{
    /// <summary>
    /// api层 和数据层模型  属性值转换
    /// </summary>
    public class ApiToBusinessModelMapping
    {
        /// <summary>
        /// 产品 模型转换
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BProductDetailModel GetBProductDetailModelByApiUserProductModel(ApiUserProductModel model)
        {
            BProductDetailModel bProductDetailModel = new BProductDetailModel()
            {
                ImgUrl = model.ImgUrl,
                Manufactor = model.Manufactor,
                Price = model.Price,
                ProductName = model.ProductName,
                Remarks = model.Remarks,
                Specifications = model.Specifications
            };

            return bProductDetailModel;
        }
    }
}