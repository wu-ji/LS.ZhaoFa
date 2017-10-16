using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LS.UtilityTools
{
    public static class PeddlerTagSource
    {
        public static PeddlerTagModel PeddlerTag { get; private set; }

        static PeddlerTagSource()
        {
            String v = GetSource();

            PeddlerTag = JsonConvert.DeserializeObject<PeddlerTagModel>(v);
        }

        private static String GetSource()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/PeddlerTag.json";

            return File.ReadAllText(path);
        }
    }

    public class PeddlerTagModel
    {
        /// <summary>
        /// 职业
        /// </summary>
        public Dictionary<String, String> Occupation { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public Dictionary<String, String> Industry { get; set; }

        /// <summary>
        /// 兴趣
        /// </summary>
        public Dictionary<String, String> Interest { get; set; }
    }
}