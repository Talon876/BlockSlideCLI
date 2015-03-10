﻿using System;
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
        private const int WIDTH = 18; //18, 78
        private const int HEIGHT = 10; //10, 24
        private Grid<TileType> mGrid; 
        private readonly Player mPlayer;
        private readonly GameInputProcessor mInputProcessor;

        public Board(Player player)
        {
            mInputProcessor = new GameInputProcessor();

            mPlayer = player;
            Reset();
        }

        public void Reset()
        {
            mGrid = new Grid<TileType>(WIDTH, HEIGHT);

            var random = new Random();
            for (var x = 0; x < mGrid.Width; x++)
            {
                for (var y = 0; y < mGrid.Height; y++)
                {
                    var value = random.Next(0, 100);
                    mGrid.Set(x, y, value < 80 ? TileType.Floor : TileType.Wall);
                }
            }
            var start = Vector2.RandomVector(mGrid.Width, mGrid.Height);
            var end = Vector2.RandomVector(mGrid.Width, mGrid.Height);
            mGrid.Set(start, TileType.Start);
            mGrid.Set(end, TileType.Finish);
            mPlayer.Location = start;
            InitialDraw();
        }

        private void InitialDraw()
        {
            Console.Clear();

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
                Reset();
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
            var moving = true;
            
            while (moving)
            {
                if (!IsBlocking(destination + direction))
                {
                    destination += direction;
                }
                else
                {
                    moving = false;
                }
            }
            mPlayer.Location = destination;
        }
    }
}
