using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Analysis
{
    public class FurthestPointFinder
    {
        public Tuple<Vector2, Vector2> FindFurthestPointPair(Level level)
        {
            return FindFurthestPointPair(level.LevelGrid, level.MovementCalculator);
        }

        public Tuple<Vector2, Vector2> FindFurthestPointPair(Grid<TileType> grid, IMovementCalculator movementCalculator)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var graphBuilder = new GraphBuilder();
            var shortestPathFinder = new ShortestPathFinder();
            var startEndPairs = new Dictionary<Tuple<Vector2, Vector2>, int>();

            var currentBestPair = new Tuple<int, Vector2, Vector2>(0, Vector2.Zero, Vector2.Zero);
            var examined = 0;
            grid.ForEach((x, y, value) =>
            {
                var start = new Vector2(x, y);
                var graphRootNode = graphBuilder.BuildGraph(grid, start.Clone(), movementCalculator);
                var shortestPathData = shortestPathFinder.CalculateShortestPathInformation(graphRootNode, start.Clone());
                var end = shortestPathData.DistanceMap.OrderByDescending(entry => entry.Value).FirstOrDefault().Key;
                var pathDistance = shortestPathData.DistanceMap[end];
                startEndPairs[new Tuple<Vector2, Vector2>(start, end)] = pathDistance;
                examined++;
                if (pathDistance > currentBestPair.Item1)
                {
                    currentBestPair = new Tuple<int, Vector2, Vector2>(pathDistance, start, end);
                    Debug.WriteLine("Checked [{3}/{4}]; Found new best pair: Start/End/Distance found: {0} -> {1} = {2}",
                        start, end, pathDistance, examined, grid.Width * grid.Height);
                }
            });

            var bestStartEndPair = startEndPairs.OrderByDescending(entry => entry.Value).First().Key;

            stopwatch.Stop();
            Debug.WriteLine("Took {0}ms to select start and end locations.", stopwatch.ElapsedMilliseconds);
            return bestStartEndPair;
        }
    }
}
