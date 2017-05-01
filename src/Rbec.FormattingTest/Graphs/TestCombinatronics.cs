using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  [TestClass]
  public class TestCombinatronics
  {
    public static (int, int) TriangleInvert(int n)
    {
      var m = 0;
      while (n > m)
        n -= ++m;
      return (n, m - n);
    }

    [TestMethod] public void TestTriangleInvert()
    {
      for (var i = 0; i < 100000; i++)
        Assert.AreEqual(TriangleInvert(i), Combinatronics.DecodeTriangle(i));
    }

    [TestMethod] public void TestTriangleInvertsTriangle()
    {
      for (var i = 0; i < 100000; i++)
        Assert.AreEqual(i, Combinatronics.EncodeTriangle(Combinatronics.DecodeTriangle(i)));
    }

    [TestMethod] public void TestIsFeasible()
    {

      var random = new Random();
      for (var k = 10; k >= 0; k--)
      {
        var m = k * (k + 1) / 2;

        var array = new int[m];

        for (var i = 0; i < 1000; i++)
        {
          for (var j = 0; j < array.Length; j++)
            array[j] = random.Next(10);
          var edges = new Edges(array);
          Assert.IsTrue(LP.IsFeasible(edges, LP.Feasible(edges)));
        }
      }

    }

    [TestMethod]
    public void TestSolutionIsFeasible()
    {

      var random = new Random();
      for (var k = 10; k >= 0; k--)
      {
        var m = k * (k + 1) / 2;

        var array = new int[m];

        for (var i = 0; i < 1000; i++)
        {
          for (var j = 0; j < array.Length; j++)
            array[j] = random.Next(10);
          var edges = new Edges(array);
          var solution = LP.Solve(edges);
          Assert.IsTrue(LP.IsFeasible(edges, solution), $"{Environment.NewLine}{edges}{Environment.NewLine}{Environment.NewLine}{solution}{Environment.NewLine}{LP.Feasible(edges)}");
        }
      }

    }

    public static Solution ExhaustiveSearch(Edges edges)
    {
      foreach (var sol in LP.All(edges.Count, 5))
      {
        if (LP.IsFeasible(edges, sol))
          return sol;
      }
      throw new NotImplementedException();
    }

    [TestMethod] public void TestIsOptimal()
    {

      var random = new Random();
      for (var k = 0; k < 6; k++)
      {
        var m = k * (k + 1) / 2;

        var array = new int[m];

        for (var i = 0; i < 1000; i++)
        {
          for (var j = 0; j < array.Length; j++)
            array[j] = random.Next(6);
          var edges = new Edges(array);
          var solution = LP.Solve(edges);
          Assert.AreEqual(ExhaustiveSearch(edges).Length, solution.Length);
        }
      }

    }

    //[TestMethod] public void TestSolve()
    //{
    //  var random = new Random();
    //  var k = 10000;
    //  var m = k * (k + 1) / 2;

    //  var array = new int[m];

    //  for (var i = 0; i < 1; i++)
    //  {
    //    for (var j = 0; j < array.Length; j++)
    //      array[j] = random.Next(100);
    //    var edges = new Edges(array);
    //    Assert.IsTrue(LP.IsFeasible(edges, LP.Solve(edges)));
    //  }

    //}
  }
}