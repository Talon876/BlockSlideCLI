using System.Collections.Generic;

namespace BlockSlideCLI
{
    public interface ILevelDataGenerator
    {
        IEnumerable<string> GetLevelData(int level);
    }
}
