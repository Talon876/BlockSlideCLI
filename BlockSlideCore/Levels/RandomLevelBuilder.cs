using System;
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
            : this(boardWidth, boardHeight, new MovementCalculator())
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
            var furthestPointFinder = new FurthestPointFinder();
            var bestStartEndPair = furthestPointFinder.FindFurthestPointPair(grid, mMovementCalculator);

            grid.Set(bestStartEndPair.Item1, TileType.Start);
            grid.Set(bestStartEndPair.Item2, TileType.Finish);
            return grid;
        }
    }
}
