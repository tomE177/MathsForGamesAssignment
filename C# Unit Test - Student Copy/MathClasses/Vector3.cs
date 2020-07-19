using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        //public float x, y, z;
        

        public float[] xyz = new float[3];


        public Vector3()
        {
            xyz[0] = 0;
            xyz[1] = 0;
            xyz[2] = 0;
        }

        public Vector3(float X, float Y, float Z)
        {
            xyz[0] = X;
            xyz[1] = Y;
            xyz[2] = Z;
        }

        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            Vector3 newVector3 = new Vector3();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; ++x)
                {
                    newVector3.xyz[y] = newVector3.xyz[y] + lhs.Axis[x].xyz[y] * rhs.xyz[x];
                }
            }

            return newVector3;
        }

    }
}
