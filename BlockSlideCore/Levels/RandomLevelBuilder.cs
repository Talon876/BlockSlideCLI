using System;
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
            var start = Vector2.RandomVector(grid.Width, grid.Height);

            var graphBuilder = new GraphBuilder();
            var graphRootNode = graphBuilder.BuildGraph(grid, start.Clone(), mMovementCalculator);
            var shortestPathFinder = new ShortestPathFinder();
            var shortestPathData = shortestPathFinder.CalculateShortestPathInformation(graphRootNode, start.Clone());
            var end = shortestPathData.DistanceMap.OrderByDescending(entry => entry.Value).FirstOrDefault().Key;

            grid.Set(start, TileType.Start);
            grid.Set(end, TileType.Finish);
            return grid;
        }
    }
}
