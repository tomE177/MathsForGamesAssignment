using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibrary
{
    public class Vector3
    {
        public float x, y, z;


        public float[] xyz = new float[3];


        public Vector3()
        {
            xyz[0] = 0;
            xyz[1] = 0;
            xyz[2] = 0;

            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float X, float Y, float Z)
        {
            xyz[0] = X;
            xyz[1] = Y;
            xyz[2] = Z;

            x = X;
            y = Y;
            z = Z;
        }

        //functions
        public void UpdatePoints()
        {
            x = xyz[0];
            y = xyz[1];
            z = xyz[2];
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
            xyz = vector.xyz;
            UpdatePoints();
        }


        //overloaders
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            Vector3 newVector3 = new Vector3();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; ++y)
                {
                    newVector3.xyz[x] = newVector3.xyz[x] + lhs.Axis[y].xyz[x] * rhs.xyz[y];
                }
            }
            newVector3.UpdatePoints();
            return newVector3;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();

            for (int y = 0; y < 3; y++)
            {
                v3.xyz[y] = v1.xyz[y] + v2.xyz[y];
            }
            v3.UpdatePoints();
            return v3;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3();

            for (int y = 0; y < 3; y++)
            {
                v3.xyz[y] = v1.xyz[y] - v2.xyz[y];
            }
            v3.UpdatePoints();
            return v3;
        }


        public static Vector3 operator *(Vector3 v1, float scaler)
        {

            for (int i = 0; i < 3; i++)
            {
                v1.xyz[i] *= scaler;
            }
            v1.UpdatePoints();
            return v1;
        }

        public static Vector3 operator *(float scaler, Vector3 v1)
        {
            return v1 * scaler;
        }


        public static Vector3 operator /(Vector3 v1, float scaler)
        {

            for (int i = 0; i < 3; i++)
            {
                v1.xyz[i] = v1.xyz[i] / scaler;
            }
            v1.UpdatePoints();
            return v1;
        }

        public static Vector3 operator /(float scaler, Vector3 v1)
        {
            return v1 * scaler;
        }

    }
}
