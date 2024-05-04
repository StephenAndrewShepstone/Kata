using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal void MergeSortedLinkedList(SortedSinglyLinkedList<T> outerLinkedList)
        {
            if (outerLinkedList is null) throw new ArgumentNullException(nameof(outerLinkedList));

            if (Head is null)
            {
                Head = outerLinkedList.Head;
                return;
            }
            if (outerLinkedList.Head is null)
            {
                return;
            }

            SinglyLinkedListNode<T> innerNode = Head;
            SinglyLinkedListNode<T> outerNode = outerLinkedList.Head;
            SinglyLinkedListNode<T> swapNode;
            while (innerNode.Next is not null && outerNode is not null)
            {
                if (innerNode.Next.Value.CompareTo(outerNode.Value) == 1)
                {
                    swapNode = innerNode.Next;
                    innerNode.Next = outerNode;
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
            if (innerNode.Next is null) innerNode.Next = outerNode;
        }
    }
}
