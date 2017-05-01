using System;

namespace Rbec.Formatting
{
  public sealed class Text
  {
    public readonly int Left;
    public readonly int Top;
    public int Right;
    public int Bottom;
    public readonly string[] Lines;
    public readonly byte RowAlign;
    public readonly byte ColAlign;

    public Text(int left, int top, int right, int bottom, string[] lines, byte rowAlign, byte colAlign)
    {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
      Lines = lines;
      RowAlign = rowAlign;
      ColAlign = colAlign;
    }

    public Text ReplaceRight(int old, int @new)
    {
      if (Right == old)
        Right = @new;
      return this;
    }

    public Text ReplaceBottom(int old, int @new)
    {
      if (Bottom == old)
        Bottom = @new;
      return this;
    }

    public override string ToString() => $"{Left} → {Right} {string.Join(Environment.NewLine, Lines)}";
  }
}