using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;
using System.Text;
using System.Collections;

namespace virtual_socket
{
    public class Server
    {
        private const int bufferSize = 512;

        IPAddress serverAddress { get; set; }
        int serverPort { get; set; }
        public EndPoint clientRemote { get; private set; }
        VirtualSocket socket = null;
        ReliabilityLayer rl = new ReliabilityLayer();


        public Server(string address, int port)
        {
            serverAddress = IPAddress.Parse(address);
            serverPort = port;
        }

        public string ReadFromSocket()
        {
            if (rl.socket != null)
            {
                byte[] rec = new byte[bufferSize];
                int howMany;
                string message = "";

                howMany = rl.ReceiveMessageWithRandomError(rec);
                // howMany = rl.ReceiveMessage(rec);
                // howMany = socket.ReceiveWithPacketDrop(rec);
                // howMany = socket.ReceiveWithDelay(rec);
                // howMany = socket.ReceiveWithBitError(rec);

                if (howMany > 0)
                {
                    SendAck(); // Viesti saatu
                    message = message + Encoding.UTF8.GetString(rec, 0, howMany - 1);
                    clientRemote = rl.remoteEp;
                    return message;
                }

                return "Packet dropped";
            }
            else
            {
                return "Socket is null, cannot read from it";
            }
        }

        private void SendAck()
        {
            Random random = new Random();

            byte[] ack = { (byte)random.Next(255) };
            // rl.socket.SendTo(ack, clientRemote);
            rl.SendPacket(Encoding.UTF8.GetString(ack), rl.remoteEp);
        }

        public void Listen()
        {
            // CreateUdpSocket();
            // BindToEndpoint();
            rl.CreateUdpSocket();
            rl.BindToEndpoint(serverAddress, serverPort);
        }

        private void BindToEndpoint()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Loopback, serverPort);
            EndPoint ep = (EndPoint)iep;
            // socket.Bind(ep);
            rl.socket.Bind(ep);
        }

        public void CreateUdpSocket()
        {
            socket = new VirtualSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.ReceiveTimeout = 5000;
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