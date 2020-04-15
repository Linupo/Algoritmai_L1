using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_Op
{
    class MyDataArray
{
        public int width;
        public int height;
        public LinkedList<int> bs;
        public byte[] b;

        public MyDataArray (string fileName)
        {
            b = ReadFromFile(fileName);
            width = BitConverter.ToInt32(b, 0x0012); //paveikslėlio plotis
            height = BitConverter.ToInt32(b, 0x0016); //paveikslėlio aukštis
            int j = 54; //Nes antraste tokio ilgio
            bs = new LinkedList<int>();

            for (int i = 0; i < width * height; i++)
            {
                bs.Insert(((b[j + 1]) << 8) + b[j]);
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
            LinkedList<int>.Node<int> current = bs.Head;
            while(current != null)
            {
                byte[] p = BitConverter.GetBytes(current.Value);
                b[j] = p[0];
                b[j + 1] = p[1];
                j += 2;
                current = current.NextNode;
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
            Helpers.operationsCounter = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int minValue = bs.Head.Value; Helpers.operationsCounter++;
            int maxValue = bs.Head.Value; Helpers.operationsCounter++;
            LinkedList<int>.Node<int> current = bs.Head; Helpers.operationsCounter++;
            while(current != null)
            {
                if (current.Value > maxValue)
                {
                    maxValue = current.Value; Helpers.operationsCounter++;
                }
                if (current.Value < minValue)
                {
                    minValue = current.Value; Helpers.operationsCounter++;
                }
                current = current.NextNode; Helpers.operationsCounter++;
            }

            List<LinkedList<int>> buckets = new List<LinkedList<int>>(); Helpers.operationsCounter++;
            for (int i = 0; i< maxValue - minValue + 1; i++)
            {
                buckets.Add(new LinkedList<int>()); Helpers.operationsCounter++;
            }

            current = bs.Head; Helpers.operationsCounter++;
            while (current != null)
            {
                buckets[current.Value - minValue].Insert(current.Value); Helpers.operationsCounter++;
                current = current.NextNode; Helpers.operationsCounter++;
            }

            bs.DeleteLinkedList(); Helpers.operationsCounter++;
            for (int i = 0; i < maxValue - minValue + 1; i++)
            {
                if (buckets[i].Count > 0)
                {
                    current = buckets[i].Head; Helpers.operationsCounter++;
                    while (current != null)
                    {
                        bs.Insert(current.Value); Helpers.operationsCounter++;
                        current = current.NextNode; Helpers.operationsCounter++;
                    }
                }
            }
            Console.WriteLine("Time elapsed: " + watch.ElapsedMilliseconds);
            Console.WriteLine("Operations performed: " + Helpers.operationsCounter);
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
