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
