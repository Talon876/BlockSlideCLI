using System;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

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
            var end = Vector2.RandomVector(grid.Width, grid.Height);
            grid.Set(start, TileType.Start);
            grid.Set(end, TileType.Finish);
            return grid;
        }
    }
}
