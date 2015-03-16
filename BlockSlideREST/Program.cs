using System;

namespace BlockSlideREST
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new BlockSlideRestServer {Port = 8008};
            server.Start();
        }
    }
}
