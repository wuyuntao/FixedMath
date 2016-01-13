using System;

namespace FixedMath
{
	public struct FVector2 : IEquatable<FVector2>, IFormattable
	{
		public static readonly FVector2 Zero = new FVector2(Fixed.FromInt(0), Fixed.FromInt(0));
		
		public static readonly FVector2 One = new FVector2(Fixed.FromInt(1), Fixed.FromInt(1));

		public static readonly FVector2 Max = new FVector2(Fixed.MaxValue, Fixed.MaxValue);
		
		public static readonly FVector2 Min = new FVector2(Fixed.MinValue, Fixed.MinValue);

		public Fixed X;
		public Fixed Y;

		public FVector2(Fixed value)
		{
			X = value;
			Y = value;
		}

		public FVector2(Fixed x, Fixed y)
		{
			X = x;
			Y = y;
		}

		#region IEquatable

		public override bool Equals(object obj)
		{
			if (!(obj is FVector2))
				return false;

			return Equals((FVector2)obj);
		}

		public bool Equals(FVector2 obj)
		{
			return obj.X == X && obj.Y == Y;
		}

		public override int GetHashCode()
		{
			return (X.GetHashCode() * 0x18d ^ Y.GetHashCode()).GetHashCode();
		}

		#endregion

		#region IFormattable

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("({0}, {1})", X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
		}

		public string ToString(string format)
		{
			return string.Format("({0}, {1})", X.ToString(format), Y.ToString(format));
		}

		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format("({0}, {1})", X.ToString(formatProvider), Y.ToString(formatProvider));
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", X.ToString(), Y.ToString());
		}

		#endregion

		#region Methods

		public Fixed Length()
		{
			return FMath.Sqrt(X * X + Y * Y);
		}

		public Fixed LengthSquared()
		{
			return X * X + Y * Y;
		}

		public void Normalize()
		{
			var length = Length();
			if (length != new Fixed(0))
			{
				X /= length;
				Y /= length;
			}
		}

		public static FVector2 Normalize(FVector2 value)
		{
			value.Normalize();
			return value;
		}

		public static Fixed Distance(FVector2 value1, FVector2 value2)
		{
			return (value1 - value2).Length();
		}

		public static Fixed DistanceSquared(FVector2 value1, FVector2 value2)
		{
			return (value1 - value2).LengthSquared();
		}

        public static Fixed Dot(FVector2 value1, FVector2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }

		#endregion

		#region Arithmetic Operators

		public static FVector2 operator +(FVector2 value)
		{
			return new FVector2(+value.X, +value.Y);
		}

		public static FVector2 operator +(FVector2 left, FVector2 right)
		{
			return new FVector2(left.X + right.X, left.Y + right.Y);
		}

		public static FVector2 operator -(FVector2 value)
		{
			return new FVector2(-value.X, -value.Y);
		}

		public static FVector2 operator -(FVector2 left, FVector2 right)
		{
			return new FVector2(left.X - right.X, left.Y - right.Y);
		}

		public static FVector2 operator *(FVector2 value, Fixed scale)
		{
			return new FVector2(value.X * scale, value.Y * scale);
		}

		public static FVector2 operator *(Fixed scale, FVector2 value)
		{
			return new FVector2(value.X * scale, value.Y * scale);
		}

		public static FVector2 operator *(FVector2 left, FVector2 right)
		{
			return new FVector2(left.X * right.X, left.Y * right.Y);
		}

		public static FVector2 operator /(FVector2 value, Fixed scale)
		{
			return new FVector2(value.X / scale, value.Y / scale);
		}

		public static FVector2 operator /(Fixed scale, FVector2 value)
		{
			return new FVector2(scale / value.X, scale / value.Y);
		}

		public static FVector2 operator /(FVector2 left, FVector2 right)
		{
			return new FVector2(left.X / right.X, left.Y / right.Y);
		}

		#endregion

		#region Relational Operators

		public static bool operator ==(FVector2 left, FVector2 right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(FVector2 left, FVector2 right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		#endregion
	}
}
