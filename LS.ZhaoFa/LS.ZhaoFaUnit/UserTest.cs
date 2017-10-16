using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LS.BusinessServer.Business;
using LS.BusinessServer.BusinessFactory;

namespace LS.ZhaoFaUnit
{
    [TestClass]
    public class UserTest
    {
        UserBusiness UserInfo = BusinessFactory.GetBusiness<UserBusiness>();
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestUserLogin()
        {
            var user = UserInfo.GetItemByUserPwd("wuji", "123");
            Assert.IsNotNull(user);
        }
    }
}
