using System;

namespace BlockSlideCLI
{
    public class Vector2
    {
        private int mX;
        private int mY;

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 Up = new Vector2(0, -1);
        public static readonly Vector2 Down = new Vector2(0, 1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);

        private static readonly Random mRandom = new Random();

        public Vector2(int x, int y)
            :this(x,y,x,y)
        {
        }

        private Vector2(int x, int y, int previousX, int previousY)
        {
            mX = x;
            mY = y;
            PreviousX = previousX;
            PreviousY = previousY;
        }

        public int PreviousX { get; private set; }

        public int PreviousY { get; private set; }

        public int X
        {
            get { return mX; }
            set
            {
                PreviousX = mX;
                mX = value;
            }
        }

        public int Y
        {
            get { return mY; }
            set
            {
                PreviousY = mY;
                mY = value;
            }
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, int scalar)
        {
            return new Vector2(a.X*scalar, a.Y*scalar);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var vectorObj = (Vector2) obj;
            return X == vectorObj.X && Y == vectorObj.Y;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }

        public Vector2 Clone()
        {
            return new Vector2(X, Y, PreviousX, PreviousY);
        }

        public static Vector2 RandomVector(int maxX, int maxY)
        {
            return new Vector2(mRandom.Next(0, maxX), mRandom.Next(0, maxY));
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }
    }
}
