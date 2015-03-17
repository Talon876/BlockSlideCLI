using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BlockSlideCore.Analysis;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;
using BlockSlideCore.Levels;
using BlockSlideCore.Utilities;

namespace BlockSlideCLI
{
    public class Board
    {
        private const ConsoleColor PLAYER_COLOR = ConsoleColor.Green;
        private const ConsoleColor WALL_COLOR = ConsoleColor.Gray;
        private const ConsoleColor FLOOR_COLOR = ConsoleColor.DarkGray;
        private const ConsoleColor VISITED_COLOR = ConsoleColor.DarkYellow;
        private const ConsoleColor PATH_COLOR = ConsoleColor.Magenta;
        private const ConsoleColor START_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor END_COLOR = ConsoleColor.DarkGreen;
        private Level mLevel;
        private readonly GameInputProcessor mInputProcessor;
        private readonly ICollection<Vector2> mValidLocations;
        private readonly ICollection<Vector2> mBestPath; 
        private int mLevelNumber;
        private int mPlayerMoves;
        private bool mShowBestPath;

        public Board(int level)
        {
            mLevelNumber = level;
            mValidLocations = new HashSet<Vector2>();
            mBestPath = new HashSet<Vector2>();
            mInputProcessor = new GameInputProcessor();

            SetupLevel();
        }

        public void SetupLevel()
        {
            var stopwatch = new Stopwatch();
            Console.WriteLine("Generating level... please wait");
            var randomLevelNumber = new Random().Next(100000) + 100;
            stopwatch.Start();
            mLevel = new Level(mLevelNumber, new CampaignLevelBuilder());
            stopwatch.Stop();
            Debug.WriteLine("Took {0}ms (~{1} min) to generate the level.",
                stopwatch.ElapsedMilliseconds,
                stopwatch.Elapsed.TotalMinutes);
            Directory.CreateDirectory("RandomLevels");
            mLevel.SaveToFile(string.Format("{0}/RandomLevel{1}.blks", "RandomLevels", randomLevelNumber));

            mPlayerMoves = 0;
            BuildValidLocations();
            BuildBestPath();
            InitialDraw();
        }

        private void BuildBestPath()
        {
            mBestPath.Clear();
            var shortestPathFinder = new ShortestPathFinder();
            var graphBuilder = new GraphBuilder();

            var rootNode = graphBuilder.BuildGraph(mLevel);
            var shortestPathData = shortestPathFinder.CalculateShortestPathInformation(rootNode, mLevel.StartLocation);
            shortestPathData.GetPath(mLevel.FinishLocation).ForEach(mBestPath.Add);
        }

        private void BuildValidLocations()
        {
            mValidLocations.Clear();
            var validLocationCalculator = new ValidLocationCalculator();
            var validLocations = validLocationCalculator.BuildValidLocations(mLevel);
            validLocations.ForEach(mValidLocations.Add);
        }

        private void InitialDraw()
        {
            Console.Clear();
            Console.Title = string.Format("BlockSlide - Level {0} - Best Path: {1}", mLevel.LevelNumber, mBestPath.Count);

            mLevel.LevelGrid.ForEach((x, y, value) =>
            {
                if (x == mLevel.PlayerLocation.X && y == mLevel.PlayerLocation.Y)
                {
                    DrawPlayer(x, y);
                }
                else
                {
                    DrawTile(x, y);
                }
            });
        }

        private void Draw()
        {
            Console.Title = string.Format("BlockSlide - Level {0} - Moves: {1}/{2}", mLevel.LevelNumber, mPlayerMoves, mBestPath.Count);
            DrawTile(mLevel.PlayerLocation.PreviousX, mLevel.PlayerLocation.PreviousY);
            DrawPlayer(mLevel.PlayerLocation.X, mLevel.PlayerLocation.Y);
        }

        private void DrawPlayer(int x, int y)
        {
            Console.ForegroundColor = PLAYER_COLOR;
            Console.SetCursorPosition(x, y);
            Console.Write('*');
        }

        private void DrawTile(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            var currentLocation = new Vector2(x, y);
            var character = mLevel.LevelGrid.Get(currentLocation).ToCharacter();

            switch (mLevel.LevelGrid.Get(currentLocation))
            {
                case TileType.Floor:
                    Console.ForegroundColor = mValidLocations.Contains(currentLocation)
                        ? VISITED_COLOR
                        : FLOOR_COLOR;
                    if (ShowBestPath && mBestPath.Contains(currentLocation))
                    {
                        Console.ForegroundColor = PATH_COLOR;
                    }
                    break;
                case TileType.Wall:
                    Console.ForegroundColor = WALL_COLOR;
                    break;
                case TileType.Start:
                    Console.ForegroundColor = START_COLOR;
                    break;
                case TileType.Finish:
                    Console.ForegroundColor = END_COLOR;
                    break;
            }
            
            Console.Write(character);
        }
        
        public void Step(char input)
        {
            var direction = mInputProcessor.GetDirectionFromInput(input);
            if (direction != null)
            {
                var lastLocation = mLevel.PlayerLocation.Clone();
                var newLocation = mLevel.Move(direction.Value);
                if (!lastLocation.Equals(newLocation))
                {
                    mPlayerMoves++;
                }
                if (mLevel.FinishLocation.Equals(newLocation))
                {
                    NextLevel();
                }
            }
            Draw();
        }

        public void NextLevel()
        {
            mLevelNumber++;
            SetupLevel();
        }

        public void PreviousLevel()
        {
            mLevelNumber--;
            SetupLevel();
        }

        public bool ShowBestPath
        {
            get { return mShowBestPath; }
            set
            {
                mShowBestPath = value;
                InitialDraw();
            }
        }

        public void ResetPlayerLocation()
        {
            mLevel.ResetPlayerLocation();
        }
    }
}
