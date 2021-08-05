using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;
using System.Text;
using System.Collections;

namespace virtual_socket
{
    internal class Server
    {
        private const int bufferSize = 512;

        IPAddress serverAddress { get; set; }
        int serverPort { get; set; }
        // public EndPoint clientRemote { get; private set; }

        VirtualSocket socket = null;

        public Server(string address, int port)
        {
            serverAddress = IPAddress.Parse(address);
            serverPort = port;
        }

        public string ReadFromSocket()
        {
            if (socket != null)
            {
                byte[] rec = new byte[bufferSize];
                int howMany;
                string message = "";

                howMany = socket.ReceiveWithPacketDrop(rec);
                // howMany = socket.ReceiveWithDelay(rec);
                // howMany = socket.ReceiveWithBitError(rec);
                if (howMany >= 0)
                {
                    message = message + Encoding.ASCII.GetString(rec);
                    return message;
                }

                return "Packet dropped";
            }
            else
            {
                return "Socket is null, cannot read from it";
            }
        }

        public void Listen()
        {
            CreateUdpSocket();
            BindToEndpoint();
        }

        private void BindToEndpoint()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, serverPort);
            EndPoint ep = (EndPoint)iep;
            socket.Bind(ep);
        }

        public void CreateUdpSocket()
        {
            socket = new VirtualSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void CloseSocket()
        {
            if (socket != null)
            {
                socket.Close();
            }
        }


    }
}