using System;
using Rbec.Formatting.Graphs;
using Rbec.FormattingTest.Graphs;

namespace Sandbox
{
  class Program
  {
    static void Main(string[] args)
    {
      //var n = 4;
      for (var n = 0; n <= 7; n++)
      {
        var count = 1ul << (n * (n - 1) / 2);

        var validCount = 0;
        for (uint i = 0; i < count; i++)
        {
          if (Generate.IsValid(i, n))
            validCount++;
          //Console.WriteLine($"{Generate.IsValid(i, n)}{Environment.NewLine}--------------------{Environment.NewLine}{TestGraph.Format(Generate.Decode(i, n).ToDirectedGraph())}");
          //Console.WriteLine();
        }
        Console.WriteLine($"{n}: {validCount}");
      }

      Console.ReadLine();

    }
  }
}