using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LS.ZhaoFaUnit
{
    /// <summary>
    /// PDF转换单元测试
    /// </summary>
    [TestClass]
    public class PDFTest
    {
        /// <summary>
        /// 根据 网址得到html  生成pdf
        /// </summary>
        [TestMethod]
        public void GetPdfTest()
        {
            // HttpClient httpClient = new HttpClient();
            // HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, "http://www.csdn.net/");

            //var html =  httpClient.SendAsync(msg).Result.Content.ReadAsStringAsync().Result;

            var html = File.ReadAllText("C:\\Users\\LF\\Desktop\\ZhaoFa\\LS.ZhaoFa\\LS.ZhaoFaUnit\\Html\\pdf.html");

            Assert.IsNotNull(html);

            byte[] pdf = ConvertHtmlTextToPDF(html);

            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/file/"))
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/file/");
            }

            string fileId =AppDomain.CurrentDomain.BaseDirectory + "/file/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            System.IO.File.WriteAllBytes(fileId, pdf);
        }

        /// <summary>  
        /// 将Html文字 输出到PDF档里  
        /// </summary>  
        /// <param name="htmlText"></param>  
        /// <returns></returns>  
        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免当htmlText无任何html tag标签的纯文字时，转PDF时会挂掉，所以一律加上<p>标签  
            htmlText = "<p>" + htmlText + "</p>";

            MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流  
            byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串转成byte[]  
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();//要写PDF的文件，建构子没填的话预设直式A4  
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件预设开档时的缩放为100%  

            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //开启Document文件   
            doc.Open();

            //使用XMLWorkerHelper把Html parse到PDF档里  
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

            //将pdfDest设定的资料写到PDF档  
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回传PDF档案   
            return outputStream.ToArray();

        }

        //设置字体类  
        public class UnicodeFontFactory : FontFactoryImp
        {
            private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
      "arialuni.ttf");//arial unicode MS是完整的unicode字型。  
            private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
            "KAIU.TTF");//标楷体  


            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
            {
                BaseFont bfChiness = BaseFont.CreateFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //可用Arial或标楷体，自己选一个  
                BaseFont baseFont = BaseFont.CreateFont(标楷体Path, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                return new Font(bfChiness, size, style, color);
            }
        }
    }
}
