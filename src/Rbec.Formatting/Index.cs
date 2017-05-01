using System.Collections.Generic;

namespace Rbec.Formatting
{
  public abstract class Index
  {
    public static Index Origin() => new Root();
    protected int Id;
    protected abstract Index Create(Index parent, int series);
    public static implicit operator int(Index index) => index.Id;
    public Index Next(int series) => Create(this, series);
    protected abstract string ToString(int count);
    public override string ToString() => ToString(0);

    private sealed class Root : Index
    {
      private readonly Dictionary<(Index, int), Node> Nodes = new Dictionary<(Index, int), Node>();
      private int Count = 1;

      protected override Index Create(Index parent, int series)
      {
        if (!Nodes.TryGetValue((parent, series), out Node node))
          Nodes.Add((parent, series), node = new Node {Parent = parent, Series = series, Id = series == 0 ? Count++ : parent.Id});
        return node;
      }

      protected override string ToString(int count) => $"{count}";
    }

    private sealed class Node : Index
    {
      public Index Parent;
      public int Series;
      protected override Index Create(Index parent, int series) => Parent.Create(parent, series);

      protected override string ToString(int count) =>
        Series == 0
          ? Parent.ToString(count + 1)
          : $"{Parent.ToString(0)}{(char) ('₀' + Series)}{count}";
    }
  }

  public static class Indices
  {
    public static Index Offset(this Index index, int offset)
    {
      while (offset-- > 0)
        index = index.Next(0);
      return index;
    }

    public static Index Nest(this Index index, int series) => index.Next(series + 1);
  }
}