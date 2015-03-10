using BlockSlideCLI.Engine;

namespace BlockSlideCLI
{
    public interface ILevelBuilder
    {
        Grid<TileType> CreateLevel(int level);
    }
}
