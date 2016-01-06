using System;
using System.Runtime.InteropServices;

namespace FixedMath
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Fraction : IComparable, IComparable<Fraction>, IEquatable<Fraction>
    {
        private int numerator;

        private int denominator;

        public Fraction(int numer, int denom)
        {
            numerator = numer;
            denominator = denom;
        }

        #region IComparable

        public int CompareTo(object value)
        {
            if (!(value is Fraction))
                throw new ArgumentException();

            return CompareTo((Fraction)value);
        }

        public int CompareTo(Fraction value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable

        public override bool Equals(object obj)
        {
            if (!(obj is Fraction))
                return false;

            return Equals((Fraction)obj);
        }

        public bool Equals(Fraction obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IFormattable

        public override string ToString()
        {
            return string.Format("{0}/{1}", numerator, denominator);
        }

        #endregion


        #region Arithmetic Operators

        public static Fraction operator +(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Relational Operators

        public static bool operator ==(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Fraction left, Fraction right)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
