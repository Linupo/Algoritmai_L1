using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Bucket_Op
{
    class Program
    {

        static void Main(string[] args)
        {
            var name = Path.GetFileNameWithoutExtension(args[0]);
            Bitmap image = new Bitmap(args[0]);
            image.Save(name + ".bmp", ImageFormat.Bmp);

            Helpers.ConvertTo16bit("IMG_2345.bmp");

            //MyDataArray myData = new MyDataArray("16bit_IMG_2345.bmp");
            //myData.BucketSort();
            //myData.WriteToFile("16bit_IMG_2345.bmp", "sorted");

            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("image size: " + 100 * (int)Math.Pow(2, i) * 100 * (int)Math.Pow(2, i));
                Helpers.GenerateRandomImage(100 * (int)Math.Pow(2, i), 100 * (int)Math.Pow(2, i));
                MyDataArray randomData = new MyDataArray("randomImage.bmp");
                randomData.BucketSort();
                randomData.WriteToFile("randomImage.bmp", "sorted");
            }

            Console.WriteLine("The application has ended succesfully.");
            Console.Read();
        }
    }
}
