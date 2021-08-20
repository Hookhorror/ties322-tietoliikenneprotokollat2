using System.Net.Sockets;
using System;
using System.Diagnostics;
using System.Net;

namespace virtual_socket
{
    public class VirtualSocket : Socket
    {
        private const int packetDroprate = 100;
        private const int delayFrom = 500;
        private const int delayTo = 3000;
        private const int errorRate = 50;

        public int ReceiveFromWithRandomError(byte[] received, ref EndPoint ep)
        {
            Random random = new Random();
            int howMany = 0;
            int error = random.Next(1, 6); // 50 % chance for error
            switch (error)
            {
                case 1:
                    howMany = ReceiveFromWithBitError(received, ref ep);
                    break;
                case 2:
                    howMany = ReceiveFromWithDelay(received, ref ep);
                    break;
                case 3:
                    // TODO: Ei toimi, pitää toteuttaa jotenkin muuten
                    howMany = ReceiveFromWithPacketDrop(received, ref ep);
                    break;
                default:
                    howMany = ReceiveFrom(received, ref ep);
                    Debug.WriteLine("No error");
                    break;
            }

            return howMany;
        }

        private int ReceiveFromWithBitError(byte[] received, ref EndPoint ep)
        {
            int howMany = ReceiveFrom(received, ref ep);
            int byteToChange = ChooseAtRandom(howMany);
            received[byteToChange] = (byte)(received[byteToChange] - ChooseAtRandom(255));
            Debug.WriteLine("Bit error");

            return howMany;
        }

        private int ReceiveFromWithDelay(byte[] received, ref EndPoint ep)
        {
            int howMany = ReceiveFrom(received, ref ep);
            RandomDelay(delayFrom, delayTo);
            Debug.WriteLine("Packet delayed");

            return howMany;
        }

        private int ReceiveFromWithPacketDrop(byte[] received, ref EndPoint ep)
        {
            byte[] droppingPacket = new byte[received.Length];

            // TODO: jos ei toimi niin muuta ReceiveFromiksi
            Receive(droppingPacket);
            Debug.WriteLine("Packet dropped");

            return -1;
        }

        public int ReceiveWithBitError(byte[] received)
        {
            Debug.WriteLine("Bit error");
            int howMany = Receive(received);
            int byteToChange = ChooseAtRandom(howMany);
            received[byteToChange] = (byte)(received[byteToChange] - ChooseAtRandom(255));

            return howMany;
        }

        public int ReceiveWithDelay(byte[] received)
        {
            Debug.WriteLine("Packet delayed");
            int howMany = Receive(received);
            RandomDelay(delayFrom, delayTo);

            return howMany;
        }

        public int ReceiveWithPacketDrop(byte[] received)
        {
            Debug.WriteLine("Packet dropped");
            int howMany = 0;
            if (PacketDropped())
            {
                byte[] jee = new byte[received.Length];
                Receive(jee);

                return -1;
            }

            howMany = Receive(received);

            return howMany;
        }

        private int ChooseAtRandom(int max)
        {
            Random random = new Random();
            int value = random.Next(max);

            return value;
        }

        public void RandomDelay(int from, int to)
        {
            Random random = new Random();
            int delay = random.Next(from, to);
            System.Threading.Thread.Sleep(delay);
        }

        public bool PacketDropped()
        {
            Random random = new Random();
            int randomInt = random.Next(0, 100);
            if (randomInt <= packetDroprate)
            {
                return true;
            }
            else return false;
        }

        public VirtualSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
                            : base(addressFamily, socketType, protocolType)
        {

        }
    }
}