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
        var min = int.MaxValue;
        for (var i = 0; i <= k; i++)
          for (var j = k; j < solution.Length; j++)
            min = Math.Min(min, solution[i, j] - problem[i, j]);
        if (min > 0)
          solution.Delta(k, -min);
      }
      return solution;
    }

    public static Solution Feasible(Problem problem)
    {
      var solution = new int[problem.Length];
      for (var i = 0; i < solution.Length; i++)
      {
        var max = 0;
        for (var j = i; j < solution.Length; j++)
          max = Math.Max(max, problem[i, j]);
        solution[i] = max;
      }
      return new Solution(PrefixSum(solution));
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

    public static Dictionary<int, int> ToLayout(this IEnumerable<Constraint> constraints)
    {
      var graph = constraints.GroupBy(key => key.Head).ToDictionary(g => g.Key, g => g.Select(e => e.Tail).ToArray());
      var order = graph.TopologicalOrder();

      var problem = new Problem(order.Length - 1);

      foreach (var constraint in constraints)
        problem[Array.IndexOf(order, constraint.Head), Array.IndexOf(order, constraint.Tail) + 1] = constraint.Minimum;

      var solution = Solve(problem);

      var dict = new Dictionary<int, int> {{order[0], 0}};
      for (var i = 0; i < solution.Length; i++)
        dict.Add(order[i + 1], solution.Array[i]);

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
        if (!unmarked.Contains(n)) return;
        if (g.ContainsKey(n))
          foreach (var m in g[n])
            Visit(m);
        unmarked.Remove(n);
        order.Add(n);
      }
    }
  }
}