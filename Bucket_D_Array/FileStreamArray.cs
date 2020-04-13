using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_D
{
    class FileStreamArray
    {
        public FileStream fs;
        public BinaryWriter writer;
        public BinaryReader reader;

        public FileStreamArray (string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite); Helpers.operationsCounter++;
            writer = new BinaryWriter(fs); Helpers.operationsCounter++;
            reader = new BinaryReader(fs); Helpers.operationsCounter++;
        }

        public int ReadInt(int index)
        {
            reader.BaseStream.Seek(index * 4, SeekOrigin.Begin); Helpers.operationsCounter++;
            return reader.ReadInt32();
        }

        public void Close()
        {
            fs.Close();
            reader.Close();
            writer.Close();
        }

        public void WriteInt(int index, int value)
        {
            writer.BaseStream.Seek(index * 4, SeekOrigin.Begin); Helpers.operationsCounter++;
            writer.Write(value); Helpers.operationsCounter++;
        }

        public void ArrayToFile(int[] array)
        {
            for(int i = 0; i<array.Length;i++)
            {
                WriteInt(i, array[i]); Helpers.operationsCounter++;
            }
        }
    }
}
