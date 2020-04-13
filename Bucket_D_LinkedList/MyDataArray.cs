using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_D
{
    class MyDataArray
{
        public int width;
        public int height;
        public LinkedList bs;
        public byte[] b;

        public MyDataArray (string fileName)
        {
            b = ReadFromFile(fileName);
            width = BitConverter.ToInt32(b, 0x0012); //paveikslėlio plotis
            height = BitConverter.ToInt32(b, 0x0016); //paveikslėlio aukštis
            int j = 54; //Nes antraste tokio ilgio
            bs = new LinkedList("orig.bin");

            for (int i = 0; i < width * height; i++)
            {
                bs.addNode(((b[j + 1]) << 8) + b[j]);
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
            int curr = 0;
            while (curr != -1)
            {
                byte[] p = BitConverter.GetBytes(bs.getNodeValue(curr));
                b[j] = p[0];
                b[j + 1] = p[1];
                j += 2;
                curr = bs.getNodeNext(curr);
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
            Console.WriteLine("Started sorting");
            int minValue = bs.getNodeValue(0); Helpers.operationsCounter++;
            int maxValue = bs.getNodeValue(0); Helpers.operationsCounter++;
            int curr = 0; Helpers.operationsCounter++;
            Console.WriteLine("Finding max, min values");
            while (curr != -1)
            {
                if (bs.getNodeValue(curr) > maxValue)
                {
                    maxValue = bs.getNodeValue(curr); Helpers.operationsCounter++;
                }
                if (bs.getNodeValue(curr) < minValue)
                {
                    minValue = bs.getNodeValue(curr); Helpers.operationsCounter++;
                }
                curr = bs.getNodeNext(curr); Helpers.operationsCounter++;
            }
            Console.WriteLine("Initiating buckets");
            LinkedList bucket = new LinkedList("Bucket");
            for (int i = 0; i < maxValue - minValue + 1; i++)
            {
                bucket.addNode(0);
            }

            Console.WriteLine("inserting values");
            curr = 0; Helpers.operationsCounter++;
            while (curr != -1)
            {
                int currentValue = bs.getNodeValue(curr);
                int count = bucket.getNodeValue(currentValue - minValue);
                count++;
                bucket.setNodeValue(currentValue - minValue, count); Helpers.operationsCounter++;
                curr = bs.getNodeNext(curr); Helpers.operationsCounter++;
            }
            Console.WriteLine("Finishing the sort");
            bs.DeleteLinkedList(); Helpers.operationsCounter++;
            for (int i = 0; i < maxValue - minValue + 1; i++)
            {
                for (int j = 0; j<bucket.getNodeValue(i); j++)
                {
                    bs.addNode(i); Helpers.operationsCounter++;
                }
            }
            Console.WriteLine("Sorting ended, operations done: " + Helpers.operationsCounter);
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
