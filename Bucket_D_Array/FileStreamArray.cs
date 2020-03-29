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
        public int Count;

        public FileStreamArray (string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            writer = new BinaryWriter(fs);
            reader = new BinaryReader(fs);
            Count = 0;
        }

        public int ReadInt(int index)
        {
            reader.BaseStream.Seek(index * 4, SeekOrigin.Begin);
            return reader.ReadInt32();
        }
        
        public void writeIntAtEnd(int value)
        {
            writer.BaseStream.Seek(Count * 4, SeekOrigin.Begin);
            writer.Write(value);
            Count++;
        }

        public void Close()
        {
            fs.Close();
            reader.Close();
            writer.Close();
        }

        public void WriteInt(int index, int value)
        {
            writer.BaseStream.Seek(index * 4, SeekOrigin.Begin);
            writer.Write(value);
        }

        public void ArrayToFile(int[] array)
        {
            for(int i = 0; i<array.Length;i++)
            {
                WriteInt(i, array[i]);
            }
        }
    }
}
