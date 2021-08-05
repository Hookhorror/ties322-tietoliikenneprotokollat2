using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;
using System.Text;

namespace virtual_socket
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 40000);
            server.Listen();
            // Client client = new Client("127.0.0.1", 40000);


            try
            {
                while (true)
                {
                    System.Console.WriteLine(server.ReadFromSocket());

                }
            }
            finally
            {
                server.CloseSocket();
            }
            // VirtualSocket vs = new VirtualSocket();


            // string viesti = "";
            // while (viesti != "quit")
            // {
            //     Console.Write("Write message > ");
            //     viesti = Console.ReadLine();
            //     client.SendMessage(viesti);
            // }
        }


    }
}
