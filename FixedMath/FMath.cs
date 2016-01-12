﻿using System;

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

        #region Trigonometric Functions

        public static Fixed Sin(Fixed angle)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Sin(angle.RawValue));
#else
            // https://en.wikipedia.org/wiki/Trigonometric_functions#Series_definitions
            var pi = PI.RawValue;
            var pi2 = pi << 1;

            // Normalize rad to [-π, +π]
            var rad = angle.RawValue % pi2;
            if (rad > pi)
                rad -= pi2;
            else if (rad < -pi)
                rad += pi2;

            var square = (rad * rad) >> Fixed.SHIFT_BITS;

            var r = rad;                // x
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r -= rad / 6L;              // - x^3 / 3!
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r += rad / 120L;            // + x^5 / 5!
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r -= rad / 5040L;           // - x^7 / 7!
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r += rad / 362880L;         // + x^9 / 9!
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r -= rad / 39916800L;       // - x^11 / 11!
            rad = (rad * square) >> Fixed.SHIFT_BITS;
            r += rad / 6227020800L;     // + x^13 / 13!

            return new Fixed(r);
#endif
        }

        public static Fixed Asin(Fixed d)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Asin(d.RawValue));
#else
            throw new NotImplementedException();
#endif
        }


        public static Fixed Cos(Fixed angle)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Cos(angle.RawValue));
#else
            return Sin(PI / Fixed.FromInt(2) - angle);
#endif
        }

        public static Fixed Acos(Fixed d)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Acos(d.RawValue));
#else
            throw new NotImplementedException();
#endif
        }

        public static Fixed Tan(Fixed angle)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Tan(angle.RawValue));
#else
            return Sin(angle) / Cos(angle);
#endif
        }

        public static Fixed Atan(Fixed d)
        {
#if !FIXEDPOINT
			return new Fixed(Math.Atan(d.RawValue));
#else
            return Atan2(d, Fixed.FromInt(1));
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

        #endregion

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
