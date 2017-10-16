using LS.Sdk._1.BaseSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.Sdk._0.ISDK;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using LS.Sdk.ZhiFuBaoSdk.Request;
using System.Web;
using LS.Sdk.ZhiFuBaoSdk.Response;

namespace LS.Sdk.ZhiFuBaoSdk
{
    /// <summary>
    /// 支付宝平台客户端
    /// </summary>
    public class ZhiFuBaoClient : BaseClient
    {
        private ZhiFuBaoClient()
        { }

        /// <summary>
        /// 全局 唯一支付宝客户端实例
        /// </summary>
        private static ZhiFuBaoClient zhiFuBaoClient = new ZhiFuBaoClient();
        public override string AppId { get; set; }
        public override string Secret { get; set; }
        /// <summary>
        /// 回调通知地址
        /// </summary>
        public override string ReturnUrl { get; set; }
        public override string DoMain { get; set; }
        public override string Token { get; set; }

        /// <summary>
        /// 支付宝 私钥
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string PublicKey { get; set; }

        public string Charset { get; set; }

        /// <summary>
        /// 商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        public string Sign_type { get; set; }

        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 前台 通知地址
        /// </summary>
        public string Return_url { get; set; }


        /// <summary>
        /// 签名属性值 常量
        /// </summary>
        private readonly string SignProName = "sign";

        public override T AsyncSend<T>(IBaseRequest<T> request)
        {
            throw new NotImplementedException();
        }
        public override T Send<T>(IBaseRequest<T> request)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMsg = SetHttpRequest(httpClient, request);

            try
            {
                string json = httpClient.SendAsync(requestMsg).Result.Content.ReadAsStringAsync().Result;

                var response = JsonConvert.DeserializeObject<T>(json);
                return response;
            }
            catch (Exception e)
            {
                //记录日志 e

                return default(T);
            }
        }
        /// <summary>
        /// 获得wap支付 的html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetWapPayHtml(ZhiFuBaoSdkWapPayRequest request)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();


            dictionary.Add("app_id", AppId);
            dictionary.Add("method", request.Url());
            dictionary.Add("charset", Charset);
            dictionary.Add("sign_type", Sign_type);
            dictionary.Add("timestamp", Timestamp);
            dictionary.Add("version", Version);
            dictionary.Add("return_url", Return_url);
            dictionary.Add("notify_url", ReturnUrl);

            var jsonContent = JsonConvert.SerializeObject(request);
            dictionary.Add("biz_content", jsonContent);
            dictionary.Add("sign", Sign(request));

            StringBuilder sbHtml = new StringBuilder();


            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + DoMain + "?charset=" + Charset +
                 "' method='post'>");

            foreach (var temp in dictionary)
            {

                sbHtml.Append("<input type='hidden'  name='" + temp.Key + "' value='" + temp.Value + "'/>");

            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='post' style='display:none;'></form>");

            //表单实现自动提交
            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 支付宝回调处理 请用RSACheckV1方法 此方法目前不做实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestContent"></param>
        /// <param name="deserializeType"></param>
        /// <returns></returns>
        public override T Callback<T>(string requestContent, int deserializeType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置请求报文
        /// </summary>
        /// <typeparam name="T">响应模型</typeparam>
        /// <param name="request">请求模型</param>
        /// <returns></returns>
        public override HttpContent SetHttpContent<T>(IBaseRequest<T> request)
        {

            var pros = request.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();


            dictionary.Add("app_id", HttpUtility.UrlEncode(AppId, request.GetEncoding()));
            dictionary.Add("method", HttpUtility.UrlEncode(request.Url(), request.GetEncoding()));
            dictionary.Add("charset", HttpUtility.UrlEncode(Charset, request.GetEncoding()));
            dictionary.Add("sign_type", HttpUtility.UrlEncode(Sign_type, request.GetEncoding()));
            dictionary.Add("timestamp", HttpUtility.UrlEncode(Timestamp, request.GetEncoding()));
            dictionary.Add("version", HttpUtility.UrlEncode(Version, request.GetEncoding()));
            if (Return_url != null && Return_url.Length > 0)
                dictionary.Add("return_url", HttpUtility.UrlEncode(Return_url, request.GetEncoding()));

            if (ReturnUrl != null && ReturnUrl.Length > 0)
                dictionary.Add("notify_url", HttpUtility.UrlEncode(ReturnUrl, request.GetEncoding()));

            var jsonContent = JsonConvert.SerializeObject(request);
            dictionary.Add("biz_content", HttpUtility.UrlEncode(jsonContent, request.GetEncoding()));
            dictionary.Add("sign", HttpUtility.UrlEncode(Sign(request), request.GetEncoding()));

            var returnDictionary = dictionary.OrderBy(item => item.Key);
            var para = returnDictionary.Select(str => $"{str.Key}={str.Value}");
            var content = string.Join("&", para);


            StringContent stringContent = new StringContent(content, request.GetEncoding());
            stringContent.Headers.ContentType.MediaType = request.GetContentType();

            return stringContent;
        }

        public override HttpRequestMessage SetHttpRequest<T>(HttpClient client, IBaseRequest<T> request)
        {
            if (request.GetHttpMethod() == HttpMethod.Get)
            {
                string para = SetHttpContent(request).ReadAsStringAsync().Result;

                HttpRequestMessage requestMsg = new HttpRequestMessage(request.GetHttpMethod(), DoMain + "?" + para);


                return requestMsg;
            }
            else
            {
                HttpRequestMessage requestMsg = new HttpRequestMessage(request.GetHttpMethod(), DoMain);


                requestMsg.Content = SetHttpContent(request);

                return requestMsg;
            }
        }


        /// <summary>
        /// 1.筛选并排序
        /// 获取所有请求参数，不包括字节类型参数，如文件、字节流，剔除sign字段，剔除值为空的参数，并按照第一个字符的键值ASCII码递增排序（字母升序排序），如果遇到相同字符则按照第二个字符的键值ASCII码递增排序，以此类推。
        ///2.拼接
        ///将排序后的参数与其对应值，组合成“参数=参数值”的格式，并且把这些参数用&字符连接起来，此时生成的字符串为待签名字符串。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public override string Sign<T>(IBaseRequest<T> request)
        {
            var pros = request.GetType().GetProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            dictionary.Add("app_id", AppId);
            dictionary.Add("method", request.Url());
            dictionary.Add("charset", Charset);
            dictionary.Add("sign_type", Sign_type);
            dictionary.Add("timestamp", Timestamp);
            dictionary.Add("version", Version);
            if (Return_url != null && Return_url.Length > 0)
                dictionary.Add("return_url", Return_url);
            if (ReturnUrl != null && ReturnUrl.Length > 0)
                dictionary.Add("notify_url", ReturnUrl);

            var jsonContent = JsonConvert.SerializeObject(request);


            dictionary.Add("biz_content", jsonContent);



            var returnDictionary = dictionary.OrderBy(item => item.Key);
            var para = returnDictionary.Select(str => $"{str.Key}={str.Value}");
            var content = string.Join("&", para);
            var sig = RSASignCharSet(content, PrivateKey, Charset, false, Sign_type);
            return sig;
        }
        /// <summary>
        /// 获取 缓存中的 支付宝客户端
        /// </summary>
        /// <returns></returns>
        public static ZhiFuBaoClient GetClient()
        {
            return zhiFuBaoClient;
        }

        /// <summary>
        /// 替换 缓存中的支付宝客户端 并且返回新的实例
        /// </summary>
        /// <param name="appid">替换的appid 应用id</param>
        /// <param name="charset">替换的charset 编码格式</param>
        /// <param name="sign_type">替换的sign_type 签名类型</param>
        /// <param name="timestamp">替换的timestamp 请求时间</param>
        /// <param name="version">替换的version 版本号</param>
        /// <param name="privateKey">替换的privateKey 私有密钥</param>
        /// <param name="returnUrl">替换的returnUrl 前台 通知地址</param>
        /// <param name="domain">替换的domain 请求域名</param>
        /// <param name="notify_url">替换的notify_url 回调通知地址</param>
        /// <param name="publicKey">替换的publicKey 支付宝公钥</param>
        /// <returns></returns>
        public static ZhiFuBaoClient GetClient(string appid, string charset, string sign_type, string timestamp, string version, string privateKey, string returnUrl, string domain, string notify_url, string publicKey)
        {
            zhiFuBaoClient.AppId = appid;
            zhiFuBaoClient.Charset = charset;
            zhiFuBaoClient.Sign_type = sign_type;
            zhiFuBaoClient.Timestamp = timestamp;
            zhiFuBaoClient.Version = version;
            zhiFuBaoClient.DoMain = domain;
            zhiFuBaoClient.PrivateKey = privateKey;
            zhiFuBaoClient.ReturnUrl = notify_url;
            zhiFuBaoClient.Return_url = returnUrl;
            zhiFuBaoClient.PublicKey = publicKey;
            return zhiFuBaoClient;
        }

        /// <summary>
        /// 支付回调方法 外界控制器中需要 将post数据 全部放在 字典集合中
        /// </summary>
        /// <param name="parameters">支付宝方发送的 数据字典集合</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <param name="charset">编码模式</param>
        /// <param name="zhiFuBaoSdkNotifyOrderResponse">填充的数据模型 运行该方法后 该数据类型的所有属性会在方法内赋值</param>
        /// <returns></returns>
        public bool RSACheckV1<TResponse>(IDictionary<string, string> parameters, string publicKeyPem, string charset, TResponse zhiFuBaoSdkNotifyOrderResponse)
        {
            zhiFuBaoSdkNotifyOrderResponse = SetObjValueByDic(parameters, zhiFuBaoSdkNotifyOrderResponse);

            string sign = parameters["sign"];

            parameters.Remove("sign");
            parameters.Remove("sign_type");
            string signContent = GetSignContent(parameters);
            return RSACheckContent(signContent, sign, publicKeyPem, charset, "RSA");
        }

        /// <summary>
        /// 根据key 设置模型对应属性的值
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="obj"></param>
        private T SetObjValueByDic<T>(IDictionary<string, string> parameters, T obj)
        {
            var pros = obj.GetType().GetProperties();

            foreach (var item in parameters)
            {
                foreach (var pro in pros)
                {
                    if (item.Key == pro.Name)
                    {
                        if (pro.PropertyType == typeof(int))
                            pro.SetValue(obj, Convert.ToInt32(item.Value));
                        else
                            pro.SetValue(obj, item.Value);
                        break;
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 根据 字典的key和value 组合出key=value 的组合
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string GetSignContent(IDictionary<string, string> parameters)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value).Append("&");
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);

            return content;
        }

        #region 验签
        private bool RSACheckContent(string signContent, string sign, string publicKeyPem, string charset, string signType)
        {

            try
            {
                if (string.IsNullOrEmpty(charset))
                {
                    charset = "UTF-8";
                }

                if ("RSA2".Equals(signType))
                {
                    string sPublicKeyPEM = File.ReadAllText(publicKeyPem);

                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.PersistKeyInCsp = false;
                    RSACryptoServiceProviderExtension.LoadPublicKeyPEM(rsa, sPublicKeyPEM);

                    bool bVerifyResultOriginal = rsa.VerifyData(Encoding.GetEncoding(charset).GetBytes(signContent), "SHA256", Convert.FromBase64String(sign));
                    return bVerifyResultOriginal;

                }

                else
                {
                    string sPublicKeyPEM = File.ReadAllText(publicKeyPem);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.PersistKeyInCsp = false;
                    RSACryptoServiceProviderExtension.LoadPublicKeyPEM(rsa, sPublicKeyPEM);

                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    bool bVerifyResultOriginal = rsa.VerifyData(Encoding.GetEncoding(charset).GetBytes(signContent), sha1, Convert.FromBase64String(sign));
                    return bVerifyResultOriginal;
                }
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 签名方法

        public static string RSASignCharSet(string data, string privateKeyPem, string charset, bool keyFromFile, string signType)
        {

            byte[] signatureBytes = null;
            try
            {
                RSACryptoServiceProvider rsaCsp = null;
                if (keyFromFile)
                {//文件读取
                    rsaCsp = LoadCertificateFile(privateKeyPem, signType);
                }
                else
                {
                    //字符串获取
                    rsaCsp = LoadCertificateString(privateKeyPem, signType);
                }

                byte[] dataBytes = null;
                if (string.IsNullOrEmpty(charset))
                {
                    dataBytes = Encoding.UTF8.GetBytes(data);
                }
                else
                {
                    dataBytes = Encoding.GetEncoding(charset).GetBytes(data);
                }
                if (null == rsaCsp)
                {
                    throw new Exception("您使用的私钥格式错误，请检查RSA私钥配置" + ",charset = " + charset);
                }
                if ("RSA2".Equals(signType))
                {

                    signatureBytes = rsaCsp.SignData(dataBytes, "SHA256");

                }
                else
                {
                    signatureBytes = rsaCsp.SignData(dataBytes, "SHA1");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("您使用的私钥格式错误，请检查RSA私钥配置" + ",charset = " + charset);
            }
            return Convert.ToBase64String(signatureBytes);
        }

        public static string RSASignCharSet(string data, string privateKeyPem, string charset, string signType)
        {
            RSACryptoServiceProvider rsaCsp = LoadCertificateFile(privateKeyPem, signType);
            byte[] dataBytes = null;
            if (string.IsNullOrEmpty(charset))
            {
                dataBytes = Encoding.UTF8.GetBytes(data);
            }
            else
            {
                dataBytes = Encoding.GetEncoding(charset).GetBytes(data);
            }


            if ("RSA2".Equals(signType))
            {

                byte[] signatureBytes = rsaCsp.SignData(dataBytes, "SHA256");

                return Convert.ToBase64String(signatureBytes);

            }
            else
            {
                byte[] signatureBytes = rsaCsp.SignData(dataBytes, "SHA1");

                return Convert.ToBase64String(signatureBytes);
            }
        }
        private static RSACryptoServiceProvider LoadCertificateString(string strKey, string signType)
        {
            byte[] data = null;
            //读取带
            //ata = Encoding.Default.GetBytes(strKey);
            data = Convert.FromBase64String(strKey);
            //data = GetPem("RSA PRIVATE KEY", data);
            try
            {
                RSACryptoServiceProvider rsa = DecodeRSAPrivateKey(data, signType);
                return rsa;
            }
            catch (Exception ex)
            {
                //    throw new AopException("EncryptContent = woshihaoren,zheshiyigeceshi,wanerde", ex);
            }
            return null;
        }

        private static RSACryptoServiceProvider LoadCertificateFile(string filename, string signType)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(filename))
            {
                byte[] data = new byte[fs.Length];
                byte[] res = null;
                fs.Read(data, 0, data.Length);
                if (data[0] != 0x30)
                {
                    res = GetPem("RSA PRIVATE KEY", data);
                }
                try
                {
                    RSACryptoServiceProvider rsa = DecodeRSAPrivateKey(res, signType);
                    return rsa;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }
        private static byte[] GetPem(string type, byte[] data)
        {
            string pem = Encoding.UTF8.GetString(data);
            string header = String.Format("-----BEGIN {0}-----\\n", type);
            string footer = String.Format("-----END {0}-----", type);
            int start = pem.IndexOf(header) + header.Length;
            int end = pem.IndexOf(footer, start);
            string base64 = pem.Substring(start, (end - start));

            return Convert.FromBase64String(base64);
        }

        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey, string signType)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // --------- Set up stream to decode the asn.1 encoded RSA private key ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------ all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);


                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                CspParameters CspParameters = new CspParameters();
                CspParameters.Flags = CspProviderFlags.UseMachineKeyStore;

                int bitLen = 1024;
                if ("RSA2".Equals(signType))
                {
                    bitLen = 2048;
                }

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(bitLen, CspParameters);
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                binr.Close();
            }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }

            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        #endregion

    }
}
