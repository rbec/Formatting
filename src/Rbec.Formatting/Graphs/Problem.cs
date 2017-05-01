using System;

namespace Rbec.Formatting.Graphs
{
  public sealed class Problem
  {
    private readonly int[] Array;

    public Problem(int length)
    {
      Array = new int[length * (length + 1) / 2];
      Length = length;
    }

    public Problem(int[] array)
    {
      Array = array;
      Length = Solver.TriangleHeight(array.Length);
    }

    public readonly int Length;

    public int this[int col, int row]
    {
      get => Array[Solver.EncodeTriangle((col, row))];
      set => Array[Solver.EncodeTriangle((col, row))] = value;
    }

    public override string ToString()
    {
      var s = string.Empty;
      for (var j = Length - 1; j >= 0; j--)
      {
        s += $"{j} | ";
        for (var i = 0; i < Length - j; i++)
          s += $"{this[i, j],2} ";
        s += Environment.NewLine;
      }
      s += "    ";
      for (var j = 0; j < Length; j++)
        s += new string('-', 3);
      s += Environment.NewLine + "    ";
      for (var j = 0; j < Length; j++)
        s += $"{j,2} ";
      return s;
    }
  }
}