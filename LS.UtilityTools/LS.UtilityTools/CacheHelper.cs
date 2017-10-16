using System;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System.Text.RegularExpressions;

namespace LS.UtilityTools
{
    public class CacheHelper
    {
        /// <summary>
        /// 建立缓存
        /// </summary>
        public static void TryAddCache(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemovedCallback)
        {
            if ((!String.IsNullOrWhiteSpace(key)) && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemovedCallback);
            }
        }

        public static void TryAddCache(string key, object value, DateTime enddate)
        {
            if ((!String.IsNullOrWhiteSpace(key)) && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, enddate, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
        }

        public static void TryAddCache(string key, object value, TimeSpan timeSpan)
        {
            if ((!String.IsNullOrWhiteSpace(key)) && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, timeSpan, CacheItemPriority.Normal, null);
            }
        }

        public static void TryAddCache(string key, object value, CacheDependency dependencies)
        {
            if ((!String.IsNullOrWhiteSpace(key)) && value != null)
            {
                HttpRuntime.Cache.Insert(key, value, dependencies);
            }
        }

        public static object Update(String key, DateTime enddate)
        {
            var v = GetCache(key);

            if (v == null)
            {
                return null;
            }

            TryAddCache(key, v, enddate);

            return v;
        }


        public static object GetCache(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return null;
            }


            return HttpRuntime.Cache.Get(key);
        }



        /// <summary>
        /// 移除缓存
        /// </summary>
        public static Object TryRemoveCache(string key)
        {
            var obj = GetCache(key);

            if (obj != null)
            {
                HttpRuntime.Cache.Remove(key);
            }

            return obj;
        }


        /// <summary>
        /// 移除键中带某关键字的缓存
        /// </summary>
        public static void RemoveMultiCache(string keyInclude)
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (CacheEnum.Key.ToString().IndexOf(keyInclude.ToString()) >= 0)
                    HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }

        public static void RemoveMultiCache(Regex regex)
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (regex.IsMatch(CacheEnum.Key.ToString()))
                    HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());

            }
        }


        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}

