using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Graphs
{
    internal class SinglyLinkedListNode<T> where T : IEquatable<T>, IComparable<T>
    {
        internal SinglyLinkedListNode<T>? Next { get; set; }

        internal T Value { get; set; }

        internal SinglyLinkedListNode()
        {
            Value = Value;
        }

        internal SinglyLinkedListNode(T Value)
        {
            Value = Value;
        }
    }
}
