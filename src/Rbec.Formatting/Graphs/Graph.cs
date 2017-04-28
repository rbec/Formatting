using System.Collections.Generic;
using System.Linq;

namespace Rbec.Formatting.Graphs
{
  public static class Graph
  {
    public static int[] TopologicalOrder(this IReadOnlyDictionary<int, int[]> g)
    {
      var order = new List<int>();
      var unmarked = g.SelectMany(entry => entry.Value.Concat(new[] {entry.Key})).Distinct().ToList();

      while (unmarked.Count > 0)
        Visit(unmarked[0]);

      return (order as IEnumerable<int>).Reverse().ToArray();

      void Visit(int n)
      {
        if (unmarked.Contains(n))
        {
          if (g.ContainsKey(n))
            foreach (var m in g[n])
            {
              Visit(m);
            }
          unmarked.Remove(n);
          order.Add(n);
        }
      }
    }
  }
}