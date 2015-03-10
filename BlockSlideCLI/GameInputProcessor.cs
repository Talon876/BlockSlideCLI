using System.Collections.Generic;

namespace BlockSlideCLI
{
    public class GameInputProcessor
    {
        private readonly Dictionary<char, Direction> mMovementMap;

        public GameInputProcessor()
        {
            mMovementMap = new Dictionary<char, Direction>
            {
                {'w', Direction.Up},
                {'s', Direction.Down},
                {'a', Direction.Left},
                {'d', Direction.Right},
            };
        }

        public Direction? GetDirectionFromInput(char input)
        {
            Direction? returnDirection = null;
            Direction direction;
            if (mMovementMap.TryGetValue(input, out direction))
            {
                returnDirection = direction;
            }
            return returnDirection;
        }
    }
}
