using System;
using System.Collections.Generic;
using System.Net;

namespace BlockSlideCore.Levels
{
    public class WebLevelDataGenerator : ILevelDataProvider
    {
        private const string LEVEL_RESOURCE = "http://nolat.org/blockslide/levels/Level{0}.blks";

        public IEnumerable<string> GetLevelData(int level)
        {
            string levelData;
            using (var webClient = new WebClient())
            {
                levelData = webClient.DownloadString(string.Format(LEVEL_RESOURCE, level));
            }
            levelData.Replace("\r\n", "\n");
            return levelData.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
