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
        Assert.AreEqual(TriangleInvert(i), Combinatronics.DecodeTriangle(i));
    }

    [TestMethod]
    public void TestTriangleInvertsTriangle()
    {
      for (var i = 0; i < 100000; i++)
        Assert.AreEqual(i, Combinatronics.EncodeTriangle(Combinatronics.DecodeTriangle(i)));
    }

    [TestMethod]
    public void TestAB()
    {
        Assert.AreEqual(Combinatronics.A(0), Combinatronics.C(0));
        Assert.AreEqual(Combinatronics.A(1), Combinatronics.C(1));
        Assert.AreEqual(Combinatronics.A(2), Combinatronics.C(2));
        Assert.AreEqual(Combinatronics.A(3), Combinatronics.C(3));
        Assert.AreEqual(Combinatronics.A(4), Combinatronics.C(4));
        Assert.AreEqual(Combinatronics.A(5), Combinatronics.C(5));
        Assert.AreEqual(Combinatronics.A(6), Combinatronics.C(6));
        Assert.AreEqual(Combinatronics.A(7), Combinatronics.C(7));
        Assert.AreEqual(Combinatronics.A(8), Combinatronics.C(8));
        Assert.AreEqual(Combinatronics.A(9), Combinatronics.C(9));
        Assert.AreEqual(Combinatronics.A(10), Combinatronics.C(10));
    }
  }
}