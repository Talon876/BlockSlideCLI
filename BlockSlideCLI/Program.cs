using System;
using System.IO;
using System.Linq;
using BlockSlideCLI.Engine;

namespace BlockSlideCLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BlockSlide!");
            var blockslide = new BlockSlideCLI();
            blockslide.Start();

            Console.Read();
        }
    }
}
