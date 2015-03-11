﻿using System;

namespace BlockSlideCLI
{
    public class BlockSlideCLI
    {
        public void Start()
        {
            var player = new Player();
            var input = new KeyboardInputManager();
            var board = new Board(player);
            char keypressed;
            do
            {
                keypressed = input.GetInput();
                if (keypressed == 'r')
                {
                    board.SetupLevel();
                }
                if (keypressed == 'n')
                {
                    board.NextLevel();
                }
                board.Step(keypressed);
            } while (keypressed != 'q');
            
            Console.WriteLine("Goodbye!");
        }
    }

}
