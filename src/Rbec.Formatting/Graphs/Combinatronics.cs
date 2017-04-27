using System;

namespace Rbec.Formatting.Graphs
{
  public static class Combinatronics
  {
    public static (int, int) TriangleInvert(int n)
    {
      //var m = ((int)Math.Sqrt(1.0 + 8 * n) - 1) / 2;
      var m = (SqrtInt(1 + 8 * n) - 1) / 2;
      var mm = n - m * (m + 1) / 2;
      return (mm, m - mm);
    }

    public static int SqrtInt(int n)
    {
      double last;
      double x = n;
      do
      {
        last = x;
        x = (x + n / x) / 2;
      } while (Math.Abs(x - last) >= 1);
      return (int)x;
    }

    //public static (int, int) TriangleInvert(int n)
    //{
    //  var m = SqrtInt2(n);
    //  var mm = n - m * (m + 1) / 2;
    //  return (mm, m - mm);
    //}

    //public static int SqrtInt2(int n)
    //{
    //  double last;
    //  double x = n/2.0;
    //  do
    //  {
    //    last = x;
    //    x = (x*x-2*n)/(2*x+1);
    //  } while (Math.Abs(x - last) >= 1);
    //  return (int)x;
    //}
  }
}