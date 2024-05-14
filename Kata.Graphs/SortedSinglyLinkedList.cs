namespace Kata.Graphs
{
    /// <summary>
    /// A linked list that it in some sorted order in which it's values define a sorted set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SortedSinglyLinkedList<T> where T : IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// The first node in the sorted linked list node.
        /// </summary>
        internal SinglyLinkedListNode<T>? Head { get; set; }

        /// <summary>
        /// The default constructor
        /// </summary>
        internal SortedSinglyLinkedList()
        {
            
        }

        /// <summary>
        /// Constuctor that takes the head of a chain of linked list nodes in a sorted order.
        /// </summary>
        /// <param name="headOfSortedLinkedList"></param>
        internal SortedSinglyLinkedList(SinglyLinkedListNode<T> headOfSortedLinkedList)
        {
            Head = headOfSortedLinkedList;
        }

        /// <summary>
        /// Performs inline merging of an outer linked list into this linked list.
        /// </summary>
        /// <param name="outerLinkedList"></param>
        /// <exception cref="ArgumentNullException"></exception>
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
