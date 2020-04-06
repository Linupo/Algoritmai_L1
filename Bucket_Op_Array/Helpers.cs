using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Bucket_Op
{
    static class Helpers
    {
        public static int operationCounter;

        static Random rnd = new Random();

        /// <summary>
        /// Converts the given picture to a 16bit image and writes it to a given filepath
        /// </summary>
        /// <param name="FilePath">given filePath</param>
        public static void ConvertTo16bit(string FilePath)
        {
            using (FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                byte[] b = new byte[file.Length];
                file.Read(b, 0, (int)file.Length);
                int width = BitConverter.ToInt32(b, 0x0012); //paveikslėlio plotis
                int height = BitConverter.ToInt32(b, 0x0016); //paveikslėlio aukštis
                int points = width * height;
                var a = new byte[points * 2];
                //Verčiame į 16bit
                Array.Copy(BitConverter.GetBytes(54 + points * 2), 0, b, 0x0002, 4); //File size in bytes
                Array.Copy(BitConverter.GetBytes(16), 0, b, 0x001C, 2);    //Bits per Pixel used to store palette entry information. This also identifies in an indirect way the number of possible colors. Possible values are:

                int j = 0;
                for (int i = 54; i < b.Length; i += 3)
                {
                    a[j + 1] = (byte)(((b[i + 2] & 0b11111000) >> 1) | (b[i + 1] >> 6));
                    a[j] = (byte)(((b[i + 1] & 0b00111000) << 2) | (b[i] >> 3));
                    j += 2;
                }

                using (FileStream file2 = new FileStream("16bit_" + FilePath, FileMode.Create, FileAccess.Write))
                {
                    file2.Seek(0, SeekOrigin.Begin);
                    file2.Write(b, 0, 54);
                    file2.Write(a, 0, a.Length);
                    file2.Close();
                }
            }
        }

        /// <summary>
        /// Generates random image of given dimensions
        /// </summary>
        /// <param name="height">image height</param>
        /// <param name="width">image width</param>
        static public void GenerateRandomImage(int height, int width)
        {
            Bitmap randomBitmap = new Bitmap(height, width, PixelFormat.Format16bppRgb555);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    randomBitmap.SetPixel(x, y, Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(rnd.Next(0x1000000)))));
                }
            }
            randomBitmap.Save("randomImage.bmp", ImageFormat.Bmp);
        }
    }
}
