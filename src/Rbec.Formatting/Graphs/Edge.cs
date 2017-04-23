namespace Rbec.Formatting.Graphs
{
  public struct Edge
  {
    public Edge(int head, int tail)
    {
      Head = head;
      Tail = tail;
    }

    public int Head;
    public int Tail;
 //   public override string ToString() => $"{Head} → {Tail}";
    public override string ToString() => $"{Head} -> {Tail}";
  }
}