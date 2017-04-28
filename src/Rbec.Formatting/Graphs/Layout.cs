using System;
using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public static class Layout
  {
    public static Dictionary<int, int> ToLayout(Dictionary<Edge, int> constraints)
    {
      var graph = constraints.Keys.GroupBy(key => key.Head).ToDictionary(g => g.Key, g => g.Select(e => e.Tail).ToArray());
      var order = graph.TopologicalOrder();

      var k = order.Length - 1;

      var array = new int[k * (k + 1) / 2];

      foreach (var constraint in constraints)
      {
        var h = Array.IndexOf(order, constraint.Key.Head);
        var t = k-Array.IndexOf(order, constraint.Key.Tail);

        array[(h + t) * (h + t + 1)/2 + h] = constraint.Value;
      }

      var edges = new Edges(array);
      var sol = LP.Solve(edges);

      var dict = new Dictionary<int, int> {{order[0], 0}};
      for (var i = 0; i < sol.Length; i++)
        dict.Add(order[i+1], sol.Array[i]);

      return dict;
    }
  }
}