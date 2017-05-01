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

    public static Solution Solve(Edges edges) => Solve(edges, Feasible(edges));

    public static Solution Solve(Edges edges, Solution sol)
    {
      var l = edges.Count;
      for (var k = 0; k < sol.Length; k++)
      {
        var δ = int.MaxValue;
        for (var i = 0; i <= k; i++)
          for (var j = 0; j < l - k; j++)
            δ = Math.Min(δ, sol[i, l - j - 1] - edges[i, j]);
        if (δ > 0)
          sol.Delta(k, -δ);
      }
      return sol;
    }

    public static Solution Feasible(Edges edges)
    {
      var sol = new int[edges.Count];
      for (var i = 0; i < sol.Length; i++)
      {
        var max = 0;
        for (var j = 0; j < sol.Length - i; j++)
          max = Math.Max(max, edges[i, j]);
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

      var k = order.Length - 1;

      var array = new int[k * (k + 1) / 2];

      foreach (var constraint in constraints)
      {
        var h = Array.IndexOf(order, constraint.Key.Head);
        var t = k - Array.IndexOf(order, constraint.Key.Tail);

        array[(h + t) * (h + t + 1) / 2 + h] = constraint.Value;
      }

      var edges = new Edges(array);
      var sol = Solve(edges);

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