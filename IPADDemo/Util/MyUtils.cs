using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Util
{
    public static class MyUtils
    {
        public static Bitmap Base64StringToImage(string basestr)
        {
            Bitmap bitmap = null;
            try
            {
                String inputStr = basestr;
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                bitmap = bmp;
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Base64StringToImage 转换失败\nException：" + ex.Message);
            }

            return bitmap;
        }

        public static string ImageToBase64String(this Image image)
        {
            return Convert.ToBase64String(image.ImageToBytes());
        }
    }
}
