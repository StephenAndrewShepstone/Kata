namespace Kata.Graphs
{
    internal class SinglyLinkedListNode<T> where T : IEquatable<T>, IComparable<T>
    {
        internal SinglyLinkedListNode<T>? Next { get; set; }

        internal T Value { get; set; }

        internal SinglyLinkedListNode()
        {
        }

        internal SinglyLinkedListNode(T value)
        {
            Value = value;
        }
    }
}
