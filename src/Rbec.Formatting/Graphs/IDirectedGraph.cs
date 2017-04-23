using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public interface IDirectedGraph : IEnumerable<Edge>
  {
    IEnumerable<int> this[int head] { get; }
    IEnumerable<int> Nodes { get; }
  }

  public sealed class DirectedGraph : IDirectedGraph
  {
    public ILookup<int, int> Lookup;
    public IEnumerator<Edge> GetEnumerator() => Lookup.SelectMany(group => group.Select(end => new Edge(group.Key, end))).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerable<int> this[int head] => Lookup.Contains(head) ? Lookup[head] : Enumerable.Empty<int>();
    public IEnumerable<int> Nodes => this.SelectMany(edge => new[] {edge.Head, edge.Tail}).Distinct().OrderBy(key => key);
  }
}