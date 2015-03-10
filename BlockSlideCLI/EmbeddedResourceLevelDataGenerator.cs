using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BlockSlideCLI
{
    public class EmbeddedResourceLevelDataGenerator : ILevelDataGenerator
    {
        private const string LEVEL_RESOURCE = "BlockSlideCLI.Levels.Level{0}.map";

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
