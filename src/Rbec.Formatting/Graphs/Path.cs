using System;
using System.Collections;
using System.Collections.Generic;

namespace Rbec.Formatting.Graphs
{
  public sealed class Path : IEnumerable<Edge>
  {
    public readonly int Head;
    public readonly Path Tail;

    public Path(int head, Path tail = null)
    {
      Head = head;
      Tail = tail;
    }

    public IEnumerator<Edge> GetEnumerator()
    {
      if (Tail == null)
        throw new InvalidOperationException("cannot enumerate orphaned head");
      for (var node = this; node.Tail != null; node = node.Tail)
        yield return new Edge(node.Head, node.Tail.Head);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<int> Nodes()
    {
      yield return Head;
      for (var node = this; node.Tail != null; node = node.Tail)
        yield return node.Tail.Head;
    }

    public override string ToString() => string.Join(" → ", Nodes());
  }
}