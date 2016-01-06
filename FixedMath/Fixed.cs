using System;

namespace FixedMath
{
    public struct Fixed : IComparable, IFormattable, IConvertible, IComparable<Fixed>, IEquatable<Fixed>
    {
        // https://en.wikipedia.org/wiki/Q_%28number_format%29
        private const int SHIFT_BITS = 20;
        private const int SHIFT_NUMBER = 1048576;   // = Math.Pow(2, 20)

        public static readonly Fixed MAX_VALUE = FromRawValue(0x7FFFFFFFFFFL);
        public static readonly Fixed MIX_VALUE = FromRawValue(-0x7FFFFFFFFFFL);

        private long rawValue;

        #region Factory Methods

        private static Fixed FromRawValue(long rawValue)
        {
            return new Fixed() { rawValue = rawValue };
        }

        public static Fixed FromInt(int value)
        {
            return new Fixed() { rawValue = value << SHIFT_BITS };
        }


        public static Fixed FromFloat(float value)
        {
            return new Fixed() { rawValue = (long)(value * SHIFT_NUMBER) };
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
                while (denominator < fractionalPart)
                    denominator *= 10;

                integerPart = integerPart << SHIFT_BITS;
                fractionalPart = ((fractionalPart << SHIFT_BITS) / denominator);

                return new Fixed() { rawValue = integerPart | fractionalPart };
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
            return rawValue.CompareTo(value.rawValue);
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
            return obj.rawValue == rawValue;
        }

        public override int GetHashCode()
        {
            return rawValue.GetHashCode();
        }

        #endregion

        #region IFormattable

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var value = (float)rawValue / SHIFT_NUMBER;

            return value.ToString(format, formatProvider);
        }

        public string ToString(string format)
        {
            var value = (float)rawValue / SHIFT_NUMBER;

            return value.ToString(format);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            var value = (float)rawValue / SHIFT_NUMBER;

            return value.ToString(formatProvider);
        }

        public override string ToString()
        {
            var value = (float)rawValue / SHIFT_NUMBER;

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
            return rawValue != 0;
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return (sbyte)(rawValue >> SHIFT_BITS);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return (byte)(rawValue >> SHIFT_BITS);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return (short)(rawValue >> SHIFT_BITS);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return (ushort)(rawValue >> SHIFT_BITS);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return (int)(rawValue >> SHIFT_BITS);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return (uint)(rawValue >> SHIFT_BITS);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return (rawValue >> SHIFT_BITS);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return (ulong)(rawValue >> SHIFT_BITS);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return (float)((decimal)rawValue / SHIFT_NUMBER);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return (double)((decimal)rawValue / SHIFT_NUMBER);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return (decimal)rawValue / SHIFT_NUMBER;
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
            return new Fixed() { rawValue = left.rawValue + right.rawValue };
        }

        public static Fixed operator -(Fixed left, Fixed right)
        {
            return new Fixed() { rawValue = left.rawValue - right.rawValue };
        }

        public static Fixed operator *(Fixed left, Fixed right)
        {
            return new Fixed() { rawValue = (left.rawValue * right.rawValue) >> SHIFT_BITS };
        }

        public static Fixed operator /(Fixed left, Fixed right)
        {
            return new Fixed() { rawValue = (left.rawValue << SHIFT_BITS) / right.rawValue };
        }

        #endregion

        #region Relational Operators

        public static bool operator ==(Fixed left, Fixed right)
        {
            return left.rawValue == right.rawValue;
        }

        public static bool operator !=(Fixed left, Fixed right)
        {
            return left.rawValue != right.rawValue;
        }

        public static bool operator <(Fixed left, Fixed right)
        {
            return left.rawValue < right.rawValue;
        }

        public static bool operator >(Fixed left, Fixed right)
        {
            return left.rawValue > right.rawValue;
        }

        public static bool operator <=(Fixed left, Fixed right)
        {
            return left.rawValue <= right.rawValue;
        }

        public static bool operator >=(Fixed left, Fixed right)
        {
            return left.rawValue >= right.rawValue;
        }

        #endregion
    }
}
