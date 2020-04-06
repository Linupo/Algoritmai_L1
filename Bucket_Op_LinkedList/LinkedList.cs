using System;
using System.Collections.Generic;
using System.Text;

namespace Bucket_Op
{
    class LinkedList<T>
    {
        public Node<T> Head;
        public int Count;
        public bool IsEmpty
        {
            get
            { 
                return Head == null; Helpers.operationsCounter++;
            }
        }
        public LinkedList()
        {
            Head = null; Helpers.operationsCounter++;
        }

        public Node<T> Insert(T obj)
        {
            // Creates a link, sets its link to the first item and then makes this the first item in the list.
            Node<T> link = new Node<T>(obj); Helpers.operationsCounter++;
            link.NextNode = Head; Helpers.operationsCounter++;
            if (Head != null)
            {
                Head.PreviousNode = link; Helpers.operationsCounter++;
            }
            Head = link; Helpers.operationsCounter++;
            Count++; Helpers.operationsCounter++;
            return link;
        }

        public void DeleteLinkedList()
        {
            Head = null; Helpers.operationsCounter++;
        }

        public Node<T> Delete()
        {
            // Gets the first item, and sets it to be the one it is linked to
            Node<T> temp = Head; Helpers.operationsCounter++;
            if (Head != null)
            {
                Head = Head.NextNode; Helpers.operationsCounter++;
                if (Head != null)
                {
                    Head.PreviousNode = null; Helpers.operationsCounter++;
                }
            }
            Count--; Helpers.operationsCounter++;
            return temp;
        }

        public Node<T> Get(int i)
        {
            int index = 0; Helpers.operationsCounter++;
            Node<T> temp = Head; Helpers.operationsCounter++;
            while (temp!=null)
            {
                Helpers.operationsCounter++;
                if (index == i)
                    break;
                temp = temp.NextNode; Helpers.operationsCounter++;
                index++; Helpers.operationsCounter++;
            }
            return temp;
        }

        public void Set(int i, T val)
        {
            int index = 0; Helpers.operationsCounter++;
            Node<T> temp = Head; Helpers.operationsCounter++;
            while (temp != null)
            {
                Helpers.operationsCounter++;
                if (index == i)
                {
                    temp.Value = val; Helpers.operationsCounter++;
                    break;
                }
                temp = temp.NextNode; Helpers.operationsCounter++;
                index++; Helpers.operationsCounter++;
            }
        }

        public void InsertAfter(Node<T> link, T obj)
        {
            Helpers.operationsCounter++;
            if (link == null)
                return;
            Node<T> newLink = new Node<T>(obj); Helpers.operationsCounter++;
            newLink.PreviousNode = link; Helpers.operationsCounter++;
            // Update the 'after' link's next reference, so its previous points to the new one
            if (link.NextNode != null)
            {
                link.NextNode.PreviousNode = newLink; Helpers.operationsCounter++;
            }
            // Steal the next link of the node, and set the after so it links to our new one
            newLink.NextNode = link.NextNode; Helpers.operationsCounter++;
            link.NextNode = newLink; Helpers.operationsCounter++;
            Count++; Helpers.operationsCounter++;
        }

        public class Node<T>
        {
            public T Value { get; set; }
            public Node<T> PreviousNode { get; set; }
            public Node<T> NextNode { get; set; }

            public Node(T val)
            {
                Value = val; Helpers.operationsCounter++;
            }
        }
    }
}
