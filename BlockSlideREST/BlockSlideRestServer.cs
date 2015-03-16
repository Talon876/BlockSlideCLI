using System.Diagnostics;
using Grapevine.Server;

namespace BlockSlideREST
{
    public class BlockSlideRestServer
    {
        public int Port { get; set; }

        public void Start()
        {
            var server = new RESTServer()
            {
                Port = Port.ToString(),
            };
            server.Start();
            Debug.WriteLine(string.Format("Started server at {0}", server.BaseUrl));
        }
    }
}
