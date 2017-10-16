using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace LS.UtilityTools
{
    public static class EnumHelper
    {
        /// <summary>
        ///  获取枚举类型上面的描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum obj)
        {
            return GetDescriptions(obj).LastOrDefault();
        }

        public static string GetDescriptionByValue<T>(int value) where T : struct
        {
            var t = typeof(T);

            var enumObj = Enum.Parse(t, value.ToString());

            return (enumObj as Enum).GetDescription();
        }

        public static IList<String> GetDescriptions(this Enum obj)
        {
            if (obj == null)
            {
                return new List<String>();
            }

            String[] flags = new Regex(", ").Split(obj.ToString());

            Type t = obj.GetType();

            List<String> descriptions = new List<string>(flags.Length);

            foreach (var s in flags)
            {
                FieldInfo fi = t.GetField(s);

                DescriptionAttribute[] arrDesc =
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);


                descriptions.Add(arrDesc.Length == 1 ? arrDesc[0].Description : "");
            }

            return descriptions;
        }

        public static IEnumerable<T> GetEnums<T>(this Enum obj)
        {
            String[] flags = new Regex(", ").Split(obj.ToString());

            Type t = typeof(T);

            return flags.Select(s => (T)Enum.Parse(t, s));
        }

        public static IEnumerable<T> GetValues<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }


}