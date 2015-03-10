using System.IO;
using System.Linq;
using BlockSlideCLI.Engine;

namespace BlockSlideCLI
{
    public class CampaignLevelBuilder : ILevelBuilder
    {
        private const string LEVEL_FILE = "Levels/Level{0}.map";

        public Grid<TileType> CreateLevel(int level)
        {
            var grid = new Grid<TileType>(Config.WIDTH, Config.HEIGHT);
            var levelData = File.ReadAllLines(string.Format(LEVEL_FILE, level))
                .Select(line =>
                    line.Trim().Split(' ')
                        .Select(int.Parse)
                        .Select(MapTile))
                .ToList();
            for (var y = 0; y < levelData.Count; y++)
            {
                for (var x = 0; x < levelData.ElementAt(y).Count(); x++)
                {
                    grid.Set(x, y, levelData.ElementAt(y).ElementAt(x));
                }
            }
            return grid;
        }

        private TileType MapTile(int value)
        {
            switch (value)
            {
                case 0:
                    return TileType.Floor;
                case 1:
                    return TileType.Wall;
                case 2:
                    return TileType.Start;
                case 3:
                    return TileType.Finish;
                default:
                    return TileType.Floor;
            }
        }
    }
}
