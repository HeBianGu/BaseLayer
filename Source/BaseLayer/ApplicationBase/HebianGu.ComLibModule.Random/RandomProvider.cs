//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;

namespace DotNet.Utilities
{
    /// <summary> 随机数管理，最大值、最小值可以自己进行设定 </summary>
    public static class RandomProvider
    {
        public static int Minimum = 100000;
        public static int Maximal = 999999;
        public static int RandomLength = 6;
        private static string RandomString = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ";
        private static Random random = new Random(DateTime.Now.Second);

        /// <summary> 产生随机字符 </summary
        public static string GetRandomString()
        {
            string returnValue = string.Empty;

            for (int i = 0; i < RandomLength; i++)
            {
                int r = random.Next(0, RandomString.Length - 1);

                returnValue += RandomString[r];
            }
            return returnValue;
        }

        /// <summary> 产生随机数 </summary> 
        public static int GetRandom()
        {
            return random.Next(Minimum, Maximal);
        }

        /// <summary> 产生随机数 </summary> 
        public static int GetRandom(int minimum, int maximal)
        {
            return random.Next(minimum, maximal);
        }

        public static bool NextBool()
        {
            return random.NextDouble() > 0.5;
        }

        public static T NextEnum<T>() where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        public static byte[] NextBytes(int length)
        {
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }

        /// <summary> UInt16 </summary>
        public static UInt16 NextUInt16()
        {
            return BitConverter.ToUInt16(NextBytes(2), 0);
        }

        /// <summary> Int16 </summary>
        public static Int16 NextInt16()
        {
            return BitConverter.ToInt16(NextBytes(2), 0);
        }

        /// <summary> Float </summary>
        public static float NextFloat()
        {
            return BitConverter.ToSingle(NextBytes(4), 0);
        }

        /// <summary> 时间日期 </summary>
        public static DateTime NextDateTime(DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }

        /// <summary> 时间日期 </summary>
        public static DateTime NextDateTime()
        {
            return NextDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

    }
}