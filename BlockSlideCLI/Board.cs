using System;
using System.Collections.Generic;
using BlockSlideCore.DataStructures;
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
        private const ConsoleColor START_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor END_COLOR = ConsoleColor.DarkGreen;
        private Grid<TileType> mGrid; 
        private readonly Player mPlayer;
        private readonly GameInputProcessor mInputProcessor;
        private int mLevel;
        private readonly ILevelBuilder mLevelBuilder;
        private readonly ICollection<Vector2> mValidLocations;

        public Board(Player player)
        {
            mPlayer = player;
            mLevel = 1;

            mValidLocations = new HashSet<Vector2>();
            mInputProcessor = new GameInputProcessor();
            mLevelBuilder = new CampaignLevelBuilder();

            SetupLevel();
        }

        public void SetupLevel()
        {
            mGrid = mLevelBuilder.CreateLevel(mLevel);
            mPlayer.Location = FindStart();
            BuildValidLocations();
            InitialDraw();
        }

        private void BuildValidLocations()
        {
            mValidLocations.Clear();
            var validLocationCalculator = new ValidLocationCalculator();
            var validLocations = validLocationCalculator.BuildValidLocations(FindStart(), GetNeighborInDirection);
            validLocations.ForEach(mValidLocations.Add);
        }

        private Vector2 FindStart()
        {
            Vector2 vector = null;
            mGrid.ForEach((x, y, value) =>
            {
                if (value == TileType.Start)
                {
                    vector = new Vector2(x, y);
                }
            });
            return vector;
        }

        private void InitialDraw()
        {
            Console.Clear();
            Console.Title = string.Format("BlockSlide - Level {0}", mLevel);

            mGrid.ForEach((x, y, value) =>
            {
                if (x == mPlayer.Location.X && y == mPlayer.Location.Y)
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
            DrawTile(mPlayer.Location.PreviousX, mPlayer.Location.PreviousY);
            DrawPlayer(mPlayer.Location.X, mPlayer.Location.Y);
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
            switch (mGrid.Get(x, y))
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
                mPlayer.Location = GetNeighborInDirection(mPlayer.Location.Clone(), direction.Value.ToVector());
            }

            if (mGrid.Get(mPlayer.Location) == TileType.Finish)
            {
                NextLevel();
            }
            Draw();
        }
        
        private bool IsBlocking(Vector2 location)
        {
            var blocking = true;

            if (location.X >= 0 && location.X < mGrid.Width &&
                location.Y >= 0 && location.Y < mGrid.Height)
            {
                if (mGrid.Get(location) != TileType.Wall)
                {
                    blocking = false;
                }
            }
            return blocking;
        }
        
        private Vector2 GetNeighborInDirection(Vector2 start, Vector2 direction)
        {
            var canContinueInDirection = true;
            var destination = start.Clone();
            while (canContinueInDirection)
            {
                if (!IsBlocking(destination + direction))
                {
                    destination += direction;
                }
                else
                {
                    canContinueInDirection = false;
                }
            }
            return destination.Clone();
        }

        public void NextLevel()
        {
            mLevel++;
            SetupLevel();
        }
    }
}
