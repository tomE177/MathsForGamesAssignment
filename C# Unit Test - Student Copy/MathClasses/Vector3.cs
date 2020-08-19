using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public float Dot(Vector3 v3)
        {
            return x * v3.x + y * v3.y + z * v3.z;
        }

        public Vector3 Cross(Vector3 v3)
        {
            return new Vector3((y * v3.z - z * v3.y), (z * v3.x - x * v3.z), (x * v3.y - y * v3.x));
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        public void Normalize()
        {
            Vector3 vector = this / Magnitude();
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        //overloads
        public static Vector3 operator /(Vector3 v1, float scaler)
        {
            v1.x = v1.x / scaler;
            v1.y = v1.y / scaler;
            v1.z = v1.z / scaler;

            return v1;
        }

        public static Vector3 operator /(float scaler, Vector3 v1)
        {
            return v1 * scaler;
        }

        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            Vector3 newVector3 = new Vector3();
            newVector3.x = rhs.Dot(new Vector3(lhs.m1,lhs.m4, lhs.m7));
            newVector3.y = rhs.Dot(new Vector3(lhs.m2, lhs.m5, lhs.m8));
            newVector3.z = rhs.Dot(new Vector3(lhs.m3, lhs.m6, lhs.m9));

            return newVector3;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();

            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            v3.z = v1.z + v2.z;
            
            return v3;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();
            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            v3.z = v1.z - v2.z;

            return v3;
        }

        public static Vector3 operator *(Vector3 v1, float scaler)
        {
            v1.x *= scaler;
            v1.y *= scaler;
            v1.z *= scaler;

            return v1;
        }

        public static Vector3 operator *(float scaler, Vector3 v1)
        {
            return v1 * scaler;
        }
    }
}
