using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {

        //public Matrix3()
        //{
        //    m1 = 1; m2 = 0; m3 = 0;
        //    m4 = 0; m5 = 1; m6 = 0;
        //    m7 = 0; m8 = 0; m9 = 1;
        //}
        public Vector3[] Axis = new Vector3[3]; //xyz

        public Matrix3(Vector3 XAxis, Vector3 YAxis, Vector3 ZAxis)
        {
            Axis[0] = XAxis;
            Axis[1] = YAxis;
            Axis[2] = ZAxis;
        }

        public Matrix3()
        {
            for (int i = 0; i < 3; ++i)
            {
                Axis[i] = new Vector3();
            }

            Axis[0].xyz[0] = 1;
            Axis[0].xyz[1] = 0;
            Axis[0].xyz[2] = 0;

            Axis[1].xyz[0] = 0;
            Axis[1].xyz[1] = 1;
            Axis[1].xyz[2] = 0;

            Axis[2].xyz[0] = 0;
            Axis[2].xyz[1] = 0;
            Axis[2].xyz[2] = 1;
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            Matrix3 multiMatrix = new Matrix3();

            for (int i = 0; i < 3; ++i)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        multiMatrix.Axis[y].xyz[i] = multiMatrix.Axis[y].xyz[i] + rhs.Axis[i].xyz[x] * lhs.Axis[x].xyz[y];
                    }
                }
            }


            return multiMatrix;
        }

    }
}