namespace Kata.Graphs
{
    /// <summary>
    /// Static helper class for getting different representations for the graph.
    /// </summary>
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
