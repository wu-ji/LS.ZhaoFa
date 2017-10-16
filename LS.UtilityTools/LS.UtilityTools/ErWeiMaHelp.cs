using com.google.zxing;
using com.google.zxing.common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools
{
    public class ErWeiMaHelp
    {

        public static MemoryStream GetMa(string content)
        {
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 350, 350);
            //writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png, "wjg2");
            System.Drawing.Bitmap Img = toBitmap(byteMatrix);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Img.Dispose();
            return ms;
            //Response.ClearContent();   //需要输出图象信息   要修改HTTP头   
            //Response.ContentType = "image/Png ";
            //Response.BinaryWrite(ms.ToArray());

            //Response.End();
        }
        public static void writeToFile(ByteMatrix matrix, System.Drawing.Imaging.ImageFormat format, string file)
        {
            System.Drawing.Imaging.EncoderParameters eps = new System.Drawing.Imaging.EncoderParameters();
            eps.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            Bitmap bmap = toBitmap(matrix);
            bmap.Save(file, format);
        }

        public static Bitmap toBitmap(ByteMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }

            return bmap;
        }

    }
}
