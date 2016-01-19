using System;

namespace FixedMath
{
    [Serializable]
    public class FRandom
    {
        private int seed;
        private uint x;
        private uint y;
        private uint z;
        private uint c;

        public FRandom()
            : this(Environment.TickCount)
        { }

        public FRandom(int seed)
        {
            this.seed = seed;

            x = (uint)seed;
            y = 987654321;
            z = 43219876;
            c = 6543217;
        }

        uint JKiss()
        {
            x = 314527869 * x + 1234567;
            y ^= y << 5;
            y ^= y >> 7;
            y ^= y << 22;
            ulong t = ((ulong)4294584393 * z + c);
            c = (uint)(t >> 32);
            z = (uint)t;
            return (x + y + z);
        }

        public int Next(int maxValue) // Return [0, maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("Maximum value is less than minimal value.");

            return (int)(JKiss() % maxValue);
        }

        public int Next(int min, int max) // Return [minValue, maxValue)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("Maximum value is less than minimal value.");

            // special case: a difference of one (or less) will always return the minimum
            // e.g. -1,-1 or -1,0 will always return -1
            uint diff = (uint)(max - min);
            if (diff <= 1)
                return min;

            return min + ((int)(JKiss() % diff));
        }

        public Fixed NextFixed(Fixed min, Fixed max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("Maximum value is less than minimal value.");

            return min + NextFixed() * (max - min);
        }

        public Fixed NextFixed(Fixed max)
        {
            return NextFixed(Fixed.Zero, max);
        }

        public Fixed NextFixed()
        {
#if FIXEDPOINT
            return new Fixed(Next(Fixed.SHIFT_NUMBER + 1));
#else
            // a single 32 bits random value is not enough to create a random double value
            uint a = JKiss() >> 6;  // Upper 26 bits
            uint b = JKiss() >> 5;  // Upper 27 bits
            return new Fixed((a * 134217728.0 + b) / 9007199254740992.0);
#endif
        }

        public int Seed
        {
            get { return seed; }
        }
    }
}
