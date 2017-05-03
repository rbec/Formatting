using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting
{
  public sealed class Cell<T>
  {
    public readonly T Content;
    public readonly int Next;

    public Cell(T content, int next)
    {
      Content = content;
      Next = next;
    }
  }

  public static class Tables
  {
    public static (int[], int) AlignMap((int, int)[] aligns, int a, int b)
    {
      var map = new int[b + 1];
      var i = 0;
      var j = 0;
      var k = a + b - aligns.Length;

      while (j < aligns.Length)
      {
        if (aligns[j].Item1 == a)
          k = aligns[j].Item2;
        if (aligns[j].Item2 == i)
          map[i] = aligns[j++].Item1;
        else
          map[i] = a + i - j;
        i++;
      }
      while (i < map.Length)
        map[i] = a + i++ - j;
      return (map, k);
    }

    public static List<Cell<T>>[] Align<T>(this List<Cell<T>>[] a, List<Cell<T>>[] b, (int, int)[] aligns)
    {
      var (map, k) = AlignMap(aligns, a.Length, b.Length);
      var c = new List<Cell<T>>[a.Length + b.Length - aligns.Length];
      for (var i = 0; i < a.Length; i++)
        c[i] = a[i].Select(cell => cell.Next == a.Length ? new Cell<T>(cell.Content, k) : cell).ToList();
      for (var j = a.Length; j < c.Length; j++)
        c[j] = new List<Cell<T>>();

      for (var i = 0; i < b.Length; i++)
        c[map[i]].AddRange(b[i].Select(cell => new Cell<T>(cell.Content, map[cell.Next])));
      return c;
    }
  }
}