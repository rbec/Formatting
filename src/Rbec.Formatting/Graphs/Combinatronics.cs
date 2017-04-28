using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public sealed class Solution
  {
    private readonly int[] Array;

    public int Length => Array.Length;

    public Solution(int[] array)
    {
      Array = array;
    }

    public int this[int a, int b] => Array[b] - (a == 0 ? 0 : Array[a - 1]);

    public void Delta(int i, int δ)
    {
      for (var j = i; j < Array.Length; j++)
        Array[j] += δ;
    }

    public override string ToString()
    {
      return "    " + string.Join(" ", Array.Select(n => $"{n,2}"));
    }

    public int Size => Array[Array.Length - 1];
  }

  public sealed class Edges
  {
    private readonly int[] Array;

    public Edges(int[] array)
    {
      Array = array;
      Count = Combinatronics.TriangleHeight(array.Length);
    }

    public readonly int Count;
    public int this[int col, int row] => Array[Combinatronics.EncodeTriangle((col, row))];

    public override string ToString()
    {
      var s = string.Empty;
      for (var j = Count - 1; j >= 0; j--)
      {
        s += $"{j} | ";
        for (var i = 0; i < Count - j; i++)
          s += $"{this[i, j],2} ";
        s += Environment.NewLine;
      }
      s += "    ";
      for (var j = 0; j < Count; j++)
        s += new string('-', 3);
      s += Environment.NewLine + "    ";
      for (var j = 0; j < Count; j++)
        s += $"{j,2} ";
      return s;
    }
  }

  public static class LP
  {
    public static int[] PrefixSum(int[] array)
    {
      var l = new int[array.Length];
      if (l.Length > 0)
        l[0] = array[0];
      for (var i = 1; i < l.Length; i++)
        l[i] = l[i - 1] + array[i];
      return l;
    }

    public static IEnumerable<Solution> All(int n, int max)
    {
      var array = new int[n];

      while (true)
      {
        yield return new Solution(PrefixSum(array));
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

    public static Solution Solve(Edges edges) => Solve(edges, Feasible(edges));

    public static Solution Solve(Edges edges, Solution sol)
    {
      var l = edges.Count;
      for (var k = 0; k < sol.Length; k++)
      {
        var δ = sol[k, k] - edges[k, l - k - 1];
        for (var i = 0; i < k; i++)
          δ = Math.Min(δ, sol[i, k] - edges[i, l - k - 1]);
        for (var j = 0; j < l - k - 1; j++)
          δ = Math.Min(δ, sol[k, l - j - 1] - edges[k, j]);
        if (δ > 0)
          sol.Delta(k, -δ);
      }
      return sol;
    }

    public static bool IsFeasible(Edges edges, Solution sol)
    {
      var l = edges.Count;
      for (var k = 0; k < sol.Length; k++)
      {
        if (sol[k, k] < edges[k, l - k - 1])
          return false;
        for (var i = 0; i < k; i++)
          if (sol[i, k] < edges[i, l - k - 1])
            return false;
        for (var j = 0; j < l - k - 1; j++)
          if (sol[k, l - j - 1] < edges[k, j])
            return false;
      }
      return true;
    }

    public static Solution Feasible(Edges edges)
    {
      var sol = new int[edges.Count];
      for (var i = 0; i < sol.Length; i++)
      {
        var max = 0;
        for (var j = 0; j < sol.Length - i; j++)
          max = Math.Max(max, edges[i, j]);
        sol[i] = i == 0 ? max : max + sol[i - 1];
      }
      return new Solution(sol);
    }
  }

  public static class Combinatronics
  {
    public static int EncodeTriangle((int i, int j) coordinate) =>
      (coordinate.i + coordinate.j) * (coordinate.i + coordinate.j + 1) / 2 + coordinate.i;

    public static int TriangleHeight(int n) => ((int) Math.Sqrt(1.0 + 8 * n) - 1) / 2;

    public static (int i, int j) DecodeTriangle(int n)
    {
      var k = ((int) Math.Sqrt(1.0 + 8 * n) - 1) / 2;
      var i = n - k * (k + 1) / 2;
      return (i, k - i);
    }

    public static int A(int n) => ((int) Math.Sqrt(1.0 + 8 * n) - 1) / 2;

    public static int C(int n)
    {
      var start = 1;
      var count = n;
      while (count > 0)
      {
        var δ = count >> 1;
        var k = start + δ;
        if (k * (k + 1) / 2 <= n)
          start += count - δ;
        count -= count - δ;
      }
      return start - 1;
    }
  }
}