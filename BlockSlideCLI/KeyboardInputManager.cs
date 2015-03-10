using System;

namespace BlockSlideCLI
{
    public class KeyboardInputManager : IInputManager
    {
        public char GetInput()
        {
            var key = Console.ReadKey(true);
            return key.KeyChar;
        }
    }
}
