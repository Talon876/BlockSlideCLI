using System.Linq;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Levels
{
    public class CampaignLevelBuilder : ILevelBuilder
    {
        private readonly ILevelDataProvider mLevelDataGenerator;

        public CampaignLevelBuilder()
            : this(new EmbeddedResourceLevelDataProvider())
        {
        }

        public CampaignLevelBuilder(ILevelDataProvider levelDataGenerator)
        {
            mLevelDataGenerator = levelDataGenerator;
        }

        public Grid<TileType> CreateLevel(int level)
        {
            var levelData = mLevelDataGenerator.GetLevelData(level)
                .Select(line =>
                    line.Trim().ToCharArray()
                        .Select(MapTile))
                .ToList();
            var levelWidth = levelData.ElementAt(0).Count();
            var levelHeight = levelData.Count;
            var grid = new Grid<TileType>(levelWidth, levelHeight);
            for (var y = 0; y < levelData.Count; y++)
            {
                for (var x = 0; x < levelData.ElementAt(y).Count(); x++)
                {
                    grid.Set(x, y, levelData.ElementAt(y).ElementAt(x));
                }
            }
            return grid;
        }

        private TileType MapTile(char value)
        {
            switch (value)
            {
                case '.':
                    return TileType.Floor;
                case '#':
                    return TileType.Wall;
                case '@':
                    return TileType.Start;
                case '$':
                    return TileType.Finish;
                default:
                    return TileType.Floor;
            }
        }
    }
}
