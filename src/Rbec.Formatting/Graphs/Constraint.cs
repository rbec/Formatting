namespace Rbec.Formatting.Graphs
{
  public struct Constraint
  {
    public Constraint(int head, int tail, int minimum)
    {
      Head = head;
      Tail = tail;
      Minimum = minimum;
    }

    public int Head;
    public int Tail;
    public int Minimum;

    //   public override string ToString() => $"{Head} → {Tail}";
    public override string ToString() => $"{Head} -> {Tail}";
  }
}