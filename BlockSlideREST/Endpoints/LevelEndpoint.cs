using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using BlockSlideCore.Analysis;
using BlockSlideCore.Entities;
using BlockSlideCore.Levels;
using Grapevine;
using Grapevine.Server;

namespace BlockSlideREST.Endpoints
{
    public sealed class LevelEndpoint : RESTResource
    {
        private readonly ILevelDataProvider mLevelDataProvider;
        private readonly LevelImageGenerator mLevelImageGenerator;
        private const int MAX_LEVEL = 100;
        private const int MIN_LEVEL = 1;

        public LevelEndpoint()
        {
            mLevelDataProvider = new EmbeddedResourceLevelDataProvider();
            mLevelImageGenerator = new LevelImageGenerator();
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/blockslide/level/\d+$")]
        public void HandleGetLevel(HttpListenerContext context)
        {
            var levelString = context.Request.RawUrl.GrabFirst(@"^/blockslide/level/(\d+)$");
            ProcessRequest(levelString, context, level =>
            {
                Debug.WriteLine("Generating data for level {0}", level);
                var levelData = string.Join(Environment.NewLine, mLevelDataProvider.GetLevelData(level));
                SendTextResponse(context, levelData);
            });
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/blockslide/render/\d+/?(solved)?")]
        public void HandleRenderLevel(HttpListenerContext context)
        {
            var levelString = context.Request.RawUrl.GrabFirst(@"^/blockslide/render/(\d+)");
            var solved = context.Request.RawUrl.GrabFirst(@"^/blockslide/render/\d+/?(solved)?").Any();
            ProcessRequest(levelString, context, level =>
            {
                var levelData = new Level(level);
                var levelImage = mLevelImageGenerator.GenerateImage(levelData, 32, solved);
                context.Response.ContentType = ContentType.PNG.ToString();
                levelImage.Save(context.Response.OutputStream, ImageFormat.Png);
                context.Response.OutputStream.Close();
                context.Response.Close();
            });
        }

        private void ProcessRequest(string levelParameter, HttpListenerContext context, Action<int> action)
        {
            int level;
            if (!int.TryParse(levelParameter, out level))
            {
                NotFound(context, string.Format("{0} is not a valid 32 bit integer.", levelParameter));
            }
            else
            {
                if (level >= MIN_LEVEL && level <= MAX_LEVEL)
                {
                    action(level);
                }
                else
                {
                    NotFound(context, string.Format("Level {0} was not found.", level));
                }
            }
        }
    }
}
