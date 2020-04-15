using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_Op
{
    class MyDataArray
{
        public int[] dataArray;
        public int width;
        public int height;
        public int[] bs;
        public byte[] b;

        public MyDataArray (string fileName)
        {
            b = ReadFromFile(fileName);
            width = BitConverter.ToInt32(b, 0x0012); //paveikslėlio plotis
            height = BitConverter.ToInt32(b, 0x0016); //paveikslėlio aukštis
            int j = 54; //Nes antraste tokio ilgio
            bs = new int[width * height];

            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = ((b[j + 1]) << 8) + b[j];
                j += 2;
            }

        }

        /// <summary>
        ///Writes the given byte array into a file of given names 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="filename"></param>
        public void WriteToFile(string filename, string filePrefix)
        {

            int j = 54;
            for (int i = 0; i < bs.Length; i++)
            {
                byte[] p = BitConverter.GetBytes(bs[i]);
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int minValue = bs[0]; Helpers.operationCounter++;
            int maxValue = bs[0]; Helpers.operationCounter++;

            for (int i = 1; i < bs.Length; i++)
            {
                Helpers.operationCounter++;
                if (bs[i] > maxValue) 
                {
                    maxValue = bs[i]; Helpers.operationCounter++;
                }
                Helpers.operationCounter++;
                if (bs[i] < minValue)
                {
                    minValue = bs[i]; Helpers.operationCounter++;
                }
            }

            List<int>[] bucket = new List<int>[maxValue - minValue + 1]; Helpers.operationCounter++;

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>(); Helpers.operationCounter++;
            }

            for (int i = 0; i < bs.Length; i++)
            {
                bucket[bs[i] - minValue].Add(bs[i]); Helpers.operationCounter++;
            }

            int k = 0; Helpers.operationCounter++;
            for (int i = 0; i < bucket.Length; i++)
            {
                Helpers.operationCounter++;
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        bs[k] = bucket[i][j]; Helpers.operationCounter++;
                        k++; Helpers.operationCounter++;
                    }
                }
            }
            Console.WriteLine("Time elapsed: " + watch.ElapsedMilliseconds);
            Console.WriteLine("Operations performed: " + Helpers.operationCounter);
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
