using System;

namespace FixedMath
{
	public static class FMath
	{
		public static readonly Fixed E = Fixed.FromFraction(2718281, 1000000);

		public static readonly Fixed PI = Fixed.FromFraction(3141592, 1000000);

		public static Fixed Abs(Fixed value)
		{
			return value < Fixed.FromInt(0) ? -value : value;
		}

		public static Fixed Max(Fixed val1, Fixed val2)
		{
			return val1 >= val2 ? val1 : val2;
		}

		public static Fixed Min(Fixed val1, Fixed val2)
		{
			return val1 <= val2 ? val1 : val2;
		}

		public static Fixed Sin(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Sin(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Asin(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Asin(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}


		public static Fixed Cos(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Cos(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Acos(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Acos(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Tan(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Tan(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Atan(Fixed angle)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Atan(angle.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Atan2(Fixed y, Fixed x)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Atan2(y.RawValue, x.RawValue));
#else
			throw new NotImplementedException();
#endif
		}

		public static Fixed Sqrt(Fixed value)
		{
#if !FIXEDPOINT
			return new Fixed(Math.Sqrt(value.RawValue));
#else
            // https://en.wikipedia.org/wiki/Methods_of_computing_square_roots#Binary_numeral_system_.28base_2.29
            var v = value.RawValue << Fixed.SHIFT_BITS;
            var r = 0L;
            var bit = 1L << 62;

            while (bit > v)
                bit >>= 2;

            while (bit != 0)
            {
                if (v >= r + bit)
                {
                    v -= r + bit;
                    r = (r >> 1) + bit;
                }
                else
                    r >>= 1;
                bit >>= 2;
            }

            return new Fixed(r);
#endif
		}
	}
}
