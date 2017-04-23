using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public static class DirectedGraphs
  {
    public static DirectedGraph ToDirectedGraph(this IEnumerable<Edge> edges) => new DirectedGraph {Lookup = edges.ToLookup(arrow => arrow.Head, arrow => arrow.Tail)};

    private struct Result<T>
    {
      public bool IsUnreachable;
      public T Value;

      public static readonly Result<T> Unreachabe = new Result<T> {IsUnreachable = true};
      public static implicit operator Result<T>(T value) => new Result<T> {Value = value};
    }

    private sealed class Cache<T>
    {
      private readonly int Basis;
      private readonly Dictionary<Edge, Result<T>> Parent;
      private readonly Func<int, IEnumerable<T>, T> Func;

      public Cache(Dictionary<Edge, Result<T>> parent, int basis, Func<int, IEnumerable<T>, T> func)
      {
        Parent = parent;
        Basis = basis;
        Func = func;
      }

      public bool TryGetValue(int head, out Result<T> value) => Parent.TryGetValue(new Edge(head, Basis), out value);

      public void Add(int head, IEnumerable<T> items, out Result<T> result)
      {
        Parent.Add(new Edge(head, Basis), result = items.Any() ? Func(head, items) : Result<T>.Unreachabe);
      }
    }

    private static Result<T> DepthFirst<T>(this IDirectedGraph directedGraph, int head, Cache<T> cache)
    {
      Result<T> value;
      if (!cache.TryGetValue(head, out value))
      {
        var reachable = directedGraph[head]
          .Select(tail => directedGraph.DepthFirst(tail, cache))
          .Where(re => !re.IsUnreachable)
          .Select(re => re.Value);
        cache.Add(head, reachable, out value);
      }
      return value;
    }

    private static Dictionary<Edge, T> DepthFirst<T>(this IDirectedGraph directedGraph, IEnumerable<Edge> arrows, Func<int, IEnumerable<T>, T> ƒ, Func<int, T> seed)
    {
      var cache = new Dictionary<Edge, Result<T>>();
      foreach (var arrow in arrows)
      {
        if (!cache.ContainsKey(new Edge(arrow.Tail, arrow.Tail)))
          cache[new Edge(arrow.Tail, arrow.Tail)] = seed(arrow.Tail);
        directedGraph.DepthFirst(arrow.Head, new Cache<T>(cache, arrow.Tail, ƒ));
      }
      return cache.Where(entry => !entry.Value.IsUnreachable).ToDictionary(entry => entry.Key, entry => entry.Value.Value);
    }

    public static Dictionary<Edge, IEnumerable<Path>> Paths(this IDirectedGraph directedGraph, IEnumerable<Edge> arrows) =>
      directedGraph.DepthFirst(arrows, (head, paths) => paths.SelectMany(ps => ps.Select(p => new Path(head, p))), tail => (IEnumerable<Path>) new[] {new Path(tail)});

    public static IDirectedGraph TransativeReduction(this IDirectedGraph directedGraph) =>
      directedGraph.DepthFirst(directedGraph, (head, lengths) => lengths.Max() + 1, tail => 0).Where(result => result.Value == 1).Select(entry => entry.Key).ToDirectedGraph();
  }
}