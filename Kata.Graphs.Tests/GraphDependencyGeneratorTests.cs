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
        /*
            Independent variables
            VL - Number of vertex layers - 0, one, many
            VD Number of dependencies on each vertex - 0, one, many
            DL Number of dependent layers - 0, one, many
            DBL Number of dependencies between layers - 0, one, many
            DG - Disconnected graph - none, one, many
            DGL - Disconnected graph - layers - one, many
            DGD - Disconnected graph - none, one many
	        CG - Complete graph.

            CD Number of cyclic dependencies 0, one, many
	            - one with gap in line
	            - one no gap in line
	            - many complete
	            - many non-complete
	
            GDL - Gap between dependent layers

            Need at least many VL before VD can be considered.
            Need at least many VL before DL can be considered.
            DBL dependent on DL and VD.
            CD dependent on DBL and DL. 
            GDL dependent on VL and DL.

        VL 0
        null

        VL 1
        A - trivial

        VL many, VD 0
        A - multiple trivial
        B

        VL many, VD many, CD 1
        A B - two node cyclic
        B A

        VL many, VD one, CD 0
        A B - two node with dependency on non-sink.
        B C

        VL many, VD one, CD 0
        A B - two node with no shared dependencies
        C D

        VL many, VD one, CD 0
        A B - two node with shared dependent sink
        C B

        VL many, VD one, DL 1, CD 0
        A B - two node with dependency on other sink.
        B 

        Invalid graph
        A B - invalid graph representation duplicate base vertices.
        A 

        VL many, VD many, DL 1, CD 0
        A B C - two node with one shared dependency and one without dependency (may be redundant)
        C D

        VL many, VD many, DL Many, CD 0
        A B C - multilevel dependency
        C E F 
        E G H

        VL many, VD many, DL one, CD 0, DG 1, DGL 1, DGD 0
        A B E - multilevel dependency with disconnected node
        D
        E F G

        VL many, VD many, DL one, CD 0, DG 1, DGL 1, DGD 1
        A B E - multilevel dependency, single disconnected graph with disconnected dependencies
        D C
        E F G

        VL many, VD many, DL none, CD 0, DG many, DGL 1, DGD 1
        A B C - several disconnected graphs
        D A
        E F G

        VL many, VD many, DL one, CD 1, DG many, DGL 1, DGD 1
        A B C - multilevel dependency with cyclic component with gap between dependencies in array
        B E
        C A G

        VL many, VD many, DL many, CD many
        A B C - three-way cyclic dependency
        B D E 
        D A F          

        VL many, VD many, DL many, CD many, CG - true
        A B C - three-way cyclic dependency (3 vertex complete graph)
        B A C 
        C A B  

        VL many, VD many, DL many, CD many, CG - false, HS-true
        A B C - three-way cyclic dependency with single edge source 
        B A C 
        C A B  
        D A

         */

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
        [DataRow(0, DisplayName = "Empty graph")]
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
        public void GetAllVertexDependenciesTests(int testNumber)
        {
            GetAllVertexDependenciesTest testConfig = _tests[testNumber];

            //GraphDependencyGenerator graphDependencyGenerator = new GraphDependencyGenerator(testConfig.TestInput);
            //List<string> output = graphDependencyGenerator.GetAllDependencies();

            Assert.IsNotNull(testConfig);
            //Assert.AreEqual(testConfig.TestExpectedOutput.Count, output.Count, $"The number of vertices with dependent lists is not what was expected. Expected: ${testConfig.TestExpectedOutput.Count}, Actual: {output.Count}");

            //for (int i = 0; i < output.Count; i++)
            //{
            //    Assert.AreEqual(testConfig.TestExpectedOutput[i], output[i], $"Vertex {i} does not have the correct dependencies. Expected: {testConfig.TestExpectedOutput[i]}, Actual: {output[i]}.");
            //}
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
