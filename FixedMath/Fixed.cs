using System;

namespace FixedMath
{
#if FIXEDPOINT
	public struct Fixed : IComparable, IFormattable, IConvertible, IComparable<Fixed>, IEquatable<Fixed>
	{
		// https://en.wikipedia.org/wiki/Q_%28number_format%29
		private const int SHIFT_BITS = 20;
		private const int SHIFT_NUMBER = 1048576;   // = Math.Pow(2, 20)

		public static readonly Fixed MAX_VALUE = new Fixed(0x7FFFFFFFFFFL);
		public static readonly Fixed MIX_VALUE = new Fixed(-0x7FFFFFFFFFFL);

		internal long RawValue;

		internal Fixed(long rawValue)
		{
			RawValue = rawValue;
		}

		#region Factory Methods

		public static Fixed FromInt(int value)
		{
			return new Fixed(value << SHIFT_BITS);
		}


		public static Fixed FromFloat(float value)
		{
			return new Fixed((long)(value * SHIFT_NUMBER));
		}

		public static Fixed FromFraction(int numerator, int denominator)
		{
			return Fixed.FromInt(numerator) / Fixed.FromInt(denominator);
		}

		#endregion

		#region Parse Methods

		public static Fixed Parse(string s)
		{
			if (string.IsNullOrEmpty(s))
				throw new FormatException();

			var parts = s.Split('.');
			if (parts.Length == 1)
			{
				return FromInt(int.Parse(parts[0]));
			}
			else if (parts.Length == 2)
			{
				var integerPart = (long)int.Parse(parts[0]);
				var fractionalPart = (long)int.Parse(parts[1]);

				var denominator = 1L;
				while (denominator <= fractionalPart)
					denominator *= 10;

				integerPart = integerPart << SHIFT_BITS;
				fractionalPart = ((fractionalPart << SHIFT_BITS) / denominator);

				return new Fixed(integerPart | fractionalPart);
			}
			else
			{
				throw new FormatException();
			}
		}

		public static bool TryParse(string s, out Fixed result)
		{
			try
			{
				result = Parse(s);
				return true;
			}
			catch (Exception)
			{
				result = new Fixed();
				return false;
			}
		}

		#endregion

		#region IComparable

		public int CompareTo(object value)
		{
			if (!(value is Fixed))
				throw new ArgumentException();

			return CompareTo((Fixed)value);
		}

		public int CompareTo(Fixed value)
		{
			return RawValue.CompareTo(value.RawValue);
		}

		#endregion

		#region IEquatable

		public override bool Equals(object obj)
		{
			if (!(obj is Fixed))
				return false;

			return Equals((Fixed)obj);
		}

		public bool Equals(Fixed obj)
		{
			return obj.RawValue == RawValue;
		}

		public override int GetHashCode()
		{
			return RawValue.GetHashCode();
		}

		#endregion

		#region IFormattable

		public string ToString(string format, IFormatProvider formatProvider)
		{
			var value = (float)RawValue / SHIFT_NUMBER;

			return value.ToString(format, formatProvider);
		}

		public string ToString(string format)
		{
			var value = (float)RawValue / SHIFT_NUMBER;

			return value.ToString(format);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			var value = (float)RawValue / SHIFT_NUMBER;

			return value.ToString(formatProvider);
		}

		public override string ToString()
		{
			var value = (float)RawValue / SHIFT_NUMBER;

			return value.ToString();
		}

		#endregion

		#region IConvertable

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return RawValue != 0;
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)(RawValue >> SHIFT_BITS);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)(RawValue >> SHIFT_BITS);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)(RawValue >> SHIFT_BITS);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)(RawValue >> SHIFT_BITS);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)(RawValue >> SHIFT_BITS);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)(RawValue >> SHIFT_BITS);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (RawValue >> SHIFT_BITS);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)(RawValue >> SHIFT_BITS);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)((decimal)RawValue / SHIFT_NUMBER);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)((decimal)RawValue / SHIFT_NUMBER);
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)RawValue / SHIFT_NUMBER;
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return ToString(provider);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof(bool))
				return ((IConvertible)this).ToBoolean(provider);

			if (conversionType == typeof(sbyte))
				return ((IConvertible)this).ToSByte(provider);

			if (conversionType == typeof(byte))
				return ((IConvertible)this).ToByte(provider);

			if (conversionType == typeof(short))
				return ((IConvertible)this).ToInt16(provider);

			if (conversionType == typeof(ushort))
				return ((IConvertible)this).ToUInt16(provider);

			if (conversionType == typeof(int))
				return ((IConvertible)this).ToInt32(provider);

			if (conversionType == typeof(uint))
				return ((IConvertible)this).ToUInt32(provider);

			if (conversionType == typeof(long))
				return ((IConvertible)this).ToInt64(provider);

			if (conversionType == typeof(ulong))
				return ((IConvertible)this).ToUInt64(provider);

			if (conversionType == typeof(float))
				return ((IConvertible)this).ToSingle(provider);

			if (conversionType == typeof(double))
				return ((IConvertible)this).ToDouble(provider);

			if (conversionType == typeof(Fixed))
				return this;

			throw new InvalidCastException();
		}

		#endregion

		#region Arithmetic Operators

		public static Fixed operator +(Fixed left, Fixed right)
		{
			return new Fixed(left.RawValue + right.RawValue);
		}

		public static Fixed operator -(Fixed left, Fixed right)
		{
			return new Fixed(left.RawValue - right.RawValue);
		}

		public static Fixed operator *(Fixed left, Fixed right)
		{
			return new Fixed((left.RawValue * right.RawValue) >> SHIFT_BITS);
		}

		public static Fixed operator /(Fixed left, Fixed right)
		{
			return new Fixed((left.RawValue << SHIFT_BITS) / right.RawValue);
		}

		public static Fixed operator -(Fixed value)
		{
			return new Fixed(-value.RawValue);
		}

		public static Fixed operator +(Fixed value)
		{
			return new Fixed(value.RawValue);
		}

		#endregion

		#region Relational Operators

		public static bool Equal(Fixed left, Fixed right)
		{
			return left.RawValue == right.RawValue;
		}

		public static bool operator ==(Fixed left, Fixed right)
		{
			return left.RawValue == right.RawValue;
		}

		public static bool operator !=(Fixed left, Fixed right)
		{
			return left.RawValue != right.RawValue;
		}

		public static bool operator <(Fixed left, Fixed right)
		{
			return left.RawValue < right.RawValue;
		}

		public static bool operator >(Fixed left, Fixed right)
		{
			return left.RawValue > right.RawValue;
		}

		public static bool operator <=(Fixed left, Fixed right)
		{
			return left.RawValue <= right.RawValue;
		}

		public static bool operator >=(Fixed left, Fixed right)
		{
			return left.RawValue >= right.RawValue;
		}

		#endregion

		#region Approximate Comparison

		public static bool Approximately(Fixed left, Fixed right)
		{
			return (FMath.Abs(left - right)) < FromFraction(1, 100000) * FMath.Max(FMath.Abs(left), FMath.Abs(right));
		}

		#endregion
	}
#endif
}
