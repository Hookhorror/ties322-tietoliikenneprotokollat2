using System;
using System.Net;

namespace client
{
    internal class Client

    {
        private EndPoint ep;
        private PacketHandler ph;

        public Client()
        {
            ph = new PacketHandler();
        }

        internal void ConnectTo(string server, int port)
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(server), port);
            ep = (EndPoint)iep;
        }

        internal void SendMessage(string message)
        {
            ph.SendMessage(message, ep);
        }

        internal void CloseConnection()
        {
            ph.CloseConnection();
        }
    }
}