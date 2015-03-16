using System.Collections.Generic;
using System.Linq;
using BlockSlideCore.DataStructures;

namespace BlockSlideCore.Analysis
{
    public class ShortestPathFinder
    {
        private const int EDGE_DISTANCE = 1;

        public ShortestPathData CalculateShortestPathInformation(Node<Vector2> graphRootNode, Vector2 start)
        {
            var distance = new Dictionary<Vector2, int>();
            var previous = new Dictionary<Vector2, Vector2>();
            var neighbors = new Dictionary<Vector2, List<Vector2>>();
            var unvisited = new List<Vector2>();

            distance[start] = 0;
            previous[start] = null;

            graphRootNode.BreadthFirstSearch(node =>
            {
                if (!node.Value.Equals(start))
                {
                    distance[node.Value] = int.MaxValue;
                    previous[node.Value] = null;
                }
                unvisited.Add(node.Value);
                neighbors[node.Value] = node.Neighbors.Select(neighbor => neighbor.Value).ToList();
            });

            while (unvisited.Any())
            {
                var current = distance.Where(
                    entry => unvisited.Contains(entry.Key))
                    .OrderBy(entry => entry.Value)
                    .FirstOrDefault().Key;
                unvisited.Remove(current);

                neighbors[current].ForEach(neighbor =>
                {
                    var alt = distance[current] + EDGE_DISTANCE;
                    if (alt < distance[neighbor])
                    {
                        distance[neighbor] = alt;
                        previous[neighbor] = current;
                    }
                });
            }

            return new ShortestPathData(distance, previous);
        }
    }
}
