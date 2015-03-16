using System.Collections.Generic;
using System.Drawing;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;

namespace BlockSlideCore.Analysis
{
    public class LevelImageGenerator
    {
        public Bitmap GenerateImage(Level level, int tileSize = 32, bool drawBestPath = true)
        {
            var bitmap = new Bitmap(level.LevelGrid.Width*tileSize, level.LevelGrid.Height*tileSize);
            var graphics = Graphics.FromImage(bitmap);
            var brushMap = new Dictionary<TileType, Brush>
            {
                {TileType.Floor, new SolidBrush(Color.AliceBlue)},
                {TileType.Wall, new SolidBrush(Color.MidnightBlue)},
                {TileType.Start, new SolidBrush(Color.Lime)},
                {TileType.Finish, new SolidBrush(Color.Red)},
            };
            level.LevelGrid.ForEach((x, y, value) =>
                graphics.FillRectangle(brushMap[value], x*tileSize, y*tileSize, tileSize, tileSize));

            if (drawBestPath)
            {
                var graphNode = new GraphBuilder().BuildGraph(level);
                var bestPath = new ShortestPathFinder().CalculateShortestPathInformation(graphNode,
                    level.StartLocation).GetPath(level.FinishLocation);
                bestPath.Insert(0, level.StartLocation);
                var bestLinePen = new Pen(Color.Magenta);
                for (var i = 0; i < bestPath.Count - 1; i++)
                {
                    var item = bestPath[i];
                    var nextItem = bestPath[i + 1];
                    graphics.DrawLine(bestLinePen, item.X * tileSize + tileSize / 2, item.Y * tileSize + tileSize / 2,
                        nextItem.X * tileSize + tileSize / 2, nextItem.Y * tileSize + tileSize / 2);
                }
            }
            return bitmap;
        }
    }
}
