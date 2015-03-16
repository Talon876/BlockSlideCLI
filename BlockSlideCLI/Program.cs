using System;
using System.Drawing.Imaging;
using BlockSlideCore.Analysis;
using BlockSlideCore.Entities;
using BlockSlideCore.Levels;

namespace BlockSlideCLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BlockSlide!");

            var randomLevel = new Level(1, new RandomLevelBuilder(8, 8));
            var imageBuilder = new LevelImageGenerator();
            var image = imageBuilder.GenerateImage(randomLevel, 16);
            image.Save("favicon.png", ImageFormat.Png);


            var blockslide = new BlockSlideCLI();
            blockslide.Start();
            Console.Read();
        }

    }
}
