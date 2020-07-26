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

        public float[] xyzw = new float[4];

        public Vector4()
        {
            xyzw[0] = 0;
            xyzw[1] = 0;
            xyzw[2] = 0;
            xyzw[3] = 0;

            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vector4(float X, float Y, float Z, float W)
        {
            xyzw[0] = X;
            xyzw[1] = Y;
            xyzw[2] = Z;
            xyzw[3] = W;

            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        //Functions
        public void UpdatePoints()
        {
            x = xyzw[0];
            y = xyzw[1];
            z = xyzw[2];
            w = xyzw[3];
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
            xyzw = vector.xyzw;
            UpdatePoints();
        }

        //Overloads
        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            Vector4 v3 = new Vector4();

            for (int y = 0; y < 4; y++)
            {
                v3.xyzw[y] = v1.xyzw[y] + v2.xyzw[y];
            }
            v3.UpdatePoints();
            return v3;
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            Vector4 v3 = new Vector4();

            for (int y = 0; y < 4; y++)
            {
                v3.xyzw[y] = v1.xyzw[y] - v2.xyzw[y];
            }
            v3.UpdatePoints();
            return v3;
        }

        public static Vector4 operator *(Vector4 v1, float scaler)
        {

            for (int i = 0; i < 4; i++)
            {
                v1.xyzw[i] *= scaler;
            }
            v1.UpdatePoints();
            return v1;
        }

        public static Vector4 operator *(float scaler, Vector4 v1)
        {
            return v1 * scaler;
        }

        public static Vector4 operator /(Vector4 v1, float scaler)
        {

            for (int i = 0; i < 4; i++)
            {
                v1.xyzw[i] = v1.xyzw[i] / scaler;
            }
            v1.UpdatePoints();
            return v1;
        }

        public static Vector4 operator /(float scaler, Vector4 v1)
        {
            return v1 * scaler;
        }


        //treat as vector4 = vector3
        public static Vector4 operator ^(Vector4 v1, Vector3 v3)
        {

            for (int i = 0; i < 3; i++)
            {
                v1.xyzw[i] = v3.xyz[i];
            }
            v1.xyzw[3] = 0;
            v1.UpdatePoints();
            return v1;
        }


        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            Vector4 newVector4 = new Vector4();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; ++y)
                {
                    newVector4.xyzw[x] = newVector4.xyzw[x] + lhs.Axis[y].xyzw[x] * rhs.xyzw[y];
                }
            }
            newVector4.UpdatePoints();
            return newVector4;
        }



    }
}
