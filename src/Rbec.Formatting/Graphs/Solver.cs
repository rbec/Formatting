using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public static class Solver
  {
    public static int[] PrefixSum(int[] array)
    {
      var sum = new int[array.Length];
      if (sum.Length > 0)
        sum[0] = array[0];
      for (var i = 1; i < sum.Length; i++)
        sum[i] = sum[i - 1] + array[i];
      return sum;
    }

    public static Solution Solve(Problem problem) => Solve(problem, Feasible(problem));

    public static Solution Solve(Problem problem, Solution solution)
    {
      for (var k = 0; k < solution.Length; k++)
      {
        var δ = int.MaxValue;
        for (var i = 0; i <= k; i++)
          for (var j = 0; j < solution.Length - k; j++)
            δ = Math.Min(δ, solution[i, j] - problem[i, j]);
        if (δ > 0)
          solution.Delta(k, -δ);
      }
      return solution;
    }

    public static Solution Feasible(Problem problem)
    {
      var sol = new int[problem.Length];
      for (var i = 0; i < sol.Length; i++)
      {
        var max = 0;
        for (var j = 0; j < sol.Length - i; j++)
          max = Math.Max(max, problem[i, j]);
        sol[i] = max;
      }
      return new Solution(PrefixSum(sol));
    }

    public static int EncodeTriangle((int i, int j) coordinate) =>
      (coordinate.i + coordinate.j) * (coordinate.i + coordinate.j + 1) / 2 + coordinate.i;

    public static int TriangleHeight(int n) => ((int) Math.Sqrt(1.0 + 8 * n) - 1) / 2;

    public static (int i, int j) DecodeTriangle(int n)
    {
      var k = TriangleHeight(n);
      var i = n - k * (k + 1) / 2;
      return (i, k - i);
    }

    public static Dictionary<int, int> ToLayout(this IReadOnlyDictionary<Edge, int> constraints)
    {
      var graph = constraints.Keys.GroupBy(key => key.Head).ToDictionary(g => g.Key, g => g.Select(e => e.Tail).ToArray());
      var order = graph.TopologicalOrder();

      var problem = new Problem(order.Length - 1);

      foreach (var constraint in constraints)
      {
        var i = Array.IndexOf(order, constraint.Key.Head);
        var j = problem.Length - Array.IndexOf(order, constraint.Key.Tail);

        problem[i, j] = constraint.Value;
      }
      var sol = Solve(problem);
      //var k = order.Length - 1;

      //var array = new int[k * (k + 1) / 2];

      //foreach (var constraint in constraints)
      //{
      //  var i = Array.IndexOf(order, constraint.Key.Head);
      //  var j = k - Array.IndexOf(order, constraint.Key.Tail);

      //  array[(i + j) * (i + j + 1) / 2 + i] = constraint.Value;
      //}

      //var sol = Solve(new Problem(array));

      var dict = new Dictionary<int, int> {{order[0], 0}};
      for (var i = 0; i < sol.Length; i++)
        dict.Add(order[i + 1], sol.Array[i]);

      return dict;
    }

    public static int[] TopologicalOrder(this IReadOnlyDictionary<int, int[]> g)
    {
      var order = new List<int>();
      var unmarked = g.SelectMany(entry => entry.Value.Concat(new[] {entry.Key})).Distinct().ToList();

      while (unmarked.Count > 0)
        Visit(unmarked[0]);

      return ((IEnumerable<int>) order).Reverse().ToArray();

      void Visit(int n)
      {
        if (unmarked.Contains(n))
        {
          if (g.ContainsKey(n))
            foreach (var m in g[n])
              Visit(m);
          unmarked.Remove(n);
          order.Add(n);
        }
      }
    }
  }
}