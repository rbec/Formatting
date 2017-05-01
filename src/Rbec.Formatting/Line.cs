namespace Rbec.Formatting
{
  public sealed class Line
  {
    public readonly bool Visible;
    public readonly int Start;
    public int End;
    public int Axis;

    public Line(bool visible, int start, int end, int axis)
    {
      Visible = visible;
      Start = start;
      End = end;
      Axis = axis;
    }

    public Line ReplaceEnd(int old, int @new)
    {
      if (End == old)
        End = @new;
      return this;
    }

    public Line ReplaceAxis(int old, int @new)
    {
      if (Axis == old)
        Axis = @new;
      return this;
    }
  }
}