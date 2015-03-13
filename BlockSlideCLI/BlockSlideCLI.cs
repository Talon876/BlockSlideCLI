using System;

namespace BlockSlideCLI
{
    public class BlockSlideCLI
    {
        public void Start()
        {
            var input = new KeyboardInputManager();
            var board = new Board(1);
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
                if (keypressed == 'b')
                {
                    board.PreviousLevel();
                }
                board.Step(keypressed);
            } while (keypressed != 'q');
            
            Console.WriteLine("Goodbye!");
        }
    }

}
