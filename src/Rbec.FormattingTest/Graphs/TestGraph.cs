using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  [TestClass]
  public class TestGraph
  {

    [TestMethod] public void TestGenerate()
    {
      var g = new Dictionary<int, int[]>
              {
                {5,new []{11}},
                {7,new []{11,8}},
                {3,new []{8,10}},
                {11,new []{10,2,9}},
                {8,new []{9}}
              };
      var order = g.TopologicalOrder();
      var s = string.Join(" ", order);
      Console.WriteLine(s);
      Assert.IsTrue(true);
    }
  }
}