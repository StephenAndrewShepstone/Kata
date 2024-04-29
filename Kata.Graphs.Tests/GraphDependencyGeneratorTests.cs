using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Graphs.Tests
{
    [TestClass]
    public class GraphDependencyGenerator
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
    }
}
