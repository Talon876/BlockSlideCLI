using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BlockSlideCore.Levels
{
    public class EmbeddedResourceLevelDataProvider : ILevelDataProvider
    {
        private const string LEVEL_RESOURCE = "BlockSlideCore.GameLevelData.Level{0}.blks";

        public IEnumerable<string> GetLevelData(int level)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(string.Format(LEVEL_RESOURCE, level));
            var streamReader = new StreamReader(stream);
            var levelData = streamReader.ReadToEnd();
            return levelData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
