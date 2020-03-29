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
            if(!File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                writer = new BinaryWriter(fs);
                reader = new BinaryReader(fs);
                SetCount(0);
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                writer = new BinaryWriter(fs);
                reader = new BinaryReader(fs);
            }
        }

        void SetCount(int value)
        {
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.Write(value);
        }

        public int GetCount()
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            return reader.ReadInt32();
        }

        public int ReadInt(int index)
        {
            reader.BaseStream.Seek(index * 4 + 4, SeekOrigin.Begin);
            return reader.ReadInt32();
        }
        
        public void writeIntAtEnd(int value)
        {
            int count = GetCount();
            writer.BaseStream.Seek(count * 4 + 4, SeekOrigin.Begin);
            writer.Write(value);
            count++;
            SetCount(count);
        }

        public void Close()
        {
            fs.Close();
            reader.Close();
            writer.Close();
        }

        public void WriteInt(int index, int value)
        {
            writer.BaseStream.Seek(index * 4 + 4, SeekOrigin.Begin);
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
