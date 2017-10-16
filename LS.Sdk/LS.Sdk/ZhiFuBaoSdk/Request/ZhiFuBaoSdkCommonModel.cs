using LS.Sdk._1.BaseSDK;
using LS.Sdk.ZhiFuBaoSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Sdk.ZhiFuBaoSdk.Request
{
    /// <summary>
    /// 支付宝 接口请求模型基类 (提供所有公共参数)
    /// </summary>
   public  class ZhiFuBaoSdkCommonModel
    {
        /// <summary>
        /// 支付宝分配给开发者的应用ID
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// 接口名称 如 alipay.trade.wap.pay
        /// </summary>
        public string method { get; set; }
        /// <summary>
        ///请求使用的编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        public string charset { get; set; }
        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 商户请求参数的签名串
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 主动通知地址(回调)
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 前台回跳地址
        /// </summary>
        public string return_url { get; set; }

        /// <summary>
        /// 业务请求参数的集合，最大长度不限，除公共参数外所有请求参数都必须放在这个参数中传递，具体参照各产品快速接入文档
        /// </summary>
        public string biz_content { get; set; }
    }
}
