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
