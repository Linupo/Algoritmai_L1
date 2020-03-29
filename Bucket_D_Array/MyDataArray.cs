using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_D
{
    class MyDataArray
{
        public FileStreamArray original;
        public int width;
        public int height;
        public byte[] b;

        public MyDataArray (string fileName)
        {
            b = ReadFromFile(fileName);
            width = BitConverter.ToInt32(b, 0x0012); //paveikslėlio plotis
            height = BitConverter.ToInt32(b, 0x0016); //paveikslėlio aukštis
            int j = 54; //Nes antraste tokio ilgio
            int[] bs = new int[width * height];

            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = ((b[j + 1]) << 8) + b[j];
                j += 2;
            }

            original = new FileStreamArray("original.bin");
            Console.WriteLine("Writing bs array to file");
            original.ArrayToFile(bs);
        }

        /// <summary>
        ///Writes the given byte array into a file of given names 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="filename"></param>
        public void WriteToFile(string filename, string filePrefix)
        {
            Console.WriteLine("Writing to file");
            int j = 54;
            for (int i = 0; i < width * height; i++)
            {
                byte[] p = BitConverter.GetBytes(original.ReadInt(i));
                b[j] = p[0];
                b[j + 1] = p[1];
                j += 2;
            }

            using (FileStream file = new FileStream(filePrefix + filename, FileMode.Create, FileAccess.Write))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.Write(b, 0, b.Length);
                file.Close();
            }
        }

        public void BucketSort()
        {
            Console.WriteLine("Started sorting");
            int minValue = original.ReadInt(0);
            int maxValue = original.ReadInt(0);

            for (int i = 1; i < height * width; i++)
            {
                if (original.ReadInt(i) > maxValue)
                    maxValue = original.ReadInt(i);
                if (original.ReadInt(i) < minValue)
                    minValue = original.ReadInt(i);
            }

            FileStreamArray[] buckets = new FileStreamArray[maxValue - minValue + 1];

            Console.WriteLine("initializing empty buckets");
            for (int i = 0; i < buckets.Length; i++)
            {
                FileStreamArray bucket = new FileStreamArray("Buckets/bucket" + i + ".bin");
                buckets[i] = bucket;
            }
            Console.WriteLine("Filling buckets");
            for (int i = 0; i < width * height; i++)
            {
                int origValue = original.ReadInt(i);
                buckets[origValue - minValue].writeIntAtEnd(origValue);
            }
            Console.WriteLine("Finishing the sort");
            int k = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i].Count > 0)
                {
                    for (int j = 0; j < buckets[i].Count; j++)
                    {
                        original.WriteInt(k, buckets[i].ReadInt(j));
                        k++;
                    }
                }
            }
            Console.WriteLine("Sort finished");
        }

        /// <summary>
        /// Reads bytes from file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>byte array from file</returns>
        static byte[] ReadFromFile(string filePath)
        {
            byte[] b;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                b = new byte[file.Length];
                file.Read(b, 0, (int)file.Length);
                file.Close();
            }
            return b;
        }
    }
}
