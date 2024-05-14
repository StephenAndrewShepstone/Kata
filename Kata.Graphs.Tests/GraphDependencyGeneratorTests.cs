using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kata.Graphs.Tests
{
    [TestClass]
    public class GraphDependencyGeneratorTests
    {
        private static GetAllVertexDependenciesTest[]? _tests;

        [ClassInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            SetTests();
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            _tests = null;
        }

        [TestMethod]
        [DeploymentItem(@".\TestData\GetDependenciesTestData.json")]
        //[DataRow(0, DisplayName = "Empty graph")]
        [DataRow(1, DisplayName = "Trivial graph")]
        [DataRow(2, DisplayName = "Multiple vertex, no edges")]
        [DataRow(3, DisplayName = "Two nodes with cyclic dependency")]
        [DataRow(4, DisplayName = "Two nodes with dependency on non-sink")]
        [DataRow(5, DisplayName = "Two nodes with no shared dependencies")]
        [DataRow(6, DisplayName = "Two nodes with shared dependent sink")]
        [DataRow(7, DisplayName = "Two nodes with dependency on other sink")]
        [DataRow(8, DisplayName = "Two node with one shared dependency and one without dependency")]
        [DataRow(9, DisplayName = "Multilevel dependency")]
        [DataRow(10, DisplayName = "Multilevel dependency with disconnected node")]
        [DataRow(11, DisplayName = "Multilevel dependency, single disconnected graph with disconnected dependencies")]
        [DataRow(12, DisplayName = "Graph with several disconnections")]
        [DataRow(13, DisplayName = "Multilevel dependency with cyclic component with gap between dependencies in array")]
        [DataRow(14, DisplayName = "Three-way cyclic dependency")]
        [DataRow(15, DisplayName = "Three-way cyclic dependency (3 vertex complete graph)")]
        [DataRow(16, DisplayName = "Three-way cyclic dependency with single edge source")]
        [DataRow(17, DisplayName = "Kata test case")]
        [DataRow(18, DisplayName = "Kata test case 2")]
        public void GetAllVertexDependenciesTests(int testNumber)
        {
            GetAllVertexDependenciesTest testConfig = _tests[testNumber];

            List<string> output = GraphDependencyGenerator.GetAllDependenciesOfVertices(testConfig.TestInput);

            Assert.IsNotNull(testConfig);
            Assert.AreEqual(testConfig.TestExpectedOutput.Count, output.Count, $"The number of vertices with dependent lists is not what was expected. Expected: ${testConfig.TestExpectedOutput.Count}, Actual: {output.Count}");

            for (int i = 0; i < output.Count; i++)
            {
                Assert.AreEqual(testConfig.TestExpectedOutput[i], output[i], $"Vertex {i} does not have the correct dependencies. Expected: {testConfig.TestExpectedOutput[i]}, Actual: {output[i]}.");
            }
        }

        private static void SetTests()
        {
            using var textReader = new StreamReader("GetDependenciesTestData.json");
            var jsonSerializer = new JsonSerializer();
            _tests = (GetAllVertexDependenciesTest[]?)jsonSerializer.Deserialize(textReader, typeof(GetAllVertexDependenciesTest[]));
        }
    }

    [JsonObject]
    public class GetAllVertexDependenciesTest
    {
        public ushort? TestId { get; set; }

        public string? TestName { get; set; }

        public List<string>? TestInput { get; set; }

        public List<string>? TestExpectedOutput { get; set; }
    }
}
