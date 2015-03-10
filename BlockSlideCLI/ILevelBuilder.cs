namespace BlockSlideCLI
{
    public interface ILevelBuilder
    {
        TileType[,] CreateLevel(int level);
    }
}
