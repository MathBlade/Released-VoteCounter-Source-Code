// GENERATED CODE
using System;
using System.Runtime.CompilerServices;

#pragma warning disable 0660, 0661

namespace Unity.Mathematics
{
    [System.Serializable]
    public partial struct float4x3 : System.IEquatable<float4x3>, IFormattable
    {
        public float4 c0;
        public float4 c1;
        public float4 c2;

        /// <summary>float4x3 zero value.</summary>
        public static readonly float4x3 zero = new float4x3(0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.0f);


        /// <summary>Constructs a float4x3 matrix from three float4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(float4 c0, float4 c1, float4 c2)
        { 
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
        }

        /// <summary>Constructs a float4x3 matrix from 12 float values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(float m00, float m01, float m02,
                        float m10, float m11, float m12,
                        float m20, float m21, float m22,
                        float m30, float m31, float m32)
        { 
            this.c0 = new float4(m00, m10, m20, m30);
            this.c1 = new float4(m01, m11, m21, m31);
            this.c2 = new float4(m02, m12, m22, m32);
        }

        /// <summary>Constructs a float4x3 matrix from a single float value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(float v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
        }

        /// <summary>Constructs a float4x3 matrix from a single bool value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(bool v)
        {
            this.c0 = math.select(new float4(0.0f), new float4(1.0f), v);
            this.c1 = math.select(new float4(0.0f), new float4(1.0f), v);
            this.c2 = math.select(new float4(0.0f), new float4(1.0f), v);
        }

        /// <summary>Constructs a float4x3 matrix from a bool4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(bool4x3 v)
        {
            this.c0 = math.select(new float4(0.0f), new float4(1.0f), v.c0);
            this.c1 = math.select(new float4(0.0f), new float4(1.0f), v.c1);
            this.c2 = math.select(new float4(0.0f), new float4(1.0f), v.c2);
        }

        /// <summary>Constructs a float4x3 matrix from a single int value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(int v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
        }

        /// <summary>Constructs a float4x3 matrix from a int4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(int4x3 v)
        {
            this.c0 = v.c0;
            this.c1 = v.c1;
            this.c2 = v.c2;
        }

        /// <summary>Constructs a float4x3 matrix from a single uint value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(uint v)
        {
            this.c0 = v;
            this.c1 = v;
            this.c2 = v;
        }

        /// <summary>Constructs a float4x3 matrix from a uint4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(uint4x3 v)
        {
            this.c0 = v.c0;
            this.c1 = v.c1;
            this.c2 = v.c2;
        }

        /// <summary>Constructs a float4x3 matrix from a single double value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(double v)
        {
            this.c0 = (float4)v;
            this.c1 = (float4)v;
            this.c2 = (float4)v;
        }

        /// <summary>Constructs a float4x3 matrix from a double4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float4x3(double4x3 v)
        {
            this.c0 = (float4)v.c0;
            this.c1 = (float4)v.c1;
            this.c2 = (float4)v.c2;
        }


        /// <summary>Implicitly converts a single float value to a float4x3 matrix by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x3(float v) { return new float4x3(v); }

        /// <summary>Explicitly converts a single bool value to a float4x3 matrix by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float4x3(bool v) { return new float4x3(v); }

        /// <summary>Explicitly converts a bool4x3 matrix to a float4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float4x3(bool4x3 v) { return new float4x3(v); }

        /// <summary>Implicitly converts a single int value to a float4x3 matrix by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x3(int v) { return new float4x3(v); }

        /// <summary>Implicitly converts a int4x3 matrix to a float4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x3(int4x3 v) { return new float4x3(v); }

        /// <summary>Implicitly converts a single uint value to a float4x3 matrix by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x3(uint v) { return new float4x3(v); }

        /// <summary>Implicitly converts a uint4x3 matrix to a float4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4x3(uint4x3 v) { return new float4x3(v); }

        /// <summary>Explicitly converts a single double value to a float4x3 matrix by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float4x3(double v) { return new float4x3(v); }

        /// <summary>Explicitly converts a double4x3 matrix to a float4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float4x3(double4x3 v) { return new float4x3(v); }


        /// <summary>Returns the result of a componentwise multiplication operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator * (float4x3 lhs, float4x3 rhs) { return new float4x3 (lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2); }

        /// <summary>Returns the result of a componentwise multiplication operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator * (float4x3 lhs, float rhs) { return new float4x3 (lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs); }

        /// <summary>Returns the result of a componentwise multiplication operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator * (float lhs, float4x3 rhs) { return new float4x3 (lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2); }


        /// <summary>Returns the result of a componentwise addition operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator + (float4x3 lhs, float4x3 rhs) { return new float4x3 (lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2); }

        /// <summary>Returns the result of a componentwise addition operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator + (float4x3 lhs, float rhs) { return new float4x3 (lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs); }

        /// <summary>Returns the result of a componentwise addition operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator + (float lhs, float4x3 rhs) { return new float4x3 (lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2); }


        /// <summary>Returns the result of a componentwise subtraction operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator - (float4x3 lhs, float4x3 rhs) { return new float4x3 (lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2); }

        /// <summary>Returns the result of a componentwise subtraction operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator - (float4x3 lhs, float rhs) { return new float4x3 (lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs); }

        /// <summary>Returns the result of a componentwise subtraction operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator - (float lhs, float4x3 rhs) { return new float4x3 (lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2); }


        /// <summary>Returns the result of a componentwise division operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator / (float4x3 lhs, float4x3 rhs) { return new float4x3 (lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2); }

        /// <summary>Returns the result of a componentwise division operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator / (float4x3 lhs, float rhs) { return new float4x3 (lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs); }

        /// <summary>Returns the result of a componentwise division operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator / (float lhs, float4x3 rhs) { return new float4x3 (lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2); }


        /// <summary>Returns the result of a componentwise modulus operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator % (float4x3 lhs, float4x3 rhs) { return new float4x3 (lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2); }

        /// <summary>Returns the result of a componentwise modulus operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator % (float4x3 lhs, float rhs) { return new float4x3 (lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator % (float lhs, float4x3 rhs) { return new float4x3 (lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2); }


        /// <summary>Returns the result of a componentwise increment operation on a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator ++ (float4x3 val) { return new float4x3 (++val.c0, ++val.c1, ++val.c2); }


        /// <summary>Returns the result of a componentwise decrement operation on a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator -- (float4x3 val) { return new float4x3 (--val.c0, --val.c1, --val.c2); }


        /// <summary>Returns the result of a componentwise less than operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator < (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2); }

        /// <summary>Returns the result of a componentwise less than operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator < (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs); }

        /// <summary>Returns the result of a componentwise less than operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator < (float lhs, float4x3 rhs) { return new bool4x3 (lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2); }


        /// <summary>Returns the result of a componentwise less or equal operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator <= (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2); }

        /// <summary>Returns the result of a componentwise less or equal operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator <= (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs); }

        /// <summary>Returns the result of a componentwise less or equal operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator <= (float lhs, float4x3 rhs) { return new bool4x3 (lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2); }


        /// <summary>Returns the result of a componentwise greater than operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator > (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2); }

        /// <summary>Returns the result of a componentwise greater than operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator > (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs); }

        /// <summary>Returns the result of a componentwise greater than operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator > (float lhs, float4x3 rhs) { return new bool4x3 (lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2); }


        /// <summary>Returns the result of a componentwise greater or equal operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator >= (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator >= (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator >= (float lhs, float4x3 rhs) { return new bool4x3 (lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2); }


        /// <summary>Returns the result of a componentwise unary minus operation on a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator - (float4x3 val) { return new float4x3 (-val.c0, -val.c1, -val.c2); }


        /// <summary>Returns the result of a componentwise unary plus operation on a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 operator + (float4x3 val) { return new float4x3 (+val.c0, +val.c1, +val.c2); }


        /// <summary>Returns the result of a componentwise equality operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator == (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2); }

        /// <summary>Returns the result of a componentwise equality operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator == (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs); }

        /// <summary>Returns the result of a componentwise equality operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator == (float lhs, float4x3 rhs) { return new bool4x3 (lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2); }


        /// <summary>Returns the result of a componentwise not equal operation on two float4x3 matrices.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator != (float4x3 lhs, float4x3 rhs) { return new bool4x3 (lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2); }

        /// <summary>Returns the result of a componentwise not equal operation on a float4x3 matrix and a float value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator != (float4x3 lhs, float rhs) { return new bool4x3 (lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs); }

        /// <summary>Returns the result of a componentwise not equal operation on a float value and a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x3 operator != (float lhs, float4x3 rhs) { return new bool4x3 (lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2); }



        /// <summary>Returns the float4 element at a specified index.</summary>
        unsafe public float4 this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (float4x3* array = &this) { return ((float4*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 3)
                    throw new System.ArgumentException("index must be between[0...2]");
#endif
                fixed (float4* array = &c0) { array[index] = value; }
            }
        }

        /// <summary>Returns true if the float4x3 is equal to a given float4x3, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(float4x3 rhs) { return c0.Equals(rhs.c0) && c1.Equals(rhs.c1) && c2.Equals(rhs.c2); }

        /// <summary>Returns true if the float4x3 is equal to a given float4x3, false otherwise.</summary>
        public override bool Equals(object o) { return Equals((float4x3)o); }


        /// <summary>Returns a hash code for the float4x3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)math.hash(this); }


        /// <summary>Returns a string representation of the float4x3.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", c0.x, c1.x, c2.x, c0.y, c1.y, c2.y, c0.z, c1.z, c2.z, c0.w, c1.w, c2.w);
        }

        /// <summary>Returns a string representation of the float4x3 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", c0.x.ToString(format, formatProvider), c1.x.ToString(format, formatProvider), c2.x.ToString(format, formatProvider), c0.y.ToString(format, formatProvider), c1.y.ToString(format, formatProvider), c2.y.ToString(format, formatProvider), c0.z.ToString(format, formatProvider), c1.z.ToString(format, formatProvider), c2.z.ToString(format, formatProvider), c0.w.ToString(format, formatProvider), c1.w.ToString(format, formatProvider), c2.w.ToString(format, formatProvider));
        }

    }

    public static partial class math
    {
        /// <summary>Returns a float4x3 matrix constructed from three float4 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(float4 c0, float4 c1, float4 c2) { return new float4x3(c0, c1, c2); }

        /// <summary>Returns a float4x3 matrix constructed from from 12 float values given in row-major order.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(float m00, float m01, float m02,
                                        float m10, float m11, float m12,
                                        float m20, float m21, float m22,
                                        float m30, float m31, float m32)
        {
            return new float4x3(m00, m01, m02,
                                m10, m11, m12,
                                m20, m21, m22,
                                m30, m31, m32);
        }

        /// <summary>Returns a float4x3 matrix constructed from a single float value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(float v) { return new float4x3(v); }

        /// <summary>Returns a float4x3 matrix constructed from a single bool value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(bool v) { return new float4x3(v); }

        /// <summary>Return a float4x3 matrix constructed from a bool4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(bool4x3 v) { return new float4x3(v); }

        /// <summary>Returns a float4x3 matrix constructed from a single int value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(int v) { return new float4x3(v); }

        /// <summary>Return a float4x3 matrix constructed from a int4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(int4x3 v) { return new float4x3(v); }

        /// <summary>Returns a float4x3 matrix constructed from a single uint value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(uint v) { return new float4x3(v); }

        /// <summary>Return a float4x3 matrix constructed from a uint4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(uint4x3 v) { return new float4x3(v); }

        /// <summary>Returns a float4x3 matrix constructed from a single double value by converting it to float and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(double v) { return new float4x3(v); }

        /// <summary>Return a float4x3 matrix constructed from a double4x3 matrix by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x3 float4x3(double4x3 v) { return new float4x3(v); }

        /// <summary>Return the float3x4 transpose of a float4x3 matrix.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 transpose(float4x3 v)
        {
            return float3x4(
                v.c0.x, v.c0.y, v.c0.z, v.c0.w,
                v.c1.x, v.c1.y, v.c1.z, v.c1.w,
                v.c2.x, v.c2.y, v.c2.z, v.c2.w);
        }

        /// <summary>Returns a uint hash code of a float4x3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(float4x3 v)
        {
            return csum(asuint(v.c0) * uint4(0xFE31134Fu, 0x712A34D7u, 0x9D77A59Bu, 0x4942CA39u) + 
                        asuint(v.c1) * uint4(0xB40EC62Du, 0x565ED63Fu, 0x93C30C2Bu, 0xDCAF0351u) + 
                        asuint(v.c2) * uint4(0x6E050B01u, 0x750FDBF5u, 0x7F3DD499u, 0x52EAAEBBu)) + 0x4599C793u;
        }

        /// <summary>
        /// Returns a uint4 vector hash code of a float4x3 vector.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint4 hashwide(float4x3 v)
        {
            return (asuint(v.c0) * uint4(0x83B5E729u, 0xC267163Fu, 0x67BC9149u, 0xAD7C5EC1u) + 
                    asuint(v.c1) * uint4(0x822A7D6Du, 0xB492BF15u, 0xD37220E3u, 0x7AA2C2BDu) + 
                    asuint(v.c2) * uint4(0xE16BC89Du, 0x7AA07CD3u, 0xAF642BA9u, 0xA8F2213Bu)) + 0x9F3FDC37u;
        }

    }
}
