using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  public static class Generate
  {
    public static IEnumerable<Edge> Decode(ulong index, int n)
    {
      //var k = 0;
      //for (var i = 0; i < n - 1; i++)
      //  for (var j = i + 1; j < n; j++)
      //    if ((1ul & (index >> k++)) != 0)
      //      yield return new Edge(i, j);

      for (var i = 0; i < n - 1; i++)
        for (var j = i + 1; j < n; j++)
        {
          if ((1ul & index) != 0)
            yield return new Edge(i, j);
          index >>= 1;
        }
    }

    public static IEnumerable<IDirectedGraph> Cases(int n)
    {
      var count = 1 << (n * (n - 1) / 2);
      for (uint i = 0; i < count; i++)
        yield return Decode(i, n).ToDirectedGraph();
    }

    public static bool IsValid(ulong index, int n)
    {
      var cols = 0ul;
      var rows = 0ul;
      for (var i = 0; i < n - 1; i++)
        for (var j = i; j < n - 1; j++)
        {
          if ((1ul & index) != 0)
          {
            cols |= 1ul << i;
            rows |= 1ul << j;
          }
          index >>= 1;
        }
      return cols == rows && cols == (1ul << (n - 1)) - 1;
    }
  }

  [TestClass]
  public class TestGraph
  {

    [TestMethod]
    public void TestGenerate()
    {
      Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
      Trace.WriteLine("fish");
      Trace.Flush();

      //foreach (var g in Generate.Cases(3))
      //{
      //  Trace.WriteLine(Format(g));
      //  Console.WriteLine(Format(g));
      //}
      Assert.IsTrue(true);
    }

    public sealed class ExcludedEnumerable<T> : IEnumerable<T>
    {
      private readonly IEnumerable<T> Enumerable;
      private readonly int Index;
      public T Excluded => Enumerable.ElementAt(Index);

      public ExcludedEnumerable(IEnumerable<T> enumerable, int index)
      {
        Enumerable = enumerable;
        Index = index;
      }

      public IEnumerator<T> GetEnumerator()
      {
        var i = 0;
        foreach (var item in Enumerable)
          if (i++ != Index)
            yield return item;
      }

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static IEnumerable<ExcludedEnumerable<T>> SingleExclusions<T>(IEnumerable<T> enumerable) =>
      Enumerable.Range(0, enumerable.Count()).Select(i => new ExcludedEnumerable<T>(enumerable, i));

    public static bool IsTransitiveReductionOf(IDirectedGraph reduction, IDirectedGraph original) =>
      !reduction.Except(original).Any() &&
      original.Except(reduction).All(r => IsReachable(reduction, r)) &&
      !SingleExclusions(reduction).Any(arrows => IsReachable(arrows.ToDirectedGraph(), arrows.Excluded));

    [TestMethod] public void TR()
    {
      var n = 6;
      var cases = Cases(n).ToArray();
      foreach (var graph in cases)
      {
        var tr = graph.TransativeReduction();
        if (!IsTransitiveReductionOf(tr, graph))
          Assert.Fail(Format(graph));
      }
    }

    public const int N = 6;

    public static IEnumerable<Edge> Decode(BitArray index, int nodes)
    {
      var k = 0;
      for (var i = 0; i < nodes - 1; i++)
        for (var j = i + 1; j < nodes; j++)
          if (index[k++])
            yield return new Edge(i, j);
    }

    public static IEnumerable<Edge> Decode(ulong index, int nodes)
    {
      var k = 0;
      for (var i = 0; i < nodes - 1; i++)
        for (var j = i + 1; j < nodes; j++)
          if ((1ul & (index >> k++)) != 0)
            yield return new Edge(i, j);
    }

    public static IEnumerable<IDirectedGraph> Cases(int nodes)
    {
      var count = 1 << (nodes * (nodes - 1) / 2);
      for (uint i = 0; i < count; i++)
        yield return Decode(i, nodes).ToDirectedGraph();
    }

    public static string Format(IDirectedGraph g) => $"{string.Join(Environment.NewLine, g.Select(e => e.ToString()))}";

    public static IEnumerable<Edge> AllEdges(int n)
    {
      for (var i = 0; i < n - 1; i++)
        for (var j = i + 1; j < n; j++)
          yield return new Edge(i, j);
    }

    public static bool IsReachable(IDirectedGraph g, Edge edge)
    {
      var stack = new Stack<int>();
      var visited = new HashSet<int>();

      stack.Push(edge.Head);

      while (stack.Count > 0)
      {
        edge.Head = stack.Pop();

        if (visited.Contains(edge.Head))
          continue;
        visited.Add(edge.Head);
        foreach (var t in g[edge.Head])
        {
          if (t == edge.Tail)
            return true;
          stack.Push(t);
        }
      }
      return false;
    }

    public static IEnumerable<Path> PathsNew(IDirectedGraph g, Edge edge)
    {
      return g[edge.Head].SelectMany(t => (t == edge.Tail ? new[] {new Path(edge.Tail)} : PathsNew(g, new Edge(t, edge.Tail))).Select(p => new Path(edge.Head, p)));
    }

    [TestMethod] public void PathsWorks()
    {
      foreach (var g in Cases(N))
      {
        var allEdges = AllEdges(N).ToArray();

        var paths = g.Paths(allEdges);
        foreach (var arrow in allEdges)
        {
          if (IsReachable(g, arrow))
          {
            var r1 = PathsNew(g, arrow).ToArray();
            if (!paths.ContainsKey(arrow))
              Assert.Fail($"{arrow}{Environment.NewLine}{Environment.NewLine}{Format(g)}");
            var r2 = paths[arrow].ToArray();
            if (r1.Length != r2.Length)
              Assert.Fail(Format(g));
            for (var i = 0; i < r1.Length; i++)
              if (!r1[i].SequenceEqual(r2[i]))
                Assert.Fail(Format(g));
          }
          else
          {
            if (PathsNew(g, arrow).Any())
              Assert.Fail("fail");
            if (paths.ContainsKey(arrow))
              Assert.Fail("fail");
          }
        }
      }
    }

    //public static int Pages(int x, int y) => (x + y - 1) / y;

    //public static TimeSpan Duration<T>(ILookup<int, int> g, Edge[] edges, Func<ILookup<int, int>, int, int, T> ƒ, out List<T> result)
    //{

    //  var watch = new Stopwatch();
    //  result = new List<T>();
    //  watch.Start();
    //  foreach (var edge in edges)
    //  {
    //    result.Add(ƒ(g, edge.Head, edge.Tail));
    //  }
    //  watch.Stop();

    //  return watch.Elapsed;
    //}

    //[TestMethod] public void IsReachableBig()
    //{
    //  var a = new List<int?>();
    //  var b = a.Max();
    //  var c = b + 1;

    //  for (var n = 1; n < 30; n += 1)
    //  {
    //    var random = new Random(41423);

    //    var d1 = TimeSpan.Zero;
    //    var d2 = TimeSpan.Zero;
    //    var num = n * (n - 1) / 2;
    //    var array = new byte[Pages(num, 8)];
    //    var edges = AllEdges(n).ToArray();

    //    for (var i = 0; i < 5; i++)
    //    {
    //      random.NextBytes(array);
    //      var index = new BitArray(array);

    //      var g = Decode(index, n).ToDirectedGraph();


    //      d1 += Duration(g, edges, (g2, head, tail) => g2.Paths(head, tail).ToArray());
    //      d2 += Duration(g, edges, (g2, head, tail) => g2.PathsFast(head, tail).ToArray());

    //      List<int?> r1;
    //      List<int?> r2;
    //      d1 += Duration(g, edges, Layout.LongestPath2, out r1);
    //      d2 += Duration(g, edges, Layout.LongestPath3, out r2);

    //      //if (!r1.Select(j => j == -1 ? null : (int?)j).SequenceEqual(r2))
    //      if (!r1.SequenceEqual(r2))
    //        Assert.Fail("fail");
    //    }

    //    Console.WriteLine(@"{0,4}    {1:mm\:ss\:ffff}    {2:mm\:ss\:ffff} {3,7:0.000%}", n, d1, d2, d2.TotalDays / d1.TotalDays);
    //  }
    //}
  }
}