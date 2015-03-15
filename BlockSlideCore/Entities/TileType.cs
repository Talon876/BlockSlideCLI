using System.Collections.Generic;

namespace BlockSlideCore.Entities
{
    public enum TileType
    {
        Floor = 0,
        Wall = 1,
        Start = 2,
        Finish = 3,
    }

    public static class TileTypeUtils
    {
        public static char ToCharacter(this TileType value)
        {
            var mapping = new Dictionary<TileType, char>
            {
                {TileType.Floor, '.'},
                {TileType.Wall, '#'},
                {TileType.Start, '@'},
                {TileType.Finish, '$'},
            };
            return mapping[value];
        }
    }
}
