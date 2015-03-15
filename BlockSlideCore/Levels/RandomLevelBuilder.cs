using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlockSlideCore.Analysis;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Levels
{
    public class RandomLevelBuilder : ILevelBuilder
    {
        private readonly Random mRandom;
        private readonly int mWidth;
        private readonly int mHeight;
        private readonly IMovementCalculator mMovementCalculator;

        public RandomLevelBuilder(int boardWidth, int boardHeight, IMovementCalculator movementCalculator)
        {
            mWidth = boardWidth;
            mHeight = boardHeight;
            mMovementCalculator = movementCalculator;
            mRandom = new Random();
        }

        public RandomLevelBuilder(int boardWidth, int boardHeight)
            :this(boardWidth, boardHeight, new MovementCalculator())
        {
        }

        public Grid<TileType> CreateLevel(int level)
        {
            var grid = new Grid<TileType>(mWidth, mHeight);
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var value = mRandom.Next(0, 100);
                    grid.Set(x, y, value < 80 ? TileType.Floor : TileType.Wall);
                }
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var graphBuilder = new GraphBuilder();
            var shortestPathFinder = new ShortestPathFinder();
            var startEndPairs = new Dictionary<Tuple<Vector2, Vector2>, int>();

            grid.ForEach((x, y, value) =>
            {
                var start = new Vector2(x, y);
                var graphRootNode = graphBuilder.BuildGraph(grid, start.Clone(), mMovementCalculator);
                var shortestPathData = shortestPathFinder.CalculateShortestPathInformation(graphRootNode, start.Clone());
                var end = shortestPathData.DistanceMap.OrderByDescending(entry => entry.Value).FirstOrDefault().Key;
                startEndPairs[new Tuple<Vector2, Vector2>(start, end)] = shortestPathData.DistanceMap[end];
            });

            var bestStartEndPair = startEndPairs.OrderByDescending(entry => entry.Value).First().Key;

            stopwatch.Stop();
            Debug.WriteLine("Took {0}ms to select start and end locations.", stopwatch.ElapsedMilliseconds);
            grid.Set(bestStartEndPair.Item1, TileType.Start);
            grid.Set(bestStartEndPair.Item2, TileType.Finish);
            return grid;
        }
    }
}
