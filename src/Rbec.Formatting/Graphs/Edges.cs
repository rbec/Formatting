using System;

namespace Rbec.Formatting.Graphs
{
  public sealed class Edges
  {
    private readonly int[] Array;

    public Edges(int[] array)
    {
      Array = array;
      Count = Solver.TriangleHeight(array.Length);
    }

    public readonly int Count;
    public int this[int col, int row] => Array[Solver.EncodeTriangle((col, row))];

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
}