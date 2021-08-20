using System;
using System.Net;

namespace client
{
    public class ReliabilityLayerPositiveAcks
    {
        VirtualSocket vs;

        public ReliabilityLayerPositiveAcks()
        {
            vs = VirtualSocket.UdpSocket();
        }

        internal void SendMessage(byte[] message, EndPoint ep)
        {
            byte[] crc8 = { Crc8.ComputeChecksum(message) };
            byte[] finalMessage = Apukikkare.JoinByteArrays(message, crc8);

            byte ack = (byte)1;

            while (true)
            {
                try
                {
                    vs.SendMessage(finalMessage, ep);
                    ack = WaitForAck(ref ep);
                    if (ack == (byte)0)
                    {
                        System.Console.WriteLine("Ack received");
                        break;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    // System.Console.WriteLine("Socket exception in sending");
                    continue;
                    // throw;
                }
            }


        }

        private byte WaitForAck(ref EndPoint ep)
        {
            byte[] ack = new byte[1];
            vs.ReceiveFrom(ack, ref ep);

            return ack[0];
        }

        internal void CloseConnection()
        {
            vs.CloseConnection();
        }

        internal void BindTo(EndPoint ep)
        {
            vs.Bind(ep);
        }

        internal byte[] ReadMessage(ref EndPoint ep)
        {
            byte[] received = new byte[512];
            int howMany;
            byte[] messageBytes = new byte[0];

            try
            {
                do
                {
                    howMany = vs.ReadMessage(received, ref ep);

                } while (howMany < 0);

                messageBytes = Apukikkare.RemoveFromEnd(received, howMany);
                if (DataIsGood(messageBytes))
                {
                    SendAck(ep);
                }
                // else SendNack(ep);

            }
            catch (System.Net.Sockets.SocketException)
            {
                System.Console.WriteLine("Socket exception while reading data");
                // throw;
            }

            return messageBytes;
        }

        private void SendNack(EndPoint ep)
        {
            vs.SendNack(ep);
        }

        private void SendAck(EndPoint ep)
        {
            vs.SendAck(ep);
        }

        private bool DataIsGood(byte[] messageBytes)
        {
            bool dataIsGood;
            byte receivedChecksum = messageBytes[messageBytes.Length - 1];
            byte[] dataWithoutChecksum = Apukikkare.RemoveFromEnd(messageBytes, messageBytes.Length - 1);
            byte computedChecksum = Crc8.ComputeChecksum(dataWithoutChecksum);

            dataIsGood = receivedChecksum.Equals(computedChecksum);

            return dataIsGood;
        }
    }
}