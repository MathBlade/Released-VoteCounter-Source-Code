// GENERATED CODE
using System;
using System.Runtime.CompilerServices;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [System.Serializable]
    public partial struct double2x2 : System.IEquatable<double2x2>, IFormattable
    {
        public double2 c0;
        public double2 c1;

        /// <summary>double2x2 identity transform.</summary>
        public static readonly double2x2 identity = new double2x2(1.0, 0.0,   0.0, 1.0);

        /// <summary>double2x2 zero value.</summary>
        public static readonly double2x2 zero = new double2x2(0.0, 0.0,   0.0, 0.0);


        /// <summary>Constructs a double2x2 matrix from two double2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(double2 c0, double2 c1)
        { 
            this.c0 = c0;
            this.c1 = c1;
        }

        /// <summary>Constructs a double2x2 matrix from 4 double values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(double m00, double m01,
                         double m10, double m11)
        { 
            this.c0 = new double2(m00, m10);
            this.c1 = new double2(m01, m11);
        }

        /// <summary>Constructs a double2x2 matrix from a single double value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(double v)
        {
            this.c0 = v;
            this.c1 = v;
        }

        /// <summary>Constructs a double2x2 matrix from a single bool value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(bool v)
        {
            this.c0 = math.select(new double2(0.0), new double2(1.0), v);
            this.c1 = math.select(new double2(0.0), new double2(1.0), v);
        }

        /// <summary>Constructs a double2x2 matrix from a bool2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(bool2x2 v)
        {
            this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
            this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
        }

        /// <summary>Constructs a double2x2 matrix from a single int value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(int v)
        {
            this.c0 = v;
            this.c1 = v;
        }

        /// <summary>Constructs a double2x2 matrix from a int2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(int2x2 v)
        {
            this.c0 = v.c0;
            this.c1 = v.c1;
        }

        /// <summary>Constructs a double2x2 matrix from a single uint value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(uint v)
        {
            this.c0 = v;
            this.c1 = v;
        }

        /// <summary>Constructs a double2x2 matrix from a uint2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(uint2x2 v)
        {
            this.c0 = v.c0;
            this.c1 = v.c1;
        }

        /// <summary>Constructs a double2x2 matrix from a single float value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(float v)
        {
            this.c0 = v;
            this.c1 = v;
        }

        /// <summary>Constructs a double2x2 matrix from a float2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double2x2(float2x2 v)
        {
            this.c0 = v.c0;
            this.c1 = v.c1;
        }


        /// <summary>Implicitly converts a single double value to a double2x2 matrix by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(double v) { return new double2x2(v); }

        /// <summary>Explicitly converts a single bool value to a double2x2 matrix by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator double2x2(bool v) { return new double2x2(v); }

        /// <summary>Explicitly converts a bool2x2 matrix to a double2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator double2x2(bool2x2 v) { return new double2x2(v); }

        /// <summary>Implicitly converts a single int value to a double2x2 matrix by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(int v) { return new double2x2(v); }

        /// <summary>Implicitly converts a int2x2 matrix to a double2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(int2x2 v) { return new double2x2(v); }

        /// <summary>Implicitly converts a single uint value to a double2x2 matrix by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(uint v) { return new double2x2(v); }

        /// <summary>Implicitly converts a uint2x2 matrix to a double2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(uint2x2 v) { return new double2x2(v); }

        /// <summary>Implicitly converts a single float value to a double2x2 matrix by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(float v) { return new double2x2(v); }

        /// <summary>Implicitly converts a float2x2 matrix to a double2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2x2(float2x2 v) { return new double2x2(v); }


        /// <summary>Returns the result of a componentwise multiplication operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator * (double2x2 lhs, double2x2 rhs) { return new double2x2 (lhs.c0 * rhs.c0, lhs.c1 * rhs.c1); }

        /// <summary>Returns the result of a componentwise multiplication operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator * (double2x2 lhs, double rhs) { return new double2x2 (lhs.c0 * rhs, lhs.c1 * rhs); }

        /// <summary>Returns the result of a componentwise multiplication operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator * (double lhs, double2x2 rhs) { return new double2x2 (lhs * rhs.c0, lhs * rhs.c1); }


        /// <summary>Returns the result of a componentwise addition operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator + (double2x2 lhs, double2x2 rhs) { return new double2x2 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1); }

        /// <summary>Returns the result of a componentwise addition operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator + (double2x2 lhs, double rhs) { return new double2x2 (lhs.c0 + rhs, lhs.c1 + rhs); }

        /// <summary>Returns the result of a componentwise addition operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator + (double lhs, double2x2 rhs) { return new double2x2 (lhs + rhs.c0, lhs + rhs.c1); }


        /// <summary>Returns the result of a componentwise subtraction operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator - (double2x2 lhs, double2x2 rhs) { return new double2x2 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1); }

        /// <summary>Returns the result of a componentwise subtraction operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator - (double2x2 lhs, double rhs) { return new double2x2 (lhs.c0 - rhs, lhs.c1 - rhs); }

        /// <summary>Returns the result of a componentwise subtraction operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator - (double lhs, double2x2 rhs) { return new double2x2 (lhs - rhs.c0, lhs - rhs.c1); }


        /// <summary>Returns the result of a componentwise division operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator / (double2x2 lhs, double2x2 rhs) { return new double2x2 (lhs.c0 / rhs.c0, lhs.c1 / rhs.c1); }

        /// <summary>Returns the result of a componentwise division operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator / (double2x2 lhs, double rhs) { return new double2x2 (lhs.c0 / rhs, lhs.c1 / rhs); }

        /// <summary>Returns the result of a componentwise division operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator / (double lhs, double2x2 rhs) { return new double2x2 (lhs / rhs.c0, lhs / rhs.c1); }


        /// <summary>Returns the result of a componentwise modulus operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator % (double2x2 lhs, double2x2 rhs) { return new double2x2 (lhs.c0 % rhs.c0, lhs.c1 % rhs.c1); }

        /// <summary>Returns the result of a componentwise modulus operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator % (double2x2 lhs, double rhs) { return new double2x2 (lhs.c0 % rhs, lhs.c1 % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator % (double lhs, double2x2 rhs) { return new double2x2 (lhs % rhs.c0, lhs % rhs.c1); }


        /// <summary>Returns the result of a componentwise increment operation on a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator ++ (double2x2 val) { return new double2x2 (++val.c0, ++val.c1); }


        /// <summary>Returns the result of a componentwise decrement operation on a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator -- (double2x2 val) { return new double2x2 (--val.c0, --val.c1); }


        /// <summary>Returns the result of a componentwise less than operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator < (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 < rhs.c0, lhs.c1 < rhs.c1); }

        /// <summary>Returns the result of a componentwise less than operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator < (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 < rhs, lhs.c1 < rhs); }

        /// <summary>Returns the result of a componentwise less than operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator < (double lhs, double2x2 rhs) { return new bool2x2 (lhs < rhs.c0, lhs < rhs.c1); }


        /// <summary>Returns the result of a componentwise less or equal operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator <= (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1); }

        /// <summary>Returns the result of a componentwise less or equal operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator <= (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 <= rhs, lhs.c1 <= rhs); }

        /// <summary>Returns the result of a componentwise less or equal operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator <= (double lhs, double2x2 rhs) { return new bool2x2 (lhs <= rhs.c0, lhs <= rhs.c1); }


        /// <summary>Returns the result of a componentwise greater than operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator > (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 > rhs.c0, lhs.c1 > rhs.c1); }

        /// <summary>Returns the result of a componentwise greater than operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator > (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 > rhs, lhs.c1 > rhs); }

        /// <summary>Returns the result of a componentwise greater than operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator > (double lhs, double2x2 rhs) { return new bool2x2 (lhs > rhs.c0, lhs > rhs.c1); }


        /// <summary>Returns the result of a componentwise greater or equal operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator >= (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator >= (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 >= rhs, lhs.c1 >= rhs); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator >= (double lhs, double2x2 rhs) { return new bool2x2 (lhs >= rhs.c0, lhs >= rhs.c1); }


        /// <summary>Returns the result of a componentwise unary minus operation on a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator - (double2x2 val) { return new double2x2 (-val.c0, -val.c1); }


        /// <summary>Returns the result of a componentwise unary plus operation on a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 operator + (double2x2 val) { return new double2x2 (+val.c0, +val.c1); }


        /// <summary>Returns the result of a componentwise equality operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator == (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 == rhs.c0, lhs.c1 == rhs.c1); }

        /// <summary>Returns the result of a componentwise equality operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator == (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 == rhs, lhs.c1 == rhs); }

        /// <summary>Returns the result of a componentwise equality operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator == (double lhs, double2x2 rhs) { return new bool2x2 (lhs == rhs.c0, lhs == rhs.c1); }


        /// <summary>Returns the result of a componentwise not equal operation on two double2x2 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator != (double2x2 lhs, double2x2 rhs) { return new bool2x2 (lhs.c0 != rhs.c0, lhs.c1 != rhs.c1); }

        /// <summary>Returns the result of a componentwise not equal operation on a double2x2 matrix and a double value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator != (double2x2 lhs, double rhs) { return new bool2x2 (lhs.c0 != rhs, lhs.c1 != rhs); }

        /// <summary>Returns the result of a componentwise not equal operation on a double value and a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2x2 operator != (double lhs, double2x2 rhs) { return new bool2x2 (lhs != rhs.c0, lhs != rhs.c1); }



        /// <summary>Returns the double2 element at a specified index.</summary>
        unsafe public double2 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (double2x2* array = &this) { return ((double2*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (double2* array = &c0) { array[index] = value; }
            }
        }

        /// <summary>Returns true if the double2x2 is equal to a given double2x2, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(double2x2 rhs) { return c0.Equals(rhs.c0) && c1.Equals(rhs.c1); }

        /// <summary>Returns true if the double2x2 is equal to a given double2x2, false otherwise.</summary>
        public override bool Equals(object o) { return Equals((double2x2)o); }


        /// <summary>Returns a hash code for the double2x2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)math.hash(this); }


        /// <summary>Returns a string representation of the double2x2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("double2x2({0}, {1},  {2}, {3})", c0.x, c1.x, c0.y, c1.y);
        }

        /// <summary>Returns a string representation of the double2x2 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("double2x2({0}, {1},  {2}, {3})", c0.x.ToString(format, formatProvider), c1.x.ToString(format, formatProvider), c0.y.ToString(format, formatProvider), c1.y.ToString(format, formatProvider));
        }

    }

    public static partial class math
    {
        /// <summary>Returns a double2x2 matrix constructed from two double2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(double2 c0, double2 c1) { return new double2x2(c0, c1); }

        /// <summary>Returns a double2x2 matrix constructed from from 4 double values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(double m00, double m01,
                                          double m10, double m11)
        {
            return new double2x2(m00, m01,
                                 m10, m11);
        }

        /// <summary>Returns a double2x2 matrix constructed from a single double value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(double v) { return new double2x2(v); }

        /// <summary>Returns a double2x2 matrix constructed from a single bool value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(bool v) { return new double2x2(v); }

        /// <summary>Return a double2x2 matrix constructed from a bool2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(bool2x2 v) { return new double2x2(v); }

        /// <summary>Returns a double2x2 matrix constructed from a single int value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(int v) { return new double2x2(v); }

        /// <summary>Return a double2x2 matrix constructed from a int2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(int2x2 v) { return new double2x2(v); }

        /// <summary>Returns a double2x2 matrix constructed from a single uint value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(uint v) { return new double2x2(v); }

        /// <summary>Return a double2x2 matrix constructed from a uint2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(uint2x2 v) { return new double2x2(v); }

        /// <summary>Returns a double2x2 matrix constructed from a single float value by converting it to double and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(float v) { return new double2x2(v); }

        /// <summary>Return a double2x2 matrix constructed from a float2x2 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 double2x2(float2x2 v) { return new double2x2(v); }

        /// <summary>Return the double2x2 transpose of a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 transpose(double2x2 v)
        {
            return double2x2(
                v.c0.x, v.c0.y,
                v.c1.x, v.c1.y);
        }

        /// <summary>Returns the double2x2 full inverse of a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2x2 inverse(double2x2 m)
        {
            double a = m.c0.x;
            double b = m.c1.x;
            double c = m.c0.y;
            double d = m.c1.y;

            double det = a * d - b * c;

            return double2x2(d, -b, -c, a) * (1.0 / det);
        }

        /// <summary>Returns the determinant of a double2x2 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double determinant(double2x2 m)
        {
            double a = m.c0.x;
            double b = m.c1.x;
            double c = m.c0.y;
            double d = m.c1.y;

            return a * d - b * c;
        }

        /// <summary>Returns a uint hash code of a double2x2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(double2x2 v)
        {
            return csum(fold_to_uint(v.c0) * uint2(0xA47EC335u, 0xA477DF57u) + 
                        fold_to_uint(v.c1) * uint2(0xC4B1493Fu, 0xBA0966D3u)) + 0xAFBEE253u;
        }

        /// <summary>
        /// Returns a uint2 vector hash code of a double2x2 vector.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 hashwide(double2x2 v)
        {
            return (fold_to_uint(v.c0) * uint2(0x5B419C01u, 0x515D90F5u) + 
                    fold_to_uint(v.c1) * uint2(0xEC9F68F3u, 0xF9EA92D5u)) + 0xC2FAFCB9u;
        }

    }
}
