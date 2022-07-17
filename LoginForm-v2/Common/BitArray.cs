using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Common
{
    public class BitArray
    {
        private static List<bool> _bits;

        public BitArray(string bit)
        {
            _bits = ConvertToBits(bit);
        }

        public BitArray(List<bool> bits)
        {
            _bits = bits;
        }

        public static BitArray Parse(string bit)
        {
            List<bool> Result = ConvertToBits(bit);
            return new BitArray(Result);
        }

        private static List<bool> ConvertToBits(string bit)
        {
            List<bool> Result = new List<bool>();

            char[] Bit = bit.ToArray();
            foreach (var Item in Bit)
            {
                if (Item == '1')
                    Result.Add(true);
                else
                Result.Add(false);
            }

            return Result;
        }

        private static string ConvertBoolsToBit(List<bool> Bools)
        {
            string Result = "";
            foreach (var item in Bools)
            {
                if (item == true)
                    Result += "1";
                else
                    Result += "0";
            }

            return Result;
        }

        public static BitArray Parse(byte Byte)
        {
            return new BitArray(Convert.ToString(Byte,2).PadLeft(8,'0'));
        }

        public static BitArray Parse(byte[] Bytes)
        {
            string Bit = "";
            foreach (var Item in Bytes)
            {
                Bit += Convert.ToString(Item, 2).PadLeft(8, '0');
            }

            return new BitArray(Bit);
        }

        public byte[] GetBytes()
        {
            string Bit = ConvertBoolsToBit(_bits);

            char[] BitArray = Bit.ToArray();
            List<byte> Result = new List<byte>();
            int BitCounter = 0;
            byte ResultPer8Bit = 0;

            for (int i = BitArray.Count() - 1; i != -1; i--)
            {
                char bit = BitArray[i];

                if (bit == '1')
                    ResultPer8Bit += (byte)Math.Pow(2,BitCounter);


                if (BitCounter == 7)
                {
                    Result.Add(ResultPer8Bit);
                    BitCounter = -1;
                    ResultPer8Bit = 0;
                }
                else if(i == 0)
                {
                    Result.Add(ResultPer8Bit);
                    BitCounter = 0;
                    ResultPer8Bit = 0;
                }

                BitCounter++;
            }

            Result.Reverse();
            return Result.ToArray();
        }
    }
}
