using System;
using System.Net;

namespace client
{
    public class Server
    {
        EndPoint ep;
        PacketHandler ph;
        public Server()
        {
            ph = new PacketHandler();
        }

        private void CreateEndPoint(string address, int port)
        {


            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(address), port);
            ep = (EndPoint)iep;
        }

        public void ListenTo(string address, int port)
        {
            CreateEndPoint(address, port);
            BindTo(ep);
        }

        public void CloseConnection()
        {
            ph.CloseConnection();
        }

        public string ReadMessage()
        {
            string message = ph.ReadMessage(ref ep);
            return message;
        }

        private void BindTo(EndPoint ep)
        {
            ph.BindTo(ep);
        }
    }

}