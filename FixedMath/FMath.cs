using System;

namespace FixedMath
{
    public static class FMath
    {
		public static Fixed Abs(Fixed value)
		{
			return value < Fixed.FromInt(0) ? -value : value;
		}
    }
}
