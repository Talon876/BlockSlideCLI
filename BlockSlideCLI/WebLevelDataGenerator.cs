using System;
using System.Collections.Generic;

namespace BlockSlideCLI
{
    public class WebLevelDataGenerator : ILevelDataGenerator
    {
        private const string LEVEL_RESOURCE = "http://nolat.org/blockslide/levels/Level{0}.map";

        public IEnumerable<string> GetLevelData(int level)
        {
            string levelData;
            using (var webClient = new System.Net.WebClient())
            {
                levelData = webClient.DownloadString(string.Format(LEVEL_RESOURCE, level));
            }
            return levelData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
