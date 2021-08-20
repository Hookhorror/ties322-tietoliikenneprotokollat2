using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using client;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Server!");
            Server server = new Server();

            try
            {
                server.ListenTo("127.0.0.1", 40000);
                // string message = server.ReadMessage();
                // System.Console.WriteLine(message);
                // message = server.ReadMessage();
                // System.Console.WriteLine(message);
                // message = server.ReadMessage();
                // System.Console.WriteLine(message);
                string message;

                do
                {
                    message = server.ReadMessage();
                    System.Console.WriteLine(message);
                } while (!message.StartsWith("quit"));
            }
            finally
            {
                server.CloseConnection();
            }
        }
    }
}
