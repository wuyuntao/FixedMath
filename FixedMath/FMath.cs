using System;

namespace FixedMath
{
	public static class FMath
	{
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
	}
}
