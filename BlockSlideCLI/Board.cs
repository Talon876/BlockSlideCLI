using System;
using System.Collections.Generic;
using BlockSlideCore.DataStructures;
using BlockSlideCore.Entities;
using BlockSlideCore.Utilities;

namespace BlockSlideCLI
{
    public class Board
    {
        private const ConsoleColor PLAYER_COLOR = ConsoleColor.Green;
        private const ConsoleColor WALL_COLOR = ConsoleColor.Gray;
        private const ConsoleColor FLOOR_COLOR = ConsoleColor.DarkGray;
        private const ConsoleColor VISITED_COLOR = ConsoleColor.DarkYellow;
        private const ConsoleColor START_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor END_COLOR = ConsoleColor.DarkGreen;
        private Level mLevel;
        private readonly GameInputProcessor mInputProcessor;
        private readonly ICollection<Vector2> mValidLocations;
        private int mLevelNumber;
        public Board(int level)
        {
            mLevelNumber = level;
            mValidLocations = new HashSet<Vector2>();
            mInputProcessor = new GameInputProcessor();

            SetupLevel();
        }

        public void SetupLevel()
        {
            mLevel = new Level(mLevelNumber);
            BuildValidLocations();
            InitialDraw();
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
            Console.Title = string.Format("BlockSlide - Level {0}", mLevel.LevelNumber);

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
            char character;
            switch (mLevel.LevelGrid.Get(x, y))
            {
                case TileType.Floor:
                    Console.ForegroundColor = mValidLocations.Contains(new Vector2(x, y))
                        ? VISITED_COLOR
                        : FLOOR_COLOR;
                    character = '.';
                    break;
                case TileType.Wall:
                    Console.ForegroundColor = WALL_COLOR;
                    character = '#';
                    break;
                case TileType.Start:
                    Console.ForegroundColor = START_COLOR;
                    character = '@';
                    break;
                case TileType.Finish:
                    Console.ForegroundColor = END_COLOR;
                    character = '$';
                    break;
                default:
                    character = '?';
                    break;
            }
            Console.Write(character);
        }
        
        public void Step(char input)
        {
            var direction = mInputProcessor.GetDirectionFromInput(input);
            if (direction != null)
            {
                var newLocation = mLevel.Move(direction.Value);
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
    }
}
