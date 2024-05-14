namespace Kata.Graphs
{
    /// <summary>
    /// Simple implementation of a singly linked list. It should be constrained to internal use for the graph.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SinglyLinkedListNode<T> where T : IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// The next node in the linked list chain.
        /// </summary>
        internal SinglyLinkedListNode<T>? Next { get; set; }

        /// <summary>
        /// The value of the current node.
        /// </summary>
        internal T Value { get; set; }

        /// <summary>
        /// Default constructor of the singly linked list node.
        /// </summary>
        internal SinglyLinkedListNode()
        {
        }

        /// <summary>
        /// Constructor taking in value in linked list.
        /// </summary>
        /// <param name="value"></param>
        internal SinglyLinkedListNode(T value)
        {
            Value = value;
        }
    }
}
