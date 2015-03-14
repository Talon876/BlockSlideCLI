using System;
using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;
using BlockSlideCore.Utilities;

namespace BlockSlideCore.Levels
{
    public class RandomLevelBuilder : ILevelBuilder
    {
        private readonly Random mRandom;
        private readonly int mWidth;
        private readonly int mHeight;

        public RandomLevelBuilder(int boardWidth, int boardHeight)
        {
            mWidth = boardWidth;
            mHeight = boardHeight;
            mRandom = new Random();
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
            var validLocationCalculator = new ValidLocationCalculator();
            var validLocations = validLocationCalculator.BuildValidLocations(grid, start.Clone(), new MovementCalculator());
            var end = validLocations
                .Shuffle(new Random())
                .FirstOrDefault(location => !location.Equals(start));

            grid.Set(start, TileType.Start);
            grid.Set(end, TileType.Finish);
            return grid;
        }
    }
}
