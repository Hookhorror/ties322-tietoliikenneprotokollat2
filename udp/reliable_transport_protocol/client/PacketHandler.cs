using System;
using System.Net;
using System.Text;

namespace client
{
    public class PacketHandler
    {
        // private ReliabilityLayerNegativeAcks rl;
        // private ReliabilityLayerPositiveAcks rl;
        private ReliabilityLayer rl;

        public PacketHandler()
        {
            // rl = new ReliabilityLayerNegativeAcks();
            // rl = new ReliabilityLayerPositiveAcks();
            rl = new ReliabilityLayer();
        }

        internal void SendMessage(string message, EndPoint ep)
        {
            rl.SendMessage(Encoding.UTF8.GetBytes(message), ep);
        }

        internal void CloseConnection()
        {
            rl.CloseConnection();
        }

        internal void BindTo(EndPoint ep)
        {
            rl.BindTo(ep);
        }

        internal string ReadMessage(ref EndPoint ep)
        {
            byte[] messageBytes = rl.ReadMessage(ref ep);
            string message = Encoding.UTF8.GetString(messageBytes);

            return message;
        }
    }
}