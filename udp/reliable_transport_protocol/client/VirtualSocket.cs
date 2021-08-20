using System.Net;
using System.Net.Sockets;

namespace client
{
    public class VirtualSocket : Socket
    {
        Sabotager sabotager;

        internal void SendMessage(byte[] message, EndPoint ep)
        {
            SendTo(message, ep);
        }

        internal static VirtualSocket UdpSocket()
        {
            VirtualSocket vs = new VirtualSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            vs.ReceiveTimeout = 3000;

            return vs;
        }

        internal void CloseConnection()
        {
            Close();
        }

        public VirtualSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
                            : base(addressFamily, socketType, protocolType)
        {
            sabotager = new Sabotager();
        }

        internal int ReceiveSabotagedFrom(byte[] received, ref EndPoint ep)
        {
            int howMany = ReceiveFrom(received, ref ep);
            sabotager.SabotageRandomly(received, ref howMany);
            // sabotager.MakeBitErrorRandomly(received, 50);

            return howMany;
        }

        internal int ReadMessage(byte[] received, ref EndPoint ep)
        {
            int howMany = ReceiveSabotagedFrom(received, ref ep);

            return howMany;
        }

        internal void SendAck(EndPoint ep)
        {
            System.Console.WriteLine("Akki lähti");
            SendTo(new byte[] { (byte)0 }, ep);
        }

        internal void SendNack(EndPoint ep)
        {
            System.Console.WriteLine("Nakki napsahti");
            SendTo(new byte[] { (byte)1 }, ep);
        }
    }
}