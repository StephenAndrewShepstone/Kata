using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Graphs
{
    public class Graph
    {
        private Dictionary<char, LinkedList<char>> _verticesAndDirectNeighbours;

        public Graph(List<string> vertexTransitiveDependencies)
        {
            //Validate();
            _verticesAndDirectNeighbours = GetVerticesAndDirectNeighboursFromStringList(vertexTransitiveDependencies);
        }

        public void FlattenGraphDependencies()
        {
            var dependencyStack = new Stack<LinkedListNode<char>>();
            var visited = new HashSet<char>();
            foreach(KeyValuePair<char, LinkedList<char>> keyValuePair in _verticesAndDirectNeighbours)
            {
                if (visited.Contains(keyValuePair.Key))
                {
                    continue;
                }
                var currentNode = keyValuePair.Value.First;
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
                            LinkedList<char> innerList = _verticesAndDirectNeighbours[currentNode.Value];
                            LinkedList<char> outerList = keyValuePair.Value;
                            innerList.MergeSortedLinkedLists<char>(outerList);
                            currentNode = currentNode.Next;
                        }
                        else
                        {
                            dependencyStack.Push(currentNode);
                            visited.Add(currentNode.Value);
                            currentNode = _verticesAndDirectNeighbours[currentNode.Value].First;
                        }
                    }
                }
            }
        }

        //private void PerformDepthFirstSearchWithFlatten()
        //{

        //}

        private Dictionary<char, LinkedList<char>> GetVerticesAndDirectNeighboursFromStringList(List<string> vertexTransitiveDependencies)
        {
            var dictionary = new Dictionary<char, LinkedList<char>>();
            try
            {
                foreach (string vertexNeighbourString in vertexTransitiveDependencies)
                {
                    var neighbours = new LinkedList<char>();
                    for (int i = 1; i < vertexNeighbourString.Length; i += 2)
                    {
                        neighbours.AddLast(vertexNeighbourString[i]);
                    }
                    dictionary.Add(vertexNeighbourString[0], neighbours);
                }
                return dictionary;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("The list of strings could not be parsed as a valid graph representation.");
            }
        }
    }
}
