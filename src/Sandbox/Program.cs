﻿using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes.Columns;
using Rbec.Formatting.Graphs;

namespace Sandbox
{
  [MinColumn, MaxColumn]
  public class Benchmarks
  {
    //[Benchmark] public void TriangleInvert1()
    //{
    //  for (var i = 0; i < 100000; i++)
    //    Combinatronics.DecodeTriangle(i);
    //}

    //[Benchmark] public void TriangleInvert2()
    //{
    //  for (var i = 0; i < 100000; i++)
    //    Combinatronics.TriangleInvert2(i);
    //}
  }

  class Program
  {
    static void Main(string[] args)
    {
      var g = new[]
              {
                new Constraint(9, 8, 2),
                new Constraint(8, 7, 3),
                new Constraint(7, 6, 1),
                new Constraint(8, 6, 5),
                new Constraint(9, 7, 5),
              };
      var order = g.ToLayout();
      var s = string.Join(" ", order);
      Console.WriteLine(s);

      //var edges = new[]
      //            {
      //              0,
      //              0, 0,
      //              0, 0, 7,
      //              0, 6, 0, 0,
      //              3, 3, 2, 1, 1
      //            };
      //var e = new Edges(edges);

      //Console.WriteLine(e);
      //Console.WriteLine();
      //Console.WriteLine(LP.Feasible(e));

      //var sol = LP.Solve(e, LP.Feasible(e));
      //Console.WriteLine(sol);

      // Console.WriteLine("     " + string.Join(" ",sol));


      //for (var i = 0; i < 20; i++)
      //{
      //  //Console.WriteLine($"{i,5} {Combinatronics.A(i)} : {Combinatronics.C(i)}");
      //  Console.WriteLine($"{i,5} {Combinatronics.DecodeTriangle(i)}");
      //}
      //var summary = BenchmarkRunner.Run<Benchmarks>();
      Console.ReadLine();
      ////var n = 4;
      //for (var n = 0; n <= 7; n++)
      //{
      //  var count = 1ul << (n * (n - 1) / 2);

      //  var validCount = 0;
      //  for (uint i = 0; i < count; i++)
      //  {
      //    if (Generate.IsValid(i, n))
      //      validCount++;
      //    //Console.WriteLine($"{Generate.IsValid(i, n)}{Environment.NewLine}--------------------{Environment.NewLine}{TestGraph.Format(Generate.Decode(i, n).ToDirectedGraph())}");
      //    //Console.WriteLine();
      //  }
      //  Console.WriteLine($"{n}: {validCount}");
      //}

      //Console.ReadLine();

    }
  }
}