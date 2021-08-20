using System;

namespace client
{
    internal class Sabotager
    {
        public Sabotager() { }

        internal void Delay(int howLong)
        {
            System.Console.WriteLine("Delay for " + howLong);
            System.Threading.Thread.Sleep(howLong);
        }

        internal void MakeBitError(byte[] data)
        {
            System.Console.WriteLine("Bit error");
            data[0] = (byte)(data[0] - 1);
        }

        internal void MakeBitErrorRandomly(byte[] data, int chanceForError)
        {
            bool error = chanceForError < Apukikkare.RandomNumber(0, 100);
            if (error)
            {
                MakeBitError(data);
            }
        }

        internal int DropPacket()
        {
            System.Console.WriteLine("Packet dropped");
            return -1;
        }

        internal void Sabotage(byte[] received)
        {
            Delay(100);
            MakeBitError(received);
            // DropPacket();
        }

        internal void SabotageRandomly(byte[] received, ref int howMany)
        {
            int luku = Apukikkare.RandomNumber(1, 100);
            if (luku > 50)
            {
                int error = Apukikkare.RandomNumber(1, 3);
                switch (error)
                {
                    case 1:
                        MakeBitError(received);
                        break;
                    case 2:
                        Delay(Apukikkare.RandomNumber(100, 500));
                        break;
                    case 3:
                        howMany = DropPacket();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}