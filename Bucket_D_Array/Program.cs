using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Bucket_D
{
    class Program
    {

        static void Main(string[] args)
        {
            var name = Path.GetFileNameWithoutExtension(args[0]);
            Bitmap image = new Bitmap(args[0]);
            image.Save(name + ".bmp", ImageFormat.Bmp);

            Helpers.GenerateRandomImage(1000, 1000);
            Helpers.ConvertTo16bit("IMG_2345.bmp");

            //MyDataArray myData = new MyDataArray("16bit_IMG_2345.bmp");
            //myData.BucketSort();
            //myData.WriteToFile("16bit_IMG_2345.bmp", "sorted");

            MyDataArray rndData = new MyDataArray("randomImage.bmp");
            rndData.BucketSort();
            rndData.WriteToFile("randomImage.bmp", "sorted");

            Console.WriteLine("The application has ended succesfully.");
            Console.Read();
        }
    }
}
