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

    [TestMethod] public void TestAB()
    {
      Assert.AreEqual(Combinatronics.A(0), Combinatronics.C(0));
      Assert.AreEqual(Combinatronics.A(1), Combinatronics.C(1));
      Assert.AreEqual(Combinatronics.A(2), Combinatronics.C(2));
      Assert.AreEqual(Combinatronics.A(3), Combinatronics.C(3));
      Assert.AreEqual(Combinatronics.A(4), Combinatronics.C(4));
      Assert.AreEqual(Combinatronics.A(5), Combinatronics.C(5));
      Assert.AreEqual(Combinatronics.A(6), Combinatronics.C(6));
      Assert.AreEqual(Combinatronics.A(7), Combinatronics.C(7));
      Assert.AreEqual(Combinatronics.A(8), Combinatronics.C(8));
      Assert.AreEqual(Combinatronics.A(9), Combinatronics.C(9));
      Assert.AreEqual(Combinatronics.A(10), Combinatronics.C(10));
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
          Assert.IsTrue(LP.IsFeasible(edges, LP.Feasible(edges)), $"{Environment.NewLine}{edges}");
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
          Assert.AreEqual(ExhaustiveSearch(edges).Length, LP.Solve(edges).Length, $"{Environment.NewLine}{edges}");
        }
      }

    }
  }
}