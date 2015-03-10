using System;
using BlockSlideCLI.Engine;

namespace BlockSlideCLI
{
    public class Board
    {
        private const ConsoleColor PLAYER_COLOR = ConsoleColor.Green;
        private const ConsoleColor WALL_COLOR = ConsoleColor.DarkGray;
        private const ConsoleColor VISITED_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor START_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor END_COLOR = ConsoleColor.DarkGreen;
        private Grid<TileType> mGrid; 
        private readonly Player mPlayer;
        private readonly GameInputProcessor mInputProcessor;
        private int mLevel;
        private ILevelBuilder mLevelBuilder;

        public Board(Player player)
        {
            mPlayer = player;
            mInputProcessor = new GameInputProcessor();
            mLevel = 1;
            mLevelBuilder = new RandomLevelBuilder(Config.WIDTH, Config.HEIGHT);

            SetupLevel();
        }

        public void SetupLevel()
        {
            mGrid = mLevelBuilder.CreateLevel(mLevel);
            mPlayer.Location = FindStart();
            InitialDraw();
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
                    Console.ForegroundColor = WALL_COLOR;
                    character = ' ';
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
                CalculateNewLocation(direction.Value.ToVector());
            }

            if (mGrid.Get(mPlayer.Location) == TileType.Finish)
            {
                mLevel++;
                SetupLevel();
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

        private void CalculateNewLocation(Vector2 direction)
        {
            var source = mPlayer.Location.Clone();
            var destination = source.Clone();
            var canContinueInDirection = true;
            
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
            mPlayer.Location = destination;
        }
    }
}
