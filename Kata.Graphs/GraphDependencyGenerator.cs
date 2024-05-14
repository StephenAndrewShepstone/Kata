using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Graphs
{
    public static class GraphDependencyGenerator
    {
        public static List<string> GetAllDependenciesOfVertices(List<string> vertexTransitiveDependencies)
        {
            var graph = new Graph(vertexTransitiveDependencies);
            graph.FlattenGraphDependencies();
            return graph.GetDependencyListOfAllVertexDependencies();
        }
    }
}
