using System;

namespace BlockSlideCore.DataStructures
{
    public class Grid<T>
    {
        private readonly T[,] mBoard;
        
        public Grid(int width, int height)
        {
            mBoard = new T[height, width];
        }

        public int Width
        {
            get { return mBoard.GetLength(1); }
        }

        public int Height
        {
            get { return mBoard.GetLength(0); }
        }

        public void Set(int x, int y, T value)
        {
            mBoard[y, x] = value;
        }

        public void Set(Vector2 location, T value)
        {
            Set(location.X, location.Y, value);
        }

        public T Get(int x, int y)
        {
            var boundX = Math.Min(Math.Max(x, 0), mBoard.GetLength(1) - 1);
            var boundY = Math.Min(Math.Max(y, 0), mBoard.GetLength(0) - 1);
            return mBoard[boundY, boundX];
        }

        public T Get(Vector2 location)
        {
            return Get(location.X, location.Y);
        }

        public void ForEach(Action<int, int, T> action)
        {
            for (var row = 0; row < mBoard.GetLength(0); row++)
            {
                for (var col = 0; col < mBoard.GetLength(1); col++)
                {
                    action(col, row, mBoard[row, col]);
                }
            }
        }

    }
}
