using System;
using System.Threading;

namespace BlockSlideCLI
{
    public class RandomInputManager : IInputManager
    {
        private readonly int mDelay;
        private readonly Random mRandom = new Random();

        public RandomInputManager(int delayInMs)
        {
            mDelay = delayInMs;
        }

        public char GetInput()
        {
            Thread.Sleep(mDelay);
            var choice = mRandom.Next(4);
            char character;
            switch (choice)
            {
                case 0:
                    character = 'w';
                    break;
                case 1:
                    character = 's';
                    break;
                case 2:
                    character = 'a';
                    break;
                default:
                    character = 'd';
                    break;
            }
            if (mRandom.Next(1300) < 2)
            {
                character = 'p';
            }
            return character;
        }
    }
}
