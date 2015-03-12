using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Engine
{
    public interface IMovementCalculator
    {
        Vector2 CalculateNewLocation(Grid<TileType> levelGrid, Vector2 currentLocation, Direction movementDirection);
    }
}
