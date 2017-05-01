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

    public int this[int col, int row] => Array[row] - (col == 0 ? 0 : Array[col - 1]);

    public void Delta(int i, int δ)
    {
      for (var j = i; j < Array.Length; j++)
        Array[j] += δ;
    }
  }
}