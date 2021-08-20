using System;

namespace client
{
    class Apukikkare
    {
        internal static byte[] JoinByteArrays(byte[] firstArray, byte[] secondArray)
        {
            byte[] finalArray = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, finalArray, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, finalArray, firstArray.Length, secondArray.Length);

            return finalArray;
        }

        internal static byte[] RemoveFromEnd(byte[] received, int howMany)
        {
            byte[] finalArray = new byte[howMany];
            System.Buffer.BlockCopy(received, 0, finalArray, 0, howMany);

            return finalArray;
        }

        internal static int RandomNumber(int from, int to)
        {
            Random random = new Random();

            return random.Next(from, to + 1);
        }
    }
}