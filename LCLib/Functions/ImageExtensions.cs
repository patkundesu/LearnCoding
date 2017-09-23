using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
namespace LCLib.Functions
{
    public static class ImageExtensions
    {
        public static Image ByteToImage(this byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);

            Image newimg = Image.FromStream(ms);

            return newimg;
        }
        public static byte[] ImageToByte(this Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        public static BitmapImage ImageToBitmap(this Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }
        public static BitmapImage ToBitmap(this System.Drawing.Bitmap bmp)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                BitmapImage ix = new BitmapImage();
                ix.BeginInit();
                ix.CacheOption = BitmapCacheOption.OnLoad;
                ix.StreamSource = ms;
                ix.EndInit();
                return ix;
            }
        }
    }
}
