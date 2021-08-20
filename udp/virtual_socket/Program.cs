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
            Client client = new Client();


            try
            {
                client.AddAddress("127.0.0.1", 40000);
                client.SendMessage("Jee1");
                client.ReceiveAck();
                // client.SendMessageWithBitError("Jee");
                // client.ReceiveAck();
                client.SendMessage("Jee2");
                client.ReceiveAck();

                // while (true)
                // {
                //     System.Console.WriteLine(client.ReceiveAck()[0]);
                // }
            }
            finally
            {
                client.CloseSocket();
            }
        }

        private static void SendAndReceiveTestMessage(string message, Client client, Server server)
        {
            client.SendMessage(message);
            System.Console.WriteLine("Client: " + message);
            System.Console.WriteLine("Server: " + server.ReadFromSocket());

            client.SendMessageWithBitError(message);
            System.Console.WriteLine("Client: " + message);
            System.Console.WriteLine("Server: " + server.ReadFromSocket());
        }

        private static void SendMessageWithClient(Client client)
        {
            string viesti = "";
            while (viesti != "quit")
            {
                System.Console.WriteLine();
                Console.Write("Write message > ");
                viesti = Console.ReadLine();
                client.SendMessage(viesti);
            }
        }

        public static byte[] JoinArrays(byte[] firstArray, byte[] secondArray)
        {
            byte[] finalArray = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, finalArray, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, finalArray, firstArray.Length, secondArray.Length);

            return finalArray;
        }


    }
}
