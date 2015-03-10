namespace BlockSlideCLI
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    public static class DirectionUtils
    {
        public static Vector2 ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.Up;
                case Direction.Down:
                    return Vector2.Down;
                case Direction.Left:
                    return Vector2.Left;
                case Direction.Right:
                    return Vector2.Right;
                default:
                    return Vector2.Zero;
            }
        }
    }
}
