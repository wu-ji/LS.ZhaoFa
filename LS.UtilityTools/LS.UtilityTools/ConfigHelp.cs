using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace LS.UtilityTools
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelp
    {
        private readonly string  ConfigPath = string.Empty;

        //private static Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

        /// <summary>
        /// 创建配置文件目录
        /// </summary>
        /// <param name="path"></param>
        public ConfigHelp(string path)
        {
            this.ConfigPath = path;
        }
        /// <summary>
        /// 获得 当前程序默认配置文件ConnectionStrings节点下的数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
        public static string GetValueByNameInConnectionStrings(string name)
        {
            //System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigPath);
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// 获得 当前程序默认配置文件AppSettings节点下的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueByNameInAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获得 指定路径下Web.config文件中 AppSettings节点下的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static string GetValueByNameInAppSettings(string key, string configPath)
        {
            return WebConfigurationManager.OpenWebConfiguration(configPath).AppSettings.Settings[key].Value; ;
            //return config1.AppSettings.Settings[key].Value;  //功能：读取配置文件获取签名证书路径
        }
    }
}
