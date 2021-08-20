using System;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 40000;
            Client client = new Client();

            try
            {
                client.ConnectTo(server, port);

                client.SendMessage("Heippa");
                client.SendMessage("Heippa");
                client.SendMessage("Heippa");
                client.SendMessage("Heippa");
                client.SendMessage("Heippa");

                client.SendMessage("quit");
            }
            finally
            {
                client.CloseConnection();
            }


        }
    }
}
