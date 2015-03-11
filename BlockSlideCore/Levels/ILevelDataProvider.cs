using System.Collections.Generic;

namespace BlockSlideCore.Levels
{
    public interface ILevelDataProvider
    {
        IEnumerable<string> GetLevelData(int level);
    }
}
