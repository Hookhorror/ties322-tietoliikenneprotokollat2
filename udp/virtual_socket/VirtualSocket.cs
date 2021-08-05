using System.Net.Sockets;
using System;

namespace virtual_socket
{
    internal class VirtualSocket : Socket
    {
        private const int packetDroprate = 50;
        private const int delayFrom = 500;
        private const int delayTo = 3000;
        public VirtualSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
                            : base(addressFamily, socketType, protocolType)
        {

        }

        public int ReceiveWithBitError(byte[] received)
        {
            int howMany = Receive(received);
            int byteToChange = ChooseAtRandom(howMany);
            received[byteToChange] = (byte)(received[byteToChange] - ChooseAtRandom(255));

            return howMany;
        }

        private int ChooseAtRandom(int max)
        {
            Random random = new Random();
            int value = random.Next(max);

            return value;
        }

        public int ReceiveWithDelay(byte[] received)
        {
            int howMany = Receive(received);
            RandomDelay(delayFrom, delayTo);

            return howMany;
        }

        public int ReceiveWithPacketDrop(byte[] received)
        {
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
    }
}