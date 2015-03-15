using System;
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
            GenerateDotFiles();
            var blockslide = new BlockSlideCLI();
            blockslide.Start();

            Console.Read();
        }

        public static void GenerateDotFiles()
        {
            const string DIRECTORY = "RandomSmallDotFiles";
            var dotFileGenerator = new DotFileGenerator();
            const int COUNT = 50;
            for (var i = 1; i <= COUNT; i++)
            {
                var level = new Level(i, new RandomLevelBuilder(Config.WIDTH, Config.HEIGHT));
                dotFileGenerator.GenerateDotFile(level, DIRECTORY);
            }
            dotFileGenerator.GenerateBatchConvertFile(DIRECTORY,
                @"C:\Users\Talon\SkyDrive\bin\apps\graphviz-2.38\release\bin\", COUNT);

        }
    }
}
