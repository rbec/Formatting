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
      get => Array[Solver.EncodeTriangle((col, Length - row - 1))];
      set => Array[Solver.EncodeTriangle((col, Length - row - 1))] = value;
    }
  }
}