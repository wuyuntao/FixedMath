using System;
using System.Runtime.InteropServices;

namespace FixedMath
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Fraction : IComparable, IComparable<Fraction>, IEquatable<Fraction>
    {
        private const int NAN_NUMERATOR = 0;
        private const int POSITIVE_INFINITY_NUMERATOR = 1;
        private const int NEGATIVE_INFINITY_NUMERATOR = -1;

        public static readonly Fraction NaN = new Fraction(NAN_NUMERATOR, 0);
        public static readonly Fraction PositiveInfinity = new Fraction(POSITIVE_INFINITY_NUMERATOR, 0);
        public static readonly Fraction NegativeInfinity = new Fraction(NEGATIVE_INFINITY_NUMERATOR, 0);

        public static readonly Fraction Epsilon = new Fraction(1, int.MaxValue);
        public static readonly Fraction MinValue = new Fraction(int.MinValue, 1);
        public static readonly Fraction MaxValue = new Fraction(int.MaxValue, 1);

        public static readonly Fraction Zero = new Fraction(0, 1);
        public static readonly Fraction One = new Fraction(1, 1);

        public int Numerator;

        public int Denominator;

        public Fraction(int integer)
        {
            if (integer == int.MinValue)
                integer++;	// prevent serious issues later..

            Numerator = integer;
            Denominator = 1;
        }

        public Fraction(int numerator, int denominator)
        {
            if (numerator == int.MinValue)
                numerator++;    // prevent serious issues later..

            if (denominator == int.MinValue)
                denominator++;	// prevent serious issues later..

            Numerator = numerator;
            Denominator = denominator;
        }

        #region Parse Methods

        public static Fraction Parse(string s)
        {
            var value = ParseAsFraction(s);
            if (value == null)
            {
                value = ParseAsDecimal(s);
                if (value == null)
                    value = new Fraction(int.Parse(s));
            }

            return value.Value;
        }

        private static Fraction? ParseAsFraction(string s)
        {
            var parts = s.Split('/');
            if (parts.Length == 1)
            {
                return null;
            }
            else if (parts.Length == 2)
            {
                var numerator = int.Parse(parts[0]);
                var denominator = int.Parse(parts[1]);

                return new Fraction(numerator, denominator);
            }
            else
                throw new FormatException();
        }

        private static Fraction? ParseAsDecimal(string s)
        {
            var dot = s.IndexOf('.');
            if (dot < 0)
            {
                return null;
            }
            else if (dot == 0 || dot == s.Length - 1)
            {
                throw new FormatException();
            }
            else
            {
                s = s.Remove(dot, 1);
                var numerator = int.Parse(s);

                var denominator = 1;
                for (var i = dot; i < s.Length; i++)
                    denominator *= 10;

                return new Fraction(numerator, denominator);
            }
        }

        public static bool TryParse(string s, out Fraction value)
        {
            try
            {
                value = Parse(s);
                return true;
            }
            catch
            {
                value = NaN;
                return false;
            }
        }

        #endregion

        #region IComparable

        public int CompareTo(object value)
        {
            if (!(value is Fraction))
                throw new ArgumentException();

            return CompareTo((Fraction)value);
        }

        public int CompareTo(Fraction value)
        {
            // if left is an indeterminate, punt to the helper...
            if (Denominator == 0)
            {
                return IndeterminantCompare(Math.Sign(Numerator), value);
            }

            // if value is an indeterminate, punt to the helper...
            if (value.Denominator == 0)
            {
                // note sign-flip...
                return -IndeterminantCompare(Math.Sign(value.Numerator), this);
            }

            // they're both normal Fractions
            CrossReducePair(ref this, ref value);

            checked
            {
                var leftScale = Numerator * value.Denominator;
                var rightScale = Denominator * value.Numerator;

                return leftScale.CompareTo(rightScale);
            }
        }

        private static int IndeterminantCompare(int leftIndeterminant, Fraction right)
        {
            switch (leftIndeterminant)
            {
                case NAN_NUMERATOR:
                    if (right.IsNaN())
                        return 0;
                    else if (right.IsNegativeInfinity())
                        return 1;
                    else
                        return -1;

                case POSITIVE_INFINITY_NUMERATOR:
                    if (right.IsPositiveInfinity())
                        return 0;
                    else
                        return -1;

                case NEGATIVE_INFINITY_NUMERATOR:
                    if (right.IsNegativeInfinity())
                        return 0;
                    else
                        return -1;

                default:
                    // this CAN'T happen, something VERY wrong is going on...
                    return 0;
            }
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
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return (Numerator.GetHashCode() * 0x18d ^ Denominator.GetHashCode()).GetHashCode();
        }

        public bool IsNaN()
        {
            return Denominator == 0 && Numerator == NAN_NUMERATOR;
        }

        public bool IsInfinity()
        {
            return Denominator == 0 && Numerator != NAN_NUMERATOR;
        }
        public bool IsPositiveInfinity()
        {
            return Denominator == 0 && Numerator > 0;
        }

        public bool IsNegativeInfinity()
        {
            return Denominator == 0 && Numerator < 0;
        }

        #endregion

        #region IFormattable

        public override string ToString()
        {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }

        #endregion

        #region Reduction

        public void Reduce()
        {
            // clean up the NaNs and infinites
            if (Denominator == 0)
            {
                return;
            }

            // all forms of zero are alike.
            if (Numerator == 0)
            {
                Denominator = 1;
                return;
            }

            var iGCD = GCD(Numerator, Denominator);
            Numerator /= iGCD;
            Denominator /= iGCD;

            // if negative sign in denominator
            if (Denominator < 0)
            {
                //move negative sign to numerator
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        private static void CrossReducePair(ref Fraction frac1, ref Fraction frac2)
        {
            // leave the indeterminates alone!
            if (frac1.Denominator == 0 || frac2.Denominator == 0)
                return;

            var gcdTop = GCD(frac1.Numerator, frac2.Denominator);
            frac1.Numerator = frac1.Numerator / gcdTop;
            frac2.Denominator = frac2.Denominator / gcdTop;

            var gcdBottom = GCD(frac1.Denominator, frac2.Numerator);
            frac2.Numerator = frac2.Numerator / gcdBottom;
            frac1.Denominator = frac1.Denominator / gcdBottom;
        }

        private static int GCD(int left, int right)
        {
            // take absolute values
            if (left < 0)
                left = -left;

            if (right < 0)
                right = -right;

            // if we're dealing with any zero or one, the GCD is 1
            if (left < 2 || right < 2)
                return 1;

            do
            {
                if (left < right)
                {
                    int temp = left;  // swap the two operands
                    left = right;
                    right = temp;
                }

                left %= right;
            } while (left != 0);

            return right;
        }

        #endregion

        #region Inverse

        public void Inverse()
        {
            var temp = Numerator;
            Numerator = Denominator;
            Denominator = temp;
        }

        #endregion

        #region Conversion

        public int ToInt()
        {
            if (Denominator == 0)
                throw new ArgumentException();

            return Numerator / Denominator;
        }

        public float ToFloat()
        {
            if (Denominator == 0)
            {
                if (IsNaN())
                    return float.NaN;
                else if (IsPositiveInfinity())
                    return float.PositiveInfinity;
                else
                    return float.NegativeInfinity;
            }

            return (float)Numerator / Denominator;
        }

        #endregion

        #region Arithmetic Operators

        public static Fraction operator +(Fraction left, Fraction right)
        {
            if (left.IsNaN() || right.IsNaN())
                return NaN;

            var gcd = GCD(left.Denominator, right.Denominator); // cannot return less than 1
            var leftDenominator = left.Denominator / gcd;
            var rightDenominator = right.Denominator / gcd;

            checked
            {
                var numerator = left.Numerator * rightDenominator + right.Numerator * leftDenominator;
                var denominator = leftDenominator * rightDenominator * gcd;

                return new Fraction(numerator, denominator);
            }
        }

        public static Fraction operator -(Fraction left, Fraction right)
        {
            return left + (-right);
        }

        public static Fraction operator *(Fraction left, Fraction right)
        {
            if (left.IsNaN() || right.IsNaN())
                return NaN;

            // this would be unsafe if we were not a ValueType, because we would be changing the
            // caller's values.  If we change back to a class, must use temporaries
            CrossReducePair(ref left, ref right);

            checked
            {
                var numerator = left.Numerator * right.Numerator;
                var denominator = left.Denominator * right.Denominator;

                return new Fraction(numerator, denominator);
            }
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            right.Inverse();

            return left * right;
        }

        public static Fraction operator +(Fraction value)
        {
            return new Fraction(value.Numerator, value.Denominator);
        }

        public static Fraction operator -(Fraction value)
        {
            return new Fraction(-value.Numerator, value.Denominator);
        }

        #endregion

        #region Relational Operators

        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) != 0;
        }

        public static bool operator <(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion
    }
}
