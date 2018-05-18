using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public class RandomUtility
    {
        private RNGCryptoServiceProvider rngProvider = null;
        public RandomUtility()
        {
            this.rngProvider = new RNGCryptoServiceProvider();
        }
        public int GetRandom(int max)
        {
            decimal _base = (decimal)long.MaxValue;
            byte[] rndSeries = new byte[8];
            rngProvider.GetBytes(rndSeries);
            return (int)(Math.Abs(BitConverter.ToInt64(rndSeries, 0)) / _base * (max));
        }
        public int GetRandom(int min, int max)
        {
            int value = GetRandom(max - min) + min;
            return value;
        }
    }
}
