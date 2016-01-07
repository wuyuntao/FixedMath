using System;

namespace FixedMath
{
#if !FIXEDPOINT

	public struct Fixed : IComparable, IFormattable, IConvertible, IComparable<Fixed>, IEquatable<Fixed>
	{
		internal double RawValue;

		internal Fixed(double rawValue)
		{
			RawValue = rawValue;
		}

		#region Factory Methods

		public static Fixed FromInt(int value)
		{
			return new Fixed((double)value);
		}

		public static Fixed FromFloat(float value)
		{
			return new Fixed((double)value);
		}

		public static Fixed FromFraction(int numerator, int denominator)
		{
			return Fixed.FromInt(numerator) / Fixed.FromInt(denominator);
		}

		#endregion

		#region Parse Methods

		public static Fixed Parse(string s)
		{
			return new Fixed(double.Parse(s));
		}

		public static bool TryParse(string s, out Fixed result)
		{
			double value;
			if (double.TryParse(s, out value))
			{
				result = new Fixed(value);
				return true;
			}
			else
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
			return RawValue.ToString(format, formatProvider);
		}

		public string ToString(string format)
		{
			return RawValue.ToString(format);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return RawValue.ToString(formatProvider);
		}

		public override string ToString()
		{
			return RawValue.ToString();
		}

		#endregion

		#region IConvertable

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToBoolean(provider);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToSByte(provider);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToByte(provider);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToInt16(provider);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToUInt16(provider);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToInt32(provider);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToUInt32(provider);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToInt64(provider);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)RawValue).ToUInt64(provider);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)RawValue;
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return RawValue;
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)RawValue;
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
			return new Fixed(left.RawValue * right.RawValue);
		}

		public static Fixed operator /(Fixed left, Fixed right)
		{
			return new Fixed(left.RawValue / right.RawValue);
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
