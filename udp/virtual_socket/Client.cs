using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Encodings;
using System.Text;

namespace virtual_socket
{
    public class Client
    {
        IPAddress serverAddress;
        int serverPort;

        ReliabilityLayer rl = new ReliabilityLayer();

        public Client() { }

        public Client(string address, int port)
        {
            AddAddress(address, port);
        }

        public void AddAddress(string address, int port)
        {
            serverAddress = IPAddress.Parse(address);
            serverPort = port;
            rl.CreateUdpSocket();
        }

        public void SendMessage(string message)
        {
            byte ack;

            do
            {
                rl.SendPacket(message, serverAddress, serverPort);
                ack = rl.WaitForAck();
            }
            while (ack != (byte)0);
        }

        public void SendMessageWithBitError(string message)
        {
            // rl.SendPacketWithBitError(message, serverAddress, serverPort);

            // rl.WaitForAck();

            do
            {
                rl.SendPacketWithBitError(message, serverAddress, serverPort);
            }
            while (!(rl.WaitForAck()).Equals((byte)0));
        }

        public byte[] ReceiveAck()
        {
            byte[] rec = new byte[1];
            int howMany = rl.socket.Receive(rec);

            return rec;
        }

        public static byte[] JoinArrays(byte[] firstArray, byte[] secondArray)
        {
            byte[] finalArray = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, finalArray, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, finalArray, firstArray.Length, secondArray.Length);

            return finalArray;
        }

        public void CloseSocket()
        {
            rl.CloseSocket();
        }
    }
}
