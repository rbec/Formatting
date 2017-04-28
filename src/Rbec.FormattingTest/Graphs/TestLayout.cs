using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  [TestClass]
  public class TestLayout
  {

    [TestMethod] public void TestGenerate()
    {
      var g = new Dictionary<Edge, int>
              {
                {new Edge(5,3), 2},
                {new Edge(3,9), 7}
              };
      var order = Layout.ToLayout(g);
      var s = string.Join(" ", order);
      Console.WriteLine(s);
    }
  }
}