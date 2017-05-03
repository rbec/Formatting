using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rbec.Formatting;

namespace Rbec.FormattingTest
{
  [TestClass]
  public class TestCells
  {
    [TestMethod] public void TestAlignMap()
    {
      CollectionAssert.AreEqual(
        new[] {5, 2, 6, 7, 0, 3, 8, 9},
        Tables.AlignMap(new[] {(2, 1), (0, 4), (3, 5)}, 5, 7).Item1);
    }
  }
}