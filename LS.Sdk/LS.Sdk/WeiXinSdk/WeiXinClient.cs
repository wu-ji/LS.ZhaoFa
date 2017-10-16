using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Sdk._0.ISDK;
using System.Net.Http;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Xml;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;

namespace LS.Sdk.WeiXinSdk
{
    public class WeiXinClient : BaseClient
    {
        private WeiXinClient()
        { }
        public override string AppId { get; set; } = "1";
        public override string Secret { get; set; } = "2";
        public override string ReturnUrl { get; set; }
        public override string DoMain { get; set; } = "https://api.mch.weixin.qq.com/";
        public override string Token { get; set; }

        /// <summary>
        /// key为商户平台设置的密钥key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 商户平台id
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 微信平台 签名属性名称常量 sign
        /// </summary>
        private readonly string SignProName = "sign";

        public override T AsyncSend<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public override T Send<T>(IBaseRequest<T> request)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMsg = SetHttpRequest(httpClient, request);

            try
            {
                string content = httpClient.SendAsync(requestMsg).Result.Content.ReadAsStringAsync().Result;



                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(content);

                XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
                XmlNodeList nodes = xmlNode.ChildNodes;


                T response = new T();

                var pros = response.GetType().GetProperties();
                foreach (XmlNode xn in nodes)
                {
                    foreach (var pro in pros)
                    {
                        if (xn.Name == pro.Name)
                        {
                            if (pro.GetType() == typeof(int))
                                pro.SetValue(response, Convert.ToInt32(xn.InnerText));
                            else
                                pro.SetValue(response, xn.InnerText);

                            break;
                        }
                    }
                }

                return response;
            }
            catch (Exception e)
            {
                //记录日志 e

                return default(T);
            }
        }
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request)
        {
            HttpRequestMessage requestMsg = new HttpRequestMessage(request.GetHttpMethod(), DoMain + request.Url());

            //设置请求头 等相关凭证
            //requestMsg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("wuji", "123");
            requestMsg.Content = SetHttpContent(request);
            return requestMsg;
        }

        /// <summary>
        /// 设置请求报文
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpContent SetHttpContent<T>(IBaseRequest<T> request)
        {
            var pros = request.GetType().GetProperties();
            string xml = "<xml>";
            foreach (var pair in pros)
            {
                //字段值不能为null，会影响后续流程
                if (pair.GetValue(request) == null)
                {
                    continue;
                }

                if (pair.PropertyType == typeof(int))
                {
                    xml += "<" + pair.Name + ">" + pair.GetValue(request) + "</" + pair.Name + ">";
                }
                else if (pair.PropertyType == typeof(string))
                {
                    xml += "<" + pair.Name + ">" + "<![CDATA[" + pair.GetValue(request) + "]]></" + pair.Name + ">";
                }
            }
            xml += "</xml>";

            StringContent stringContent = new StringContent(xml);
            return stringContent;
        }
        /// <summary>
        /// 计算签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public override string Sign<T>(IBaseRequest<T> request)
        {
            var pros = request.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("appid", AppId);
            dictionary.Add("mch_id", MchId);
            foreach (var item in pros)
            {
                if (item.GetValue(request) == null || dictionary.Keys.Contains(item.Name) || item.Name == SignProName)
                    continue;
                dictionary.Add(item.Name, item.GetValue(request));
            }
            var returnDictionary = dictionary.OrderBy(item => item.Key);
            var para = returnDictionary.Select(str => $"{str.Key}={str.Value}");
            var content = string.Join("&", para);
            string stringSignTemp = content + "&key=" + Key;

            var bys = Encoding.UTF8.GetBytes(stringSignTemp);

            var signBys = MD5.Create().ComputeHash(bys);
            var sb = new StringBuilder();
            foreach (byte b in signBys)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 验证签名是否正确
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="paraObj"></param>
        /// <returns></returns>
        public bool CheckSign(string sign, object paraObj)
        {
            var pros = paraObj.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var item in pros)
            {
                if (item.PropertyType == typeof(int) && Convert.ToInt32(item.GetValue(paraObj)) == 0)
                    continue;
                if (item.GetValue(paraObj) == null || dictionary.Keys.Contains(item.Name) || item.Name == SignProName)
                    continue;
                dictionary.Add(item.Name, item.GetValue(paraObj));
            }
            var returnDictionary = dictionary.OrderBy(item => item.Key);
            var para = returnDictionary.Select(str => $"{str.Key}={str.Value}");
            var content = string.Join("&", para);
            string stringSignTemp = content + "&key=" + Key;

            var bys = Encoding.UTF8.GetBytes(stringSignTemp);

            var signBys = MD5.Create().ComputeHash(bys);
            var sb = new StringBuilder();
            foreach (byte b in signBys)
            {
                sb.Append(b.ToString("x2"));
            }


            //在本地计算新的签名
            string cal_sign = sb.ToString().ToUpper();

            if (cal_sign == sign)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 创建 一个微信SDK客户端 用来与微信api平台交互
        /// </summary>s
        /// <param name="Appid">Appid</param>
        /// <param name="Secret">Secret</param>
        /// <param name="ReturnUrl">回调地址</param>
        /// <param name="DoMain">微型api平台主域名</param>
        /// <param name="Token">本次通信携带的tioken 无则传null</param>
        /// <param name="Key">商家商户密钥</param>
        /// <param name="MchId">商家 商户id</param>
        /// <param name="clientId">所要创建的客户端id 0-为sdk内部自动选择</param>
        /// <returns></returns>
        public static WeiXinClient GetClient(string Appid, string Secret, string ReturnUrl, string DoMain, string Token, string Key, string MchId, int clientId = 0)
        {
            return new WeiXinClient()
            {
                AppId = Appid,
                DoMain = DoMain,
                Key = Key,
                MchId = MchId,
                ReturnUrl = ReturnUrl,
                Secret = Secret,
                Token = Token
            };
        }

        /// <summary>
        /// 微信回调 处理方法 包含签名验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestContent">回调内容</param>
        /// <param name="deserializeType">内容反序列化 类型id 默认为0  0-xml反序列化 1-json反序列化</param>
        /// <returns></returns>
        public override T Callback<T>(string requestContent, int deserializeType = 0)
        {
            T request = new T();
            var pros = request.GetType().GetProperties();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(requestContent);

            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;

            string sign = string.Empty;
            foreach (XmlNode xn in nodes)
            {
                foreach (var pro in pros)
                {
                    if (xn.Name == pro.Name)
                    {
                        if (pro.PropertyType == typeof(int))
                            pro.SetValue(request, Convert.ToInt32(xn.InnerText));
                        else
                            pro.SetValue(request, xn.InnerText);

                        if (pro.Name == SignProName)
                            sign = xn.InnerText;
                        break;
                    }
                }
            }
            if (CheckSign(sign, request))
            {
                LogQueueModel logQueueModel = new LogQueueModel()
                {
                    FileName = ApiFileDirectoryPara.WeiXinBusinessLog,
                    Msg = $"收到微信合法回调 请求数据为 : \r\n {requestContent}"
                };

                LogHelp.AddLogQueue(logQueueModel);

                return request;
            }
            else
            {
                //说明有人伪造 请求 记录日志
                LogQueueModel logQueueModel = new LogQueueModel()
                {
                    FileName = ApiFileDirectoryPara.WeiXinBusinessLog,
                    Msg = $"收到微信非法回调(签名错误) 请求数据为 : \r\n {requestContent}"
                };

                LogHelp.AddLogQueue(logQueueModel);

                return default(T);
            }
        }
    }
}
