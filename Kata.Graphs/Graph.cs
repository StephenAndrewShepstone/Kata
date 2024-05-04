using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kata.Graphs
{
    public class Graph
    {
        private Dictionary<char, SortedSinglyLinkedList<char>> _verticesAndDirectNeighbours;

        public Graph(List<string> vertexTransitiveDependencies)
        {
            //Validate();
            _verticesAndDirectNeighbours = GetVerticesAndDirectNeighboursFromStringList(vertexTransitiveDependencies);
        }

        public void FlattenGraphDependencies()
        {
            var dependencyStack = new Stack<SinglyLinkedListNode<char>>();
            var visited = new HashSet<char>();
            foreach(KeyValuePair<char, SortedSinglyLinkedList<char>> keyValuePair in _verticesAndDirectNeighbours)
            {
                if (visited.Contains(keyValuePair.Key))
                {
                    continue;
                }
                var currentNode = keyValuePair.Value.Head;
                //if (currentNode is not null)
                //{
                //    dependencyStack.Push(currentNode);
                //    visited.Add(currentNode.Value);
                //}
                while (dependencyStack.Count > 0 || currentNode is not null)
                {
                    if (currentNode is null)
                    {
                        currentNode = dependencyStack.Pop();
                    }
                    else
                    {
                        if (visited.Contains(currentNode.Value))
                        {
                            //var currentNode = dependencyStack.Peek();
                            //Todo: for better performance add another hashset to ensure unneccessary 
                            //merges aren't taking place if a node has been merged at a deeper level.
                            SortedSinglyLinkedList<char> innerList = _verticesAndDirectNeighbours[currentNode.Value];
                            SortedSinglyLinkedList<char> outerList = keyValuePair.Value;
                            innerList.MergeSortedLinkedList(outerList);
                            currentNode = currentNode.Next;
                        }
                        else
                        {
                            dependencyStack.Push(currentNode);
                            visited.Add(currentNode.Value);
                            currentNode = _verticesAndDirectNeighbours[currentNode.Value].Head;
                        }
                    }
                }
            }
        }

        //private void PerformDepthFirstSearchWithFlatten()
        //{

        //}

        private Dictionary<char, SortedSinglyLinkedList<char>> GetVerticesAndDirectNeighboursFromStringList(List<string> vertexTransitiveDependencies)
        {
            var dictionary = new Dictionary<char, SortedSinglyLinkedList<char>>();
            try
            {
                foreach (string vertexNeighbourString in vertexTransitiveDependencies)
                {
                    var kvp = CreateKeyValuePairFromSortedVertexDependentString(vertexNeighbourString);
                    dictionary.Add(kvp.Key, kvp.Value);
                }
                return dictionary;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("The list of strings could not be parsed as a valid graph representation.");
            }
        }

        /// <summary>
        /// Creates a sorted linked list of characters from a string of vertex dependencies. Fails in cases that the string dependencies aren't sorted and removes duplicates.
        /// </summary>
        /// <param name="sortedVertexDependentString"></param>
        /// <returns></returns>
        private static KeyValuePair<char, SortedSinglyLinkedList<char>> CreateKeyValuePairFromSortedVertexDependentString(string sortedVertexDependentString)
        {
            if (sortedVertexDependentString is null) throw new ArgumentNullException(nameof(sortedVertexDependentString));
            if (sortedVertexDependentString.Length < 1) throw new InvalidOperationException("The string cannot be empty.");
            if (sortedVertexDependentString.Length < 4)
            {
                var head = new SinglyLinkedListNode<char>(sortedVertexDependentString[2]);
                var sortedLinkedList = new SortedSinglyLinkedList<char>(head);
                sortedLinkedList.Head = head;
                return new KeyValuePair<char, SortedSinglyLinkedList<char>>(sortedVertexDependentString[0], sortedLinkedList);
            }

            var sortedVertexDependentHead = new SinglyLinkedListNode<char>(sortedVertexDependentString[0]);
            var currentNode = sortedVertexDependentHead;
            for (int i = 0; i < sortedVertexDependentString.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (sortedVertexDependentString[i] != ' ') throw new InvalidOperationException("The string is not a valid representation of a vertex and it's dependencies.");
                }
                else
                {            
                    var nextNode = new SinglyLinkedListNode<char>(sortedVertexDependentString[i]);
                    currentNode.Next = nextNode;
                    currentNode = nextNode;
                }
            }
            return new KeyValuePair<char, SortedSinglyLinkedList<char>>(sortedVertexDependentString[0], new SortedSinglyLinkedList<char>(sortedVertexDependentHead));
        }
    }
}
