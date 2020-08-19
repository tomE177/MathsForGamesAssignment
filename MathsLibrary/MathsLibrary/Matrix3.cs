using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibrary
{
    public class Matrix3
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }


        public Matrix3(float Xx, float Xy, float Xz, float Yx, float Yy, float Yz, float Zx, float Zy, float Zz)
        {
            m1 = Xx;
            m2 = Xy;
            m3 = Xz;

            m4 = Yx;
            m5 = Yy;
            m6 = Yz;

            m7 = Zx;
            m8 = Zy;
            m9 = Zz;
        }

        public Matrix3(Vector3 x, Vector3 y, Vector3 z)
        {
            m1 = x.x;
            m2 = x.y;
            m3 = x.z;

            m4 = y.x;
            m5 = y.y;
            m6 = y.z;

            m7 = z.x;
            m8 = z.y;
            m9 = z.z;
        }


        public void RotateX(float rotation)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(rotation);
            m = this * m;

            m1 = m.m1;
            m2 = m.m2;
            m3 = m.m3;

            m4 = m.m4;
            m5 = m.m5;
            m6 = m.m6;

            m7 = m.m7;
            m8 = m.m8;
            m9 = m.m9;

        }

        public void RotateZ(float rotation)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(rotation);
            m = this * m;

            m1 = m.m1;
            m2 = m.m2;
            m3 = m.m3;

            m4 = m.m4;
            m5 = m.m5;
            m6 = m.m6;

            m7 = m.m7;
            m8 = m.m8;
            m9 = m.m9;
        }


        public void SetRotateX(float rotation)
        {
            m5 = (float)Math.Cos(rotation);
            m6 = (float)Math.Sin(rotation);

            m8 = (float)-Math.Sin(rotation);
            m9 = (float)Math.Cos(rotation);

        }

        public void SetRotateY(float rotation)
        {
            m1 = (float)Math.Cos(rotation);
            m3 = (float)-Math.Sin(rotation);

            m7 = (float)Math.Sin(rotation);
            m9 = (float)Math.Cos(rotation);
        }

        public void SetRotateZ(float rotation)
        {
            m1 = (float)Math.Cos(rotation);
            m2 = (float)Math.Sin(rotation);

            m4 = (float)-Math.Sin(rotation);
            m5 = (float)Math.Cos(rotation);
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            Matrix3 multiMatrix = new Matrix3();

            multiMatrix.m1 = new Vector3(lhs.m1, lhs.m4, lhs.m7).Dot(new Vector3(rhs.m1, rhs.m2, rhs.m3));
            multiMatrix.m2 = new Vector3(lhs.m2, lhs.m5, lhs.m8).Dot(new Vector3(rhs.m1, rhs.m2, rhs.m3));
            multiMatrix.m3 = new Vector3(lhs.m3, lhs.m6, lhs.m9).Dot(new Vector3(rhs.m1, rhs.m2, rhs.m3));


            multiMatrix.m4 = new Vector3(lhs.m1, lhs.m4, lhs.m7).Dot(new Vector3(rhs.m4, rhs.m5, rhs.m6));
            multiMatrix.m5 = new Vector3(lhs.m2, lhs.m5, lhs.m8).Dot(new Vector3(rhs.m4, rhs.m5, rhs.m6));
            multiMatrix.m6 = new Vector3(lhs.m3, lhs.m6, lhs.m9).Dot(new Vector3(rhs.m4, rhs.m5, rhs.m6));


            multiMatrix.m7 = new Vector3(lhs.m1, lhs.m4, lhs.m7).Dot(new Vector3(rhs.m7, rhs.m8, rhs.m9));
            multiMatrix.m8 = new Vector3(lhs.m2, lhs.m5, lhs.m8).Dot(new Vector3(rhs.m7, rhs.m8, rhs.m9));
            multiMatrix.m9 = new Vector3(lhs.m3, lhs.m6, lhs.m9).Dot(new Vector3(rhs.m7, rhs.m8, rhs.m9));

            return multiMatrix;
        }

        public void SetTranslation(float x, float y)
        {
            m7 = x;
            m8 = y;
        }

        public void Translate(Vector3 vector3)
        {
            Vector3 vector = this * vector3;

            m7 = vector.x;
            m8 = vector.y;
        }

    }
}