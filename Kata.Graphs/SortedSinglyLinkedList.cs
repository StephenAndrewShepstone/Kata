using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kata.Graphs
{
    internal class SortedSinglyLinkedList<T> where T : IEquatable<T>, IComparable<T>
    {
        internal SinglyLinkedListNode<T>? Head { get; set; }

        internal SortedSinglyLinkedList()
        {
            
        }

        internal SortedSinglyLinkedList(SinglyLinkedListNode<T> headOfSortedLinkedList)
        {
            Head = headOfSortedLinkedList;
        }

        //internal void MergeSortedLinkedList(SortedSinglyLinkedList<T> outerLinkedList, HashSet<char> visited)
        internal void MergeSortedLinkedList(SortedSinglyLinkedList<T> outerLinkedList)
        {
            if (outerLinkedList is null) throw new ArgumentNullException(nameof(outerLinkedList));

            if (outerLinkedList.Head is null)
            {
                return;
            }
            if (Head is null)
            {
                Head = new SinglyLinkedListNode<T>(outerLinkedList.Head.Value);
                return;
            }
            
            SinglyLinkedListNode<T> innerNode;
            SinglyLinkedListNode<T> outerNode;
            if (Head.Value.CompareTo(outerLinkedList.Head.Value) < 1)
            {
                innerNode = Head;
                outerNode = outerLinkedList.Head;
                if (innerNode.Value.Equals(outerNode.Value))
                {
                    outerNode = outerNode.Next;
                }
            }
            else
            {
                var newHead = new SinglyLinkedListNode<T>(outerLinkedList.Head.Value);
                newHead.Next = Head;
                Head = newHead;
                innerNode = Head;
                outerNode = outerLinkedList.Head;
                outerNode = outerNode.Next;
            }

            SinglyLinkedListNode<T> swapNode;
            while (innerNode.Next is not null && outerNode is not null)
            {
                //if (innerNode.Next.Value.CompareTo(outerNode.Value) == 1)
                //{
                //    swapNode = innerNode.Next;
                //    innerNode.Next = outerNode;
                //    outerNode = outerNode.Next;
                //    innerNode = innerNode.Next;
                //    innerNode.Next = swapNode;
                //}
                //else if (innerNode.Next.Value.CompareTo(outerNode.Value) == 0)
                //{
                //    swapNode = outerNode.Next;
                //    innerNode.Next = outerNode;
                //    outerNode.Next = innerNode.Next.Next;
                //    innerNode = innerNode.Next;
                //    outerNode = swapNode;
                //}
                //else
                //{
                //    innerNode = innerNode.Next;
                //}

                if (innerNode.Next.Value.CompareTo(outerNode.Value) > 0)
                {
                    swapNode = innerNode.Next;
                    innerNode.Next = new SinglyLinkedListNode<T>(outerNode.Value);
                    outerNode = outerNode.Next;
                    innerNode = innerNode.Next;
                    innerNode.Next = swapNode;
                }
                else if (innerNode.Next.Value.CompareTo(outerNode.Value) == 0)
                {
                    outerNode = outerNode.Next;
                }
                else
                {
                    innerNode = innerNode.Next;
                }
            }
            if (innerNode.Next is null)
            {
                while (outerNode is not null) 
                {
                    innerNode.Next = new SinglyLinkedListNode<T>(outerNode.Value);
                    innerNode = innerNode.Next;
                    outerNode = outerNode.Next;
                }
            }
        }
    }
}
