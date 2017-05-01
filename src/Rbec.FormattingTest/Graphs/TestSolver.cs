using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  [TestClass]
  public class TestSolver
  {
    public static (int, int) DecodeTriangle(int n)
    {
      var k = 0;
      while (n > k)
        n -= ++k;
      return (n, k - n);
    }

    [TestMethod] public void TestTriangleInvert()
    {
      for (var i = 0; i < 10000; i++)
        Assert.AreEqual(DecodeTriangle(i), Solver.DecodeTriangle(i));
    }

    [TestMethod] public void TestEncodeInvertsDecodeTriangle()
    {
      for (var i = 0; i < 10000; i++)
        Assert.AreEqual(i, Solver.EncodeTriangle(Solver.DecodeTriangle(i)));
    }

    [TestMethod] public void TestIsFeasible()
    {
      var random = new Random();
      for (var k = 10; k >= 0; k--)
      {
        var array = new int[k * (k + 1) / 2];

        for (var i = 0; i < 1000; i++)
        {
          for (var j = 0; j < array.Length; j++)
            array[j] = random.Next(10);
          var edges = new Problem(array);
          Assert.IsTrue(IsFeasible(edges, Solver.Feasible(edges)));
        }
      }
    }

    [TestMethod] public void TestSolutionIsFeasible()
    {
      var random = new Random();
      for (var k = 10; k >= 0; k--)
      {
        var array = new int[k * (k + 1) / 2];

        for (var i = 0; i < 1000; i++)
        {
          for (var j = 0; j < array.Length; j++)
            array[j] = random.Next(10);
          var edges = new Problem(array);
          var solution = Solver.Solve(edges);
          Assert.IsTrue(IsFeasible(edges, solution));
        }
      }
    }

    public static bool IsFeasible(Problem problem, Solution solution)
    {
      for (var k = 0; k < solution.Length; k++)
        for (var i = 0; i <= k; i++)
          for (var j = k; j < problem.Length; j++)
            if (solution[i, j] < problem[i, j])
              return false;
      return true;
    }

    public static IEnumerable<Solution> All(int n, int max)
    {
      var array = new int[n];

      while (true)
      {
        yield return new Solution(Solver.PrefixSum(array));
        var i = 0;
        while (array[i] == max)
        {
          array[i] = 0;
          i++;
          if (i == array.Length)
            yield break;
        }
        array[i]++;
      }
    }

    public static Solution ExhaustiveSearch(Problem problem)
    {
      foreach (var sol in All(problem.Length, 5))
      {
        if (IsFeasible(problem, sol))
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
          var edges = new Problem(array);
          var solution = Solver.Solve(edges);
          Assert.AreEqual(ExhaustiveSearch(edges).Length, solution.Length);
        }
      }
    }

    [TestMethod] public void TestTopologicalOrder()
    {
      var g = new Dictionary<int, int[]>
              {
                {5, new[] {11}},
                {7, new[] {11, 8}},
                {3, new[] {8, 10}},
                {11, new[] {10, 2, 9}},
                {8, new[] {9}}
              };
      var order = g.TopologicalOrder();
      var s = string.Join(" ", order);
      Console.WriteLine(s);
      Assert.IsTrue(true);
    }

    [TestMethod] public void TestGenerate()
    {
      var g = new[]
              {
                new Constraint(5, 3, 2),
                new Constraint(3, 9, 7)
              };
      var order = g.ToLayout();
      var s = string.Join(" ", order);
      Console.WriteLine(s);
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