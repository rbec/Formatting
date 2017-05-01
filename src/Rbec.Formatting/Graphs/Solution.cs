using System.Linq;

namespace Rbec.Formatting.Graphs {
  public sealed class Solution
  {
    public readonly int[] Array;

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

    public override string ToString() => "    " + string.Join(" ", Enumerable.Select<int, string>(Array, n => $"{n,2}"));
  }
}