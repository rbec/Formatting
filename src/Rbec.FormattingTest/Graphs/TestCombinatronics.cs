using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting.Graphs;

namespace Rbec.FormattingTest.Graphs
{
  [TestClass]
  public class TestCombinatronics
  {
    public static (int, int) TriangleInvert(int n)
    {
      var m = 0;
      while (n > m)
        n -= ++m;
      return (n, m - n);
    }

    [TestMethod] public void TestTriangleInvert()
    {
      for (var i = 0; i < 100000; i++)
        Assert.AreEqual(TriangleInvert(i), Combinatronics.TriangleInvert(i));
    }

    [TestMethod]
    public void TestTriangleInvertsTriangle()
    {
      for (var i = 0; i < 100000; i++)
        Assert.AreEqual(i, Combinatronics.Triangle(Combinatronics.TriangleInvert(i)));
    }
  }
}