using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public sealed class Solution
  {
    public readonly int[] Array;

    public int Length => Array.Length;

    public Solution(int[] array)
    {
      Array = array;
    }

    public int this[int col, int row] => Array[Length - row - 1] - (col == 0 ? 0 : Array[col - 1]);

    public void Delta(int i, int δ)
    {
      for (var j = i; j < Array.Length; j++)
        Array[j] += δ;
    }

    public override string ToString() => "    " + string.Join(" ", Array.Select(n => $"{n,2}"));
  }
}