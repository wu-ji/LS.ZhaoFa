using LS.UtilityTools.ApiTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LS.UtilityTools
{
    public class OtherHelper
    {
        private static Dictionary<string, object> dic = new Dictionary<string, object>();
        /// <summary>
        /// 全局字典对象 操作锁
        /// </summary>
        private static object dicLock = new object();

        /// <summary>
        /// 根据请求报文 获取id
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetRequestIp(HttpRequestBase Request)
        {
            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }

            return ip;
        }

        /// <summary>
        /// 根据id 获取城市详细数据
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static IpAreaModel GetCityInfoByIp(string ip)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip) as HttpWebRequest;
            httpWebRequest.Method = "get";
            httpWebRequest.Proxy = null;

            try
            {
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;

                string txt = string.Empty;
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {

                    txt = stream.ReadToEnd();
                }
                response.Close();
                httpWebRequest.Abort();
                IpArea ipArea = JsonConvert.DeserializeObject<IpArea>(txt);    //JsonHelp.GetObjToJson<IpArea>(txt);

                return ipArea.data;
            }
            catch
            {
                return null;
            }


        }

        /// <summary>
        /// 根据 ip获取城市名称
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetCityByIp(string ip)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip) as HttpWebRequest;
            httpWebRequest.Method = "get";
            httpWebRequest.Proxy = null;

            try
            {
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string txt = string.Empty;
                txt = stream.ReadToEnd();

                IpArea ipArea = JsonConvert.DeserializeObject<IpArea>(txt);

                return ipArea.data.city;
            }
            catch
            {
                return "未获取到城市数据";
            }


        }

        public static string GetRegionByIp(string ip)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip) as HttpWebRequest;
            httpWebRequest.Method = "get";
            httpWebRequest.Proxy = null;

            try
            {
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                string txt = string.Empty;
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {

                    txt = stream.ReadToEnd();
                }
                response.Close();
                httpWebRequest.Abort();

                IpArea ipArea = JsonConvert.DeserializeObject<IpArea>(txt);

                return ipArea.data.region;
            }
            catch
            {
                return "未获取到城市数据";
            }


        }

        /// <summary>
        /// 根据 key来提供 全局唯一的引用类型 通常用来给特定的管道 加属于自己的事务锁防止并发
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetDicValue(string key)
        {
            if (!(dic.ContainsKey(key)))
            {
                lock (dicLock) //加锁 再判断一次 确保 对象唯一不会被覆盖
                {
                    if (!(dic.ContainsKey(key)))
                    {
                        dic[key] = new object();
                        LogHelp.WriteLog(DateTime.Now + ": 创建了 一个全局锁对象 对象key为" + key, ApiFileDirectoryPara.LockDir);
                    }
                }
            }

            return dic[key];
        }

        /// <summary>
        /// 获得指定长度的 随机数
        /// </summary>
        /// <param name="length">指定随机数长度</param>
        /// <returns></returns>
        public static string GetRadomQuantity(int length)
        {

            var seed = Guid.NewGuid().GetHashCode();

            Random rd = new Random(seed);

            string validate = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int value = rd.Next(0, 10);
                validate += value.ToString();
            }

            return validate;
        }

        public static string[] SplitHelp(string str, char[] make)
        {
            return str.Split(make, StringSplitOptions.RemoveEmptyEntries);
        }

        public class IpArea
        {
            public IpAreaModel data { get; set; }
        }

        public class IpAreaModel
        {
            public string area { get; set; }
            public string city { get; set; }

            public string region { get; set; }
        }
    }
}
