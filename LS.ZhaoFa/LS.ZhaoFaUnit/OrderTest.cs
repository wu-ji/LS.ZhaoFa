using LS.BusinessServer.Business;
using LS.BusinessServer.Business.Order;
using LS.DBServer.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.ZhaoFaUnit
{
    [TestClass]
    public class OrderTest
    {
        UserOrderBusiness userOrderBusiness = new UserOrderBusiness();
        UserLogisticsBusiness userLogisticsBusiness = new UserLogisticsBusiness();
        InquiryOrderBusiness inquiryOrderBusiness = new InquiryOrderBusiness();
        [TestMethod]
        public void AddOrderTest()
        {
            userOrderBusiness.AddItem(new UserOrder()
            {
                Id = Guid.NewGuid(),
                Flag = 0,
                PayFlag = 0,
                UserId = new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"),
                //OrderAddressId = Guid.NewGuid(),
                CouponId = new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"),
                OriginalPrice = 10,
                RealisticPrice = 5,
                CreateTime = DateTime.Now,
                PayingTime = DateTime.Now,
                CompletionTime = DateTime.Now,
                SubmissionTime = DateTime.Now

            });
        }

        /// <summary>
        /// 增加物流数据 
        /// </summary>
        [TestMethod]
        public void AddLogistics()
        {
            userLogisticsBusiness.AddItem(new UserLogistics()
            {
                Id = Guid.NewGuid(),
                UserId = new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"),
                CountermanId = new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"),
                CreateTime = DateTime.Now,
                DetailedInformation = "包裹 已到达杭州集散中心",
                OrderId = new Guid("01A8CDBC-2EDB-455A-B733-D04AA7AC3AC4"),
                LogisticsTime = DateTime.Now
            });
        }

        /// <summary>
        /// 修改物流数据s
        /// </summary>
        [TestMethod]
        public void UpdateLogistics()
        {
            userLogisticsBusiness.UpdateLogisticsById(new Guid("A970F975-6F5C-4F47-B846-3CA6359E33FE"), new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"), "包裹 已到达温州集散中心", DateTime.Now);
        }

        /// <summary>
        /// 修改 询价单状态
        /// </summary>
        [TestMethod]
        public void UpdateInquiryOrderFlag()
        {
            var msg = inquiryOrderBusiness.UpdateInquiryOrderFlag(new Guid("0B493C53-16A2-41F9-8C2D-18B3129D7099"), new Guid("E4605C37-EAFE-4EDE-BCF9-9813124E8DC2"), "测试修改-通过", DBServer.Model.Enum.BusinessOrderFlag.Effective,DateTime.Now);
        }
    }
}
