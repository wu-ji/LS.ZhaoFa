using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace LS.UtilityTools.Weixin
{
    public class WenxinHelper
    {
        #region 属性
        public static string appid = ConfigHelp.GetValueByNameInAppSettings("WeiXinAppId");  // "wxf7e7ef3610f168f0";//智慧传播家
        public static string appsecret = ConfigHelp.GetValueByNameInAppSettings("WeiXinAppSecret");  //52a8f2ebe0834ed8716b0d6bc5a88ecc智慧传播家公众微信平台下可以找到
        //public static string appid = "wx3fc1088ca9ad6fc5";  //传播家公众微信平台下可以找到
        //public static string appsecret = "c0ba51ab9ecab4a469f9e16ece6104d8";  //传播家公众微信平台下可以找到
        #endregion

        //根据appid，secret，code获取微信openid、access token信息
        public static OAuth_Token Get_token(string Code)
        {
            //获取微信回传的openid、access token
            string Str = GetJson("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + appsecret + "&code=" + Code + "&grant_type=authorization_code");
            //微信回传的数据为Json格式，将Json格式转化成对象

            OAuth_Token Oauth_Token_Model = JsonHelper.ParseFromJson<OAuth_Token>(Str);
            return Oauth_Token_Model;
        }

        //刷新Token(好像这个刷新Token没有实际作用)
        public static OAuth_Token refresh_token(string REFRESH_TOKEN)
        {
            string Str = GetJson("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" + appid + "&grant_type=refresh_token&refresh_token=" + REFRESH_TOKEN);
            OAuth_Token Oauth_Token_Model = JsonHelper.ParseFromJson<OAuth_Token>(Str);
            return Oauth_Token_Model;
        }

        //根据openid，access token获得用户信息
        public static OAuthUser Get_UserInfo(string REFRESH_TOKEN, string OPENID)
        {
            string Str = GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);
            OAuthUser OAuthUser_Model = JsonHelper.ParseFromJson<OAuthUser>(Str);
            return OAuthUser_Model;
        }

        //访问微信url并返回微信信息
        public static string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
            }
            return returnText;
        }

    }
    /// <summary>
    /// token类
    /// </summary>
    public class OAuth_Token
    {
        public OAuth_Token()
        {

            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        //access_token	网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        //expires_in	access_token接口调用凭证超时时间，单位（秒）
        //refresh_token	用户刷新access_token
        //openid	用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        //scope	用户授权的作用域，使用逗号（,）分隔
        public string _access_token;
        public string _expires_in;
        public string _refresh_token;
        public string _openid;
        public string _scope;
        public string access_token
        {
            set { _access_token = value; }
            get { return _access_token; }
        }
        public string expires_in
        {
            set { _expires_in = value; }
            get { return _expires_in; }
        }

        public string refresh_token
        {
            set { _refresh_token = value; }
            get { return _refresh_token; }
        }
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        public string scope
        {
            set { _scope = value; }
            get { return _scope; }
        }

    }
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class OAuthUser
    {
        public OAuthUser()
        { }
        #region 数据库字段
        private string _openID;
        private string _searchText;
        private string _nickname;
        private string _sex;
        private string _province;
        private string _city;
        private string _country;
        private string _headimgUrl;
        private string _privilege;
        #endregion

        #region 字段属性
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string openid
        {
            set { _openID = value; }
            get { return _openID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SearchText
        {
            set { _searchText = value; }
            get { return _searchText; }
        }
        /// <summary>
        /// 用户昵称 
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知 
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 普通用户个人资料填写的城市 
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 国家，如中国为CN 
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl
        {
            set { _headimgUrl = value; }
            get { return _headimgUrl; }
        }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）其实这个格式称不上JSON，只是个单纯数组
        /// </summary>
        public string privilege
        {
            set { _privilege = value; }
            get { return _privilege; }
        }
        #endregion
    }
    /// <summary>
    /// 将Json格式数据转化成对象
    /// </summary>
    public class JsonHelper
    {
        /// <summary>  
        /// 生成Json格式  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }
        /// <summary>  
        /// 获取Json的Model  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="szJson"></param>  
        /// <returns></returns>  
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
    public class WXToolsHelper
    {
        ///


        /// 获取全局的access_token,程序缓存
        ///

        ///第三方用户唯一凭证
        ///第三方用户唯一凭证密钥，即appsecret
        /// 得到的全局access_token
        public string Getaccess_token(string AppId, string AppSecret)
        {
            try
            {
                //先查缓存数据
                if (SessionHelper.Get("access_token") != null)
                {
                    return SessionHelper.Get("access_token").ToString();
                }
                else
                {
                    return Gettoken(AppId, AppSecret);
                }
            }
            catch
            {
                return Gettoken(AppId, AppSecret);
            }
            //return Gettoken(AppId, AppSecret);
        }


        ///
        /// 获取全局的access_token
        ///
        ///第三方用户唯一凭证
        ///第三方用户唯一凭证密钥，即appsecret
        /// 得到的全局access_token
        public string Gettoken(string AppId, string AppSecret)
        {
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppId, AppSecret);
            var data = client.DownloadString(url);
            var access_tokenMsg = JsonHelper.ParseFromJson<AccessToken>(data);
            //放入缓存中
            SessionHelper.Add("access_token", access_tokenMsg.access_token, 110);
            //HttpContext.Current.Cache.Insert("access_token", access_tokenMsg.access_token, null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);


            //清除jsapi_ticket缓存
            SessionHelper.Remove("ticket");
            //HttpContext.Current.Cache.Remove("ticket");

            //获取jsapi_ticket,为了同步
            GetTicket(access_tokenMsg.access_token.ToString());

            return access_tokenMsg.access_token.ToString();
        }


        ///


        /// 获取jsapi_ticket,程序缓存
        ///

        ///全局的access_token
        /// 得到的jsapi_ticket
        public string GetJsapi_Ticket(string access_token)
        {
            try
            {
                //先查缓存数据
                if (SessionHelper.Get("ticket") != null)
                {
                    return SessionHelper.Get("ticket").ToString();
                }
                else
                {
                    return GetTicket(access_token);
                }
            }
            catch
            {
                return GetTicket(access_token);
            }

        }


        ///
        /// 获取jsapi_ticket
        ///
        ///全局的access_token
        /// 得到的jsapi_ticket
        public string GetTicket(string access_token)
        {
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            var data = client.DownloadString(url);
            var ticketMsg = JsonHelper.ParseFromJson<Jsapi_Ticket>(data);
            try
            {
                //放入缓存中
                SessionHelper.Add("ticket", ticketMsg.ticket, 110);
                return ticketMsg.ticket;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 签名算法
        ///本代码来自开源微信SDK项目：https://github.com/night-king/weixinSDK
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        public string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url, out string string1)
        {
            var string1Builder = new StringBuilder();
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                          .Append("noncestr=").Append(noncestr).Append("&")
                          .Append("timestamp=").Append(timestamp).Append("&")
                          .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            string1 = string1Builder.ToString();
            return Encrypted(string1);
        }
        private static string Encrypted(string targetPassword)
        {
            byte[] pwBytes = Encoding.UTF8.GetBytes(targetPassword);
            byte[] hash = SHA1.Create().ComputeHash(pwBytes);
            StringBuilder hex = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) hex.Append(hash[i].ToString("X2"));

            return hex.ToString();
        }
        //public string SHA1Encrypt(string strIN)
        //{
        //    //string strIN = getstrIN(strIN);
        //    byte[] tmpByte;
        //    SHA1 sha1 = new SHA1CryptoServiceProvider();
        //    tmpByte = sha1.ComputeHash(GetKeyByteArray(strIN));
        //    sha1.Clear();
        //    return GetStringValue(tmpByte);
        //}
        //private byte[] GetKeyByteArray(string strKey)
        //{
        //    ASCIIEncoding Asc = new ASCIIEncoding();
        //    int tmpStrLen = strKey.Length;
        //    byte[] tmpByte = new byte[tmpStrLen - 1];
        //    tmpByte = Asc.GetBytes(strKey);
        //    return tmpByte;
        //}
        //private string GetStringValue(byte[] Byte)
        //{
        //    string tmpString = "";
        //        ASCIIEncoding Asc = new ASCIIEncoding();
        //        tmpString = Asc.GetString(Byte);
        //    return tmpString;
        //}
       

        private string[] strs = new string[]
                            {
                            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                            };

        /// 创建随机字符串
        public string CreatenNonce_str()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }

        /// 创建时间戳
        public long CreatenTimestamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        public class Jsapi_Ticket
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
        }
        public class AccessToken
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
    }
}
