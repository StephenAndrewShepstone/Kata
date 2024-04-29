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
         */
    }
}
