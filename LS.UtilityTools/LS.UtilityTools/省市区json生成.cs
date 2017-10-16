using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace LS.UtilityTools
{
    public class CityJson
    {
        public object CityInfo()
        {
            HttpWebRequest httpWebRequest = WebRequest.Create("http://www.stats.gov.cn/tjsj/tjbz/xzqhdm/201703/t20170310_1471429.html") as HttpWebRequest;
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
                var resultModel = GetProvinceCodeModel(txt);

                return resultModel;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private List<ProvinceCodeModel> GetProvinceCodeModel(string txt)
        {
            List<ProvinceCodeModel> ProvinceCodeModelList = new List<ProvinceCodeModel>();  // ProvinceCodeModel();

            Regex eg = new Regex(@"<p class=""MsoNormal""><b><span lang=""EN-US"">([\s\S]+?)<span>&nbsp;&nbsp;&nbsp;&nbsp; </span></span></b><b><span style=""font-family: 宋体"">([\s\S]+?)</span></b></p>([\s\S]+?)(?=<p class=""MsoNormal""><b>)");
            //return eg.Match(redirctUrl).Groups[1].Value;
            var list = eg.Matches(txt);
            foreach (Match item in list)
            {
                ProvinceCodeModel proModel = new ProvinceCodeModel()
                {
                    Province = item.Groups[2].Value,
                    CityList = new List<CityCodeModel>()
                };

                string cityTxt = item.Groups[3].Value;
                Regex egCity = new Regex(@"<p class=""MsoNormal""><span style=""font-family: 宋体"">　</span><span lang=""EN-US"">(.+?)<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=""font-family: 宋体"">　(.+?)</span></p>([\s\S]+?)(?=<p class=""MsoNormal""><span style=""font-family: 宋体"">　</span><span lang=""EN-US"">(.+?)<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>|$)");

                var listCity = egCity.Matches(cityTxt);
                foreach (Match itemCity in listCity)
                {
                    CityCodeModel cityModel = new CityCodeModel()
                    {
                        CityName = itemCity.Groups[2].Value,
                        AreaList = new List<AreaCodeModel>()
                    };
                    string AreaTxt = itemCity.Groups[3].Value;
                    Regex egArea = new Regex(@"<p class=""MsoNormal""><span style=""font-family: 宋体"">　　</span><span lang=""EN-US"">([\s\S]+?)<span>&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=""font-family: 宋体"">　　([\s\S]+?)</span></p>");
                    var listArea = egArea.Matches(AreaTxt);
                    foreach (Match itemArea in listArea)
                    {
                        AreaCodeModel AreaModel = new AreaCodeModel()
                        {
                            AreaName = itemArea.Groups[2].Value
                        };
                        cityModel.AreaList.Add(AreaModel);
                    }

                    proModel.CityList.Add(cityModel);
                }

                ProvinceCodeModelList.Add(proModel);
            }

            return ProvinceCodeModelList;
        }



        private string GetProvinceCodeModelJson(string txt)
        {
            Dictionary<string, object> cityAll = new Dictionary<string, object>();

            Regex eg = new Regex(@"<p class=""MsoNormal""><b><span lang=""EN-US"">([\s\S]+?)<span>&nbsp;&nbsp;&nbsp;&nbsp; </span></span></b><b><span style=""font-family: 宋体"">([\s\S]+?)</span></b></p>([\s\S]+?)(?=<p class=""MsoNormal""><b>)");
            //return eg.Match(redirctUrl).Groups[1].Value;
            var list = eg.Matches(txt);
            foreach (Match item in list) //处理省
            {
                var dic1 = new Dictionary<string, object>();
                cityAll.Add(item.Groups[2].Value, dic1);
                ProvinceCodeModel proModel = new ProvinceCodeModel()
                {
                    Province = item.Groups[2].Value,
                    CityList = new List<CityCodeModel>()
                };

                string cityTxt = item.Groups[3].Value; //省名

                Regex egCity = new Regex(@"<p class=""MsoNormal""><span style=""font-family: 宋体"">　</span><span lang=""EN-US"">(.+?)<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=""font-family: 宋体"">　(.+?)</span></p>([\s\S]+?)(?=<p class=""MsoNormal""><span style=""font-family: 宋体"">　</span><span lang=""EN-US"">(.+?)<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span>|$)");

                var listCity = egCity.Matches(cityTxt);
                foreach (Match itemCity in listCity)  //处理市
                {
                    var arr = new List<string>();
                    dic1.Add(itemCity.Groups[2].Value, arr);

                    string AreaTxt = itemCity.Groups[3].Value;
                    Regex egArea = new Regex(@"<p class=""MsoNormal""><span style=""font-family: 宋体"">　　</span><span lang=""EN-US"">([\s\S]+?)<span>&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=""font-family: 宋体"">　　([\s\S]+?)</span></p>");
                    var listArea = egArea.Matches(AreaTxt);
                    foreach (Match itemArea in listArea)  //处理市区
                    {
                        arr.Add(itemArea.Groups[2].Value);
                    }

                }

            }
            string json = JsonConvert.SerializeObject(cityAll);
            return json;
        }

        /// post键值对(如 orderId=1&adb=问问&detail=[1,2]) 转换成对应的json {order:"1",adb:"问问",detail:[1,2]} 
        /// </summary>
        /// <param name="postValue"></param>
        /// <returns></returns>
        private string GetJsonByPostValue(string postValue)
        {
            Regex rx = new Regex("=(?!\"|{|\\[|&)");
            Regex rx2 = new Regex("(?<!\"|}|]|=)&");
            Regex rx3 = new Regex("(?<!\"|}|]|=)$");
            var requestContent = rx.Replace(postValue, ":\"");
            requestContent = rx2.Replace(requestContent, "\",");
            requestContent = requestContent.Replace("=&", ":null,");
            requestContent = requestContent.Replace("=", ":").Replace("&", ",");
            if (rx3.IsMatch(requestContent))
                requestContent = "{" + requestContent + "\"}";
            else
                requestContent = "{" + requestContent + "}";

            return requestContent;
        }




        public class ProvinceCodeModel
        {
            public string Province { get; set; }

            public List<CityCodeModel> CityList { get; set; }
        }

        public class CityCodeModel
        {
            public string CityName { get; set; }

            public List<AreaCodeModel> AreaList { get; set; }
        }

        public class AreaCodeModel
        {
            public string AreaName { get; set; }
        }
    }
}





