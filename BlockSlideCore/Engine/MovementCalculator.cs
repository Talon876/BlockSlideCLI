using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Engine
{
    public class MovementCalculator : IMovementCalculator
    {
        private static bool IsBlocking(Grid<TileType> levelGrid, Vector2 location)
        {
            var blocking = true;

            if (location.X >= 0 && location.X < levelGrid.Width &&
                location.Y >= 0 && location.Y < levelGrid.Height)
            {
                if (levelGrid.Get(location) != TileType.Wall)
                {
                    blocking = false;
                }
            }
            return blocking;
        }

        public Vector2 CalculateNewLocation(Grid<TileType> levelGrid, Vector2 currentLocation, Direction movementDirection)
        {
            var canContinueInDirection = true;
            var destination = currentLocation.Clone();
            var direction = movementDirection.ToVector();
            while (canContinueInDirection)
            {
                if (!IsBlocking(levelGrid, destination + direction))
                {
                    destination += direction;
                }
                else
                {
                    canContinueInDirection = false;
                }
            }
            return destination.Clone();
        }
    }
}
