using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;
using System.Text;

namespace virtual_socket
{
    internal class Client
    {
        IPAddress serverAddress;
        int serverPort;

        public Client() { }

        public Client(string address, int port)
        {
            ConnectToSocket(address, port);
        }

        private void ConnectToSocket(string address, int port)
        {
            serverAddress = IPAddress.Parse(address);
            serverPort = port;
        }

        public void SendMessage(string message)
        {
            using (Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                IPEndPoint iep = new IPEndPoint(serverAddress, serverPort);
                EndPoint ep = (EndPoint)iep;

                byte[] viesti = Encoding.UTF8.GetBytes(message + "\n");
                soc.SendTo(viesti, ep);
            }
        }
    }
}
