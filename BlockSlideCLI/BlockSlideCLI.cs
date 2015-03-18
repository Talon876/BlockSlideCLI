using System;
using System.Collections.Generic;

namespace BlockSlideCLI
{
    public class BlockSlideCLI
    {
        private Dictionary<char, Action<Board>> keyMappings;

        public BlockSlideCLI()
        {
            Console.CursorVisible = false;
            keyMappings = new Dictionary<char, Action<Board>>
            {
                {'r', board => board.SetupLevel()},
                {'n', board => board.NextLevel()},
                {'b', board => board.PreviousLevel()},
                {'h', board => board.ShowBestPath = !board.ShowBestPath},
                {'p', board => board.ResetPlayerLocation()},
            };
        }

        public void Start()
        {
            var input = new KeyboardInputManager();
            var board = new Board(1);
            char keypressed;
            do
            {
                keypressed = input.GetInput();

                if (keyMappings.ContainsKey(keypressed))
                {
                    keyMappings[keypressed](board);
                }

                board.Step(keypressed);
            } while (keypressed != 'q');

            Console.WriteLine("Goodbye!");
        }
    }

}
