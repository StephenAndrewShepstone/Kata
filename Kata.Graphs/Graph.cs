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
            var flattened = new HashSet<char>();
            foreach (KeyValuePair<char, SortedSinglyLinkedList<char>> keyValuePair in _verticesAndDirectNeighbours)
            {
                if (flattened.Contains(keyValuePair.Key))
                {
                    continue;
                }
                var currentNode = keyValuePair.Value.Head;
                
                //if (currentNode is not null)
                //{
                //    //dependencyStack.Push(currentNode);
                //    visited.Add(currentNode.Value);
                //}
                while (dependencyStack.Count > 0 || currentNode is not null)
                {
                    if (currentNode is null)
                    {
                        currentNode = dependencyStack.Pop();
                        flattened.Add(currentNode.Value);
                    }
                    else
                    {
                        if (visited.Contains(currentNode.Value))
                        {
                            //var currentNode = dependencyStack.Peek();
                            //Todo: for better performance add another hashset to ensure unneccessary 
                            //merges aren't taking place if a node has been merged at a deeper level.
                            //SortedSinglyLinkedList<char> innerList = _verticesAndDirectNeighbours[currentNode.Value];
                            SortedSinglyLinkedList<char> outerList;
                            if (_verticesAndDirectNeighbours.TryGetValue(currentNode.Value, out SortedSinglyLinkedList<char> dependencies))
                            {
                                outerList = dependencies;
                            }
                            else
                            {
                                outerList = new SortedSinglyLinkedList<char>();
                            }

                            SortedSinglyLinkedList<char> innerList = dependencyStack.Any() ? _verticesAndDirectNeighbours[dependencyStack.Peek().Value] : keyValuePair.Value;
                            innerList.MergeSortedLinkedList(outerList);
                            currentNode = currentNode.Next;
                        }
                        else
                        {
                            visited.Add(currentNode.Value);
                            //currentNode = _verticesAndDirectNeighbours[currentNode.Value].Head;
                            //Problem is potentially here. On cyclic dependency of A
                            if (!flattened.Contains(currentNode.Value))
                            {
                                dependencyStack.Push(currentNode);
                                if (_verticesAndDirectNeighbours.TryGetValue(currentNode.Value, out SortedSinglyLinkedList<char> dependencies))
                                {
                                    currentNode = dependencies != null ? dependencies.Head : null;
                                }
                                else
                                {
                                    flattened.Add(dependencyStack.Pop().Value);
                                    currentNode = currentNode.Next;
                                }
                            }
                        }
                    }
                }
                flattened.Add(keyValuePair.Key);
            }
        }

        public List<string> GetDependencyListOfAllVertexDependencies()
        {
            List<string> vertexDependencies = new List<string>();
            StringBuilder stringBuilder = new StringBuilder(); 
            foreach (char vertex in _verticesAndDirectNeighbours.Keys)
            {
                stringBuilder.Append(vertex);
                SinglyLinkedListNode<char> node = _verticesAndDirectNeighbours[vertex].Head;
                while (node is not null)
                {
                    stringBuilder.Append(' ');
                    stringBuilder.Append(node.Value);
                    node = node.Next;   
                }
                vertexDependencies.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            return vertexDependencies;
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
                var sortedLinkedList = new SortedSinglyLinkedList<char>();
                if (sortedVertexDependentString.Length > 1)
                {
                    var head = new SinglyLinkedListNode<char>(sortedVertexDependentString[2]);
                    sortedLinkedList.Head = head;
                }
                return new KeyValuePair<char, SortedSinglyLinkedList<char>>(sortedVertexDependentString[0], sortedLinkedList);
            }

            var sortedVertexDependentHead = new SinglyLinkedListNode<char>(sortedVertexDependentString[2]);
            var currentNode = sortedVertexDependentHead;
            for (int i = 4; i < sortedVertexDependentString.Length; i++)
            {
                if (i % 2 == 1)
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
