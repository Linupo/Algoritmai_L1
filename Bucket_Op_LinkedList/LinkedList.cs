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
                return Head == null;
            }
        }
        public LinkedList()
        {
            Head = null;
        }

        public Node<T> Insert(T obj)
        {
            // Creates a link, sets its link to the first item and then makes this the first item in the list.
            Node<T> link = new Node<T>(obj);
            link.NextNode = Head;
            if (Head != null)
                Head.PreviousNode = link;
            Head = link;
            Count++;
            return link;
        }

        public void DeleteLinkedList()
        {
            Head = null;
        }

        public Node<T> Delete()
        {
            // Gets the first item, and sets it to be the one it is linked to
            Node<T> temp = Head;
            if (Head != null)
            {
                Head = Head.NextNode;
                if (Head != null)
                    Head.PreviousNode = null;
            }
            Count--;
            return temp;
        }

        public Node<T> Get(int i)
        {
            int index = 0;
            Node<T> temp = Head;
            while(temp!=null)
            {
                if(index == i)
                    break;
                temp = temp.NextNode;
                index++;
            }
            return temp;
        }

        public void Set(int i, T val)
        {
            int index = 0;
            Node<T> temp = Head;
            while (temp != null)
            {
                if (index == i)
                {
                    temp.Value = val;
                    break;
                }
                temp = temp.NextNode;
                index++;
            }
        }

        ///// New operations
        public void InsertAfter(Node<T> link, T obj)
        {
            if (link == null)
                return;
            Node<T> newLink = new Node<T>(obj);
            newLink.PreviousNode = link;
            // Update the 'after' link's next reference, so its previous points to the new one
            if (link.NextNode != null)
                link.NextNode.PreviousNode = newLink;
            // Steal the next link of the node, and set the after so it links to our new one
            newLink.NextNode = link.NextNode;
            link.NextNode = newLink;
            Count++;
        }

        public class Node<T>
        {
            public T Value { get; set; }
            public Node<T> PreviousNode { get; set; }
            public Node<T> NextNode { get; set; }

            public Node(T val)
            {
                Value = val;
            }
        }
    }
}
