using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Levels
{
    public interface ILevelBuilder
    {
        Grid<TileType> CreateLevel(int level);
    }
}
