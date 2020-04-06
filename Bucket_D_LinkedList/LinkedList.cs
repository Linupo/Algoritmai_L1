using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bucket_D
{
    class LinkedList
    {
        public FileStream fs;
        public BinaryWriter writer;
        public BinaryReader reader;
        public int Count;

        //XXXX YYYY   X - represents value, Y - represents next node number

        public void addNode(int value)
        {
            addNodeAt(Count, value, -1); Helpers.operationsCounter++;
            if (Count != 0)
            {
                setNodeNext(Count-1, Count); Helpers.operationsCounter++;
            }
            Count++; Helpers.operationsCounter++;

        }

        public void addNodeToNull(int value)
        {
            addNodeAt(Count++, value, -1); Helpers.operationsCounter++;
        }

        public void addNodeAt(int index, int value, int next)
        {
            int k = index * 8; Helpers.operationsCounter++;
            writeInt(k, value); Helpers.operationsCounter++;
            writeInt(k + 4, next++); Helpers.operationsCounter++;
        }

        public int getNodeValue(int index)
        {
            int k = index * 8; Helpers.operationsCounter++;
            return readInt(k);
        }

        public void setNodeValue(int index, int value)
        {
            int k = index * 8; Helpers.operationsCounter++;
            writeInt(k, value); Helpers.operationsCounter++;
        }

        public void setNodeNext(int index, int next)
        {
            int k = index * 8 + 4; Helpers.operationsCounter++;
            writeInt(k, next); Helpers.operationsCounter++;
        }

        public int getNodeNext(int index)
        {
            int k = index * 8; Helpers.operationsCounter++;
            return readInt(k + 4);
        }

        public int readInt(int index)
        {
            reader.BaseStream.Seek(index, SeekOrigin.Begin); Helpers.operationsCounter++;
            return reader.ReadInt32();
        }

        public void writeInt(int index, int value)
        {
            writer.BaseStream.Seek(index, SeekOrigin.Begin); Helpers.operationsCounter++;
            writer.Write(value); Helpers.operationsCounter++;
        }
        
        public void DeleteLinkedList()
        {
            Count = 0; Helpers.operationsCounter++;
        }

        public LinkedList(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite); Helpers.operationsCounter++;
            writer = new BinaryWriter(fs); Helpers.operationsCounter++;
            reader = new BinaryReader(fs); Helpers.operationsCounter++;
            Count = 0; Helpers.operationsCounter++;
        }

        public void Close()
        {
            fs.Close(); Helpers.operationsCounter++;
            reader.Close(); Helpers.operationsCounter++;
            writer.Close(); Helpers.operationsCounter++;
        }
    }
}
