using LS.Sdk.ZhiFuBaoSdk;
using LS.Sdk.ZhiFuBaoSdk.Request;
using LS.Sdk.ZhiFuBaoSdk.Request.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.ZhaoFaUnit.ZhiFuBao
{
    /// <summary>
    /// 支付宝 支付测试
    /// </summary>
    [TestClass]
    public class ZhiFuBaoPayTest
    {
        ZhiFuBaoClient zhiFuBaoClient = ZhiFuBaoClient.GetClient("2016081600254787", "utf-8", "RSA2",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"1.0", @"MIIEpAIBAAKCAQEAzf38hJFe+c2QmFg6TCr2bvi2VGNIrMnrmmmdSUPCftlt3hvz/U3T1ZOaqCbMzQFLshgU0az/KTJ9DY7Ebc6n4owV+2BD6ZpvCSRr26fzjr5GKKXsUIW9PETlP27k+Opb5FsYJYsfQ7vtJtlw/KkamibpzLaZ1VYK/Xhn+GodyuwWopzdoLoWm1skA0hrdahJ0LbW74W6jbrLjrXlWK6moMqBcevG3hj3j3i3e3uRcuDSMVCRvOC+e84biM70r03j5XqKGaTyAwblzwX8zgAky/l53qFnIB0kKws6PkAdHjwK5YahRRAZiGKA+Y6i4Q3LH33QrKtKo3utF3dwvDDm1wIDAQABAoIBAD6jwpspfaEm8pZxFeE9m0ypkwBcZKguitec0DiMQ2PBiJhnFa3sccNbrEb/7X1VrIgOrJ4KJlmHweFSjcb6dbD9el3Dq0MpNCSqj6XvD7BTihAoTJE76gyewrukZDqRQbpSPSiFgs9dIUOUEbizT6SaurQGvwbRsGvdpngzBLZEwom3rrtgQOqSNDfa8+W1Z7APTcS95ngTZRP/Wrbegxkyu6/409QW4HxDQlTsb26E6T5zExNWSHEPh0P2ZSIF4FJ7xzGLzz3VUfwvQnD7ALjG7hdC354ANnGuuebR20jhydYfd2Npdq2a2TULFmLbZ3C/ssgm3/mchAAYEZJ0IoECgYEA+RA00LuVu4SA3NRZhbSefXlbxS/EwrxTJrehuX+R/0wuWcyEQh9t+fYnfWu3bfVMDG6bkvmDZ4tYFvhygqxOZp6o+vHH/So0asUGLTCLY+eh6gawhWtCFcLbA0hzC58HNgtMYYkE1elbfwMximWvnilP8BLKVwvKJiua0xQf8cECgYEA07qv+t84armAipimeiaaxMGgpLB1fA0JMC8n0N9nQuT73nFcN4s2VRNd2/cXvv//hcJX3uNOmH+z5BIyK1RwYopSKSG8WBwJN9MK6hY2PN4hFdv2So3LXbeSbLZFEhQMI+SACFVBYaqLCYRXoXfWiPxOBegZmViQWhHLQLzQzpcCgYEAum0OC3uNvVmWFzV/eNxUkbjoHzX9QoIyf1WOYUoC3ySHwUGbcd8Ss7oznuak21JzxQ55ts67NCMSIcd/9x3AZYG3HHcj6fTQXWbyk5q1i2dTQ9gUAxng0mcTBmcRbg5wGFzmpE8qZm+QxAaA64XnqSxlIF8AQ52Yh2+2KO70ZkECgYEAqiZ+I/LdSM84oKjaKJH7kKE2cwMn9wAW4TUUH0RZnKWeT330KOLkT+xXYl0pJSJfe2PZ79HmKKF/tIp27OFXy6jzLADjdj9ZnRYp0EL3ZWTxW7rAK9vVkcPjlR/JVCTuOK35wjiZaV5/i69iKO3AZ6ezIAYNBHHq16czGxxiBZMCgYBy1sm+7qrYTdgzFzhPGqNduhGK7rDEohZKfuM3LAcWTtJLLrv+gFqR+5INQyZ3UIOut22DbN7U6EE6wlMrZ2Y+lteVSaX/RjF+p8jI2+OfvUqzZ+5iqSwGcvEuhOJZkoviKzoS0JSf9rE6SwB4nhTov9x+RfgIFHQHYwrPr7wXJw==", "1", "https://openapi.alipaydev.com/gateway.do","1","");
        [TestMethod]
        public void uploadWapPay()
        {
            ZhiFuBaoSdkWapPayRequest zhiFuBaoSdkWapPayRequest = new ZhiFuBaoSdkWapPayRequest()
            {
                out_trade_no = "10010",
                product_code = "QUICK_WAP_WAY",
                quit_url = "http://www.baidu.com",
                subject = "测试商品",
                total_amount =1
            };

            var response = zhiFuBaoClient.Send(zhiFuBaoSdkWapPayRequest);
        }
      
        [TestMethod]
        public void GetOrderTest()
        {
            ZhiFuBaoSdkTradeQueryRequest zhiFuBaoSdkTradeQueryRequest = new ZhiFuBaoSdkTradeQueryRequest()
            {
                out_trade_no = "10010"
            };

            var response = zhiFuBaoClient.Send(zhiFuBaoSdkTradeQueryRequest);
        }

        [TestMethod]
        public void TestModel()
        {
            ZhiFuBaoSdkTestRequest zhiFuBaoSdkTestRequest = new ZhiFuBaoSdkTestRequest()
            {
                shop_id = "2017101400077000000045952915",
                op_role = "MERCHANT"
            };

            var response = zhiFuBaoClient.Send(zhiFuBaoSdkTestRequest);
        }
    }
}


//sta8zzkwhAcAFOimfWqz/TCXFsGotUmgb6OWqwbKGtH3Qg4+SztjTtwb+2ZDBQS6NHQjZ9NMkrZVFUzBZPQvdbNQQC7qq0wmpUMn729S9gzpqtogdZItnl+UF3dC40mFV/2N6+fgoBiY3jRc6fGPdN46BAQ7zVmN4pys5ltvGcrJTkyIlFYuvWh/XSmJxD1zKTvbfh+g4tP637acVCVfIeRTqBDuQ5tQePC8Y41trsqOzHMCwp0WxBeu3r2KyjfoMQtBJU1O0KT4EsdML1otDsx68jGdfKU5u08FREKYuFb5/pauxTviQCjWqihW9XFMy1C+qnjWx91YDPsjhi8N8A==
