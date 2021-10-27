using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WebConfigReader cfgReader = new WebConfigReader();
            WebServerConfig config = cfgReader.Read("config");
            new WebServerConfig();
            
            WebServer server(config);
        }
    }
}
