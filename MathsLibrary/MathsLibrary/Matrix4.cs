using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibrary
{
    public class Matrix4
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16;

        public Vector4[] Axis = new Vector4[4]; //xyzw

        public Matrix4()
        {
            for (int i = 0; i < 4; ++i)
            {
                Axis[i] = new Vector4();
            }


            for (int i = 0; i < 4; ++i)
            {
                Axis[i].xyzw[i] = 1;
            }

            UpdateMFloats();
        }

        public Matrix4(Vector4 XAxis, Vector4 YAxis, Vector4 ZAxis, Vector4 WAxis)
        {
            Axis[0] = XAxis;
            Axis[1] = YAxis;
            Axis[2] = ZAxis;
            Axis[3] = WAxis;
            UpdateMFloats();
        }


        void UpdateMFloats()
        {
            m1 = Axis[0].xyzw[0];
            m5 = Axis[0].xyzw[1];
            m9 = Axis[0].xyzw[2];
            m13 = Axis[0].xyzw[3];

            m2 = Axis[1].xyzw[0];
            m5 = Axis[1].xyzw[1];
            m8 = Axis[1].xyzw[2];
            m14 = Axis[1].xyzw[3];

            m3 = Axis[2].xyzw[0];
            m6 = Axis[2].xyzw[1];
            m9 = Axis[2].xyzw[2];
            m15 = Axis[2].xyzw[3];

            m4 = Axis[3].xyzw[0];
            m8 = Axis[3].xyzw[1];
            m12 = Axis[3].xyzw[2];
            m16 = Axis[3].xyzw[3];

            foreach (Vector4 v3 in Axis)
            {
                v3.UpdatePoints();
            }
        }


        public void SetRotateX(float rotation)
        {
            Axis[1].xyzw[1] = (float)Math.Cos(rotation);
            Axis[1].xyzw[2] = (float)Math.Sin(rotation);

            Axis[2].xyzw[1] = (float)-Math.Sin(rotation);
            Axis[2].xyzw[2] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }

        public void SetRotateY(float rotation)
        {
            Axis[0].xyzw[0] = (float)Math.Cos(rotation);
            Axis[0].xyzw[2] = (float)-Math.Sin(rotation);

            Axis[2].xyzw[0] = (float)Math.Sin(rotation);
            Axis[2].xyzw[2] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }

        public void SetRotateZ(float rotation)
        {
            Axis[0].xyzw[0] = (float)Math.Cos(rotation);
            Axis[0].xyzw[1] = (float)Math.Sin(rotation);

            Axis[1].xyzw[0] = (float)-Math.Sin(rotation);
            Axis[1].xyzw[1] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }



        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            Matrix4 multiMatrix = new Matrix4();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    multiMatrix.Axis[x].xyzw[y] = rhs.Axis[x].Dot(new Vector4(lhs.Axis[0].xyzw[y], lhs.Axis[1].xyzw[y], lhs.Axis[2].xyzw[y], lhs.Axis[3].xyzw[y]));
                }
            }

            multiMatrix.UpdateMFloats();
            return multiMatrix;
        }

    }

}

