using LS.Sdk.WeiXinSdk;
using LS.Sdk.WeiXinSdk.Request;
using LS.Sdk.WeiXinSdk.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.ZhaoFaUnit.WeiXinTest
{
    [TestClass]
    public class WeiXinPayTest //1430189102 1332611201
    {
        WeiXinClient weiXinClient = WeiXinClient.GetClient("wxa0dd7494df4ad9f2", "2", "http://www.baidu.com", "https://api.mch.weixin.qq.com", "123", "ABCDEFG123456789987654321UTRON88", "1332611201");

        [TestMethod]
        public void UpLoadOrder()
        {
            WeiXinSdkUnifiedorderRequest weiXinSdkUnifiedorderRequest = new WeiXinSdkUnifiedorderRequest()
            {
                appid = "wxa0dd7494df4ad9f2",
                body = "测试",
                mch_id = "1332611201",
                nonce_str = "123",
                notify_url = "http://www.baidu.com",
                out_trade_no = "123000",
                scene_info = "{'h5_info': {'type':'Wap','wap_url': 'https://pay.qq.com','wap_name': '腾讯充值'}}",
                spbill_create_ip = "125.108.121.241",
                total_fee = 100,
                trade_type = "MWEB"

            };
            var sig = weiXinClient.Sign(weiXinSdkUnifiedorderRequest);
            weiXinSdkUnifiedorderRequest.sign = sig;
            var response = weiXinClient.Send(weiXinSdkUnifiedorderRequest);
        }

        [TestMethod]
        public void GetOrder()
        {
            WeiXinSdkOrderQueryRequest weiXinSdkOrderQueryRequest = new WeiXinSdkOrderQueryRequest()
            {
                appid = "wxa0dd7494df4ad9f2",
                mch_id = "1332611201",
                nonce_str = "123",
                out_trade_no = "123"
            };

            weiXinSdkOrderQueryRequest.sign = weiXinClient.Sign(weiXinSdkOrderQueryRequest);
            var response = weiXinClient.Send(weiXinSdkOrderQueryRequest);
        }

        [TestMethod]
        public void CallbackTest()
        {
            string xml = @"<xml><appid><![CDATA[wx2421b1c4370ec43b]]></appid><attach><![CDATA[支付测试]]></attach><bank_type><![CDATA[CFT]]></bank_type><fee_type><![CDATA[CNY]]></fee_type><is_subscribe><![CDATA[Y]]></is_subscribe><mch_id><![CDATA[10000100]]></mch_id><nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str><openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid><out_trade_no><![CDATA[1409811653]]></out_trade_no><result_code><![CDATA[SUCCESS]]></result_code ><return_code><![CDATA[SUCCESS]]></return_code ><sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign><sub_mch_id><![CDATA[10000100]]></sub_mch_id><time_end><![CDATA[20140903131540]]></time_end><total_fee>1</total_fee><trade_type><![CDATA[JSAPI]]></trade_type>
                          <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
                        </xml>".Trim();

            var response =  weiXinClient.Callback<WeiXinSdkNotifyOrderResponse>(xml);

        }
    }
}
