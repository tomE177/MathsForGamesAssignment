using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibrary
{
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vector4(float X, float Y, float Z, float W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }


        public float Dot(Vector4 v4)
        {
            return x * v4.x + y * v4.y + z * v4.z + w * v4.w;
        }

        public Vector4 Cross(Vector4 v3)
        {
            return new Vector4((y * v3.z - z * v3.y), (z * v3.x - x * v3.z), (x * v3.y - y * v3.x), 0);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
        }

        public void Normalize()
        {
            Vector4 vector = this / Magnitude();
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = vector.w;
        }

        //Overloads
        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            Vector4 v4 = new Vector4();

            v4.x = v1.x + v2.x;
            v4.y = v1.y + v2.y;
            v4.z = v1.z + v2.z;
            v4.w = v1.w + v2.w;

            return v4;
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            Vector4 v4 = new Vector4();

            v4.x = v1.x - v2.x;
            v4.y = v1.y - v2.y;
            v4.z = v1.z - v2.z;
            v4.w = v1.w - v2.w;

            return v4;
        }


        public static Vector4 operator *(Vector4 v1, float scaler)
        {
            v1.x *= scaler;
            v1.y *= scaler;
            v1.z *= scaler;
            v1.w *= scaler;

            return v1;
        }

        public static Vector4 operator *(float scaler, Vector4 v1)
        {
            return v1 * scaler;
        }


        public static Vector4 operator /(Vector4 v1, float scaler)
        {
            v1.x = v1.x / scaler;
            v1.y = v1.y / scaler;
            v1.z = v1.z / scaler;
            v1.w = v1.w / scaler;

            return v1;
        }

        public static Vector4 operator /(float scaler, Vector4 v1)
        {
            return v1 * scaler;
        }

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            Vector4 newVector4 = new Vector4();
            newVector4.x = rhs.Dot(new Vector4(lhs.m1, lhs.m5, lhs.m9, lhs.m13));
            newVector4.y = rhs.Dot(new Vector4(lhs.m2, lhs.m6, lhs.m10, lhs.m14));
            newVector4.z = rhs.Dot(new Vector4(lhs.m3, lhs.m7, lhs.m11, lhs.m15));
            newVector4.w = rhs.Dot(new Vector4(lhs.m4, lhs.m8, lhs.m12, lhs.m16));


            return newVector4;
        }

    }
}
