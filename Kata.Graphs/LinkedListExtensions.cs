using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Graphs
{
    internal static class LinkedListExtensions
    {
        internal static void MergeSortedLinkedLists<T>(this LinkedList<T> innerlinkedList, LinkedList<T> outerLinkedList) where T: IEquatable<T>, IComparable<T>
        {
            if (innerlinkedList is null) throw new ArgumentNullException(nameof(innerlinkedList));
            if (outerLinkedList is null) throw new ArgumentNullException(nameof(outerLinkedList));

            if (innerlinkedList.First is null)
            {
                innerlinkedList.First = outerLinkedList.First;
                return;
            }
            if (outerLinkedList.First is null)
            {
                return;
            }

            LinkedListNode<T> innerNode = innerlinkedList.First;
            LinkedListNode<T> outerNode = outerLinkedList.First;
            LinkedListNode<T> swapNode;
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
