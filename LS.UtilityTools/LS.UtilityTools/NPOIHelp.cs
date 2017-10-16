using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LS.UtilityTools
{
    public class NPOIHelp
    {
        /// <summary>
        /// 反射 NPOID导出  需要在模型上打上特性标签 ExportExcelAttribute
        /// </summary>
        /// <typeparam name="T">数据 集合类型</typeparam>
        /// <param name="modelList">数据集合</param>
        /// <param name="path">文件保存地址</param>
        /// <returns></returns>
        public static bool Export<T>(IList<T> modelList, string path)
        {

            HSSFWorkbook xssfWorkbook = new HSSFWorkbook();//定义一个工作薄
            HSSFSheet xssfSheet = (HSSFSheet)xssfWorkbook.CreateSheet("sheet1");//定义一个表单
            NPOI.SS.UserModel.IRow titleRow = xssfSheet.CreateRow(0);//创建一个行   
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();//取出字段数组
            int index = 0;//定义索引
            foreach (var propertyInfo in propertyInfos)//对数组进行遍历
            {
                Object[] exportExcelAttributes = propertyInfo.GetCustomAttributes(true);
                if (exportExcelAttributes.Count() > 0)
                {
                    foreach (var exportExcelAttribute in exportExcelAttributes)
                    {
                        var attribute = exportExcelAttribute as ExportExcelAttribute;
                        if (attribute != null)
                        {
                            titleRow.CreateCell(index++).SetCellValue(attribute.TitleName);
                        }
                    }
                }
            }

            for (int j = 0; j < modelList.Count(); j++)//传入的List遍历
            {
                NPOI.SS.UserModel.IRow Row = xssfSheet.CreateRow(j + 1);//创建列

                index = 0;//重新设定索引

                foreach (var propertyInfo in propertyInfos)
                {
                    Object[] exportExcelAttributes = propertyInfo.GetCustomAttributes(true);
                    if (exportExcelAttributes.Count() > 0)
                    {
                        foreach (var exportExcelAttribute in exportExcelAttributes)
                        {
                            var attribute = exportExcelAttribute as ExportExcelAttribute;
                            if (attribute != null)
                            {
                                object obj = propertyInfo.GetValue(modelList[j], null);//取出数据

                                var idListstr = obj as IList<string>;
                                //Type t = propertyInfo.GetType();
                                Type enTy = obj.GetType();
                                if (idListstr != null)
                                {
                                    IList<string> list = obj as List<string>;
                                    string str = string.Empty;
                                    foreach (string item in list)
                                    {
                                        str += item;
                                    }
                                    Row.CreateCell(index++).SetCellValue(str);//设定到相应的列
                                }
                                  
                                else if (enTy.IsEnum) 
                                {
                                                                     
                                    var enItem = Enum.Parse(enTy, obj.ToString());
                                   String[] flags = new Regex(", ").Split(enItem.ToString());

                                   Type t = obj.GetType();

                                   List<String> descriptions = new List<string>(flags.Length);

                                   foreach (var s in flags)
                                   {
                                       FieldInfo fi = t.GetField(s);

                                       DescriptionAttribute[] arrDesc =
                                           (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);


                                       descriptions.Add(arrDesc.Length == 1 ? arrDesc[0].Description : "");
                                   }
                                   Row.CreateCell(index++).SetCellValue(descriptions.LastOrDefault());//设定到相应的列
                                }

                                else if (obj != null)
                                {
                                    Row.CreateCell(index++).SetCellValue(obj.ToString());//设定到相应的列
                                }
                                else
                                {
                                    index++;
                                }
                            }
                        }
                    }
                }
            }

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                xssfWorkbook.Write(stream);   //向打开的这个xls文件中写入mySheet表并保存。
                //return File(Server.MapPath("/Export/日志导出文件.xls"), "application/vnd.ms-excel; charset=UTF-8", "企业管理数据.xls");
            }

            return true;
        }

    }


    [AttributeUsageAttribute(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class ExportExcelAttribute : Attribute
    {
        private readonly string titleName;

        public ExportExcelAttribute(string name)
        {
            titleName = name;
        }

        public string TitleName
        {
            get { return titleName; }
        }
    }
}
