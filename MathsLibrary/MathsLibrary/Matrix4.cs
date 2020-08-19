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

        public Matrix4()
        {
            m1 = 1; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = 1; m7 = 0; m8 = 0;
            m9 = 0; m10 = 0; m11 = 1; m12 = 0;
            m13 = 0; m14 = 0; m15 = 0; m16 = 1;
        }

        public Matrix4(float Xx, float Xy, float Xz, float Xw, float Yx, float Yy, float Yz, float Yw, float Zx, float Zy, float Zz, float Zw, float Wx, float Wy, float Wz, float Ww)
        {
            m1 = Xx;
            m2 = Xy;
            m3 = Xz;
            m4 = Xw;

            m5 = Yx;
            m6 = Yy;
            m7 = Yz;
            m8 = Yw;

            m9 = Zx;
            m10 = Zy;
            m11 = Zz;
            m12 = Zw;

            m13 = Wx;
            m14 = Wy;
            m15 = Wz;
            m16 = Ww;
        }

        public Matrix4(Vector4 x, Vector4 y, Vector4 z, Vector4 w)
        {
            m1 = x.x;
            m2 = x.y;
            m3 = x.z;
            m4 = x.w;

            m5 = y.x;
            m6 = y.y;
            m7 = y.z;
            m8 = y.w;

            m9 = z.x;
            m10 = z.y;
            m11 = z.z;
            m12 = z.w;

            m13 = w.x;
            m14 = w.y;
            m15 = w.z;
            m16 = w.w;
        }

        public void SetRotateX(float rotation)
        {
            m6 = (float)Math.Cos(rotation);
            m7 = (float)Math.Sin(rotation);

            m10 = (float)-Math.Sin(rotation);
            m11 = (float)Math.Cos(rotation);

        }

        public void SetRotateY(float rotation)
        {
            m1 = (float)Math.Cos(rotation);
            m3 = (float)-Math.Sin(rotation);

            m9 = (float)Math.Sin(rotation);
            m11 = (float)Math.Cos(rotation);
        }

        public void SetRotateZ(float rotation)
        {
            m1 = (float)Math.Cos(rotation);
            m2 = (float)Math.Sin(rotation);

            m5 = (float)-Math.Sin(rotation);
            m6 = (float)Math.Cos(rotation);
        }


        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            Matrix4 multiMatrix = new Matrix4();

            multiMatrix.m1 = new Vector4(lhs.m1, lhs.m5, lhs.m9, lhs.m13).Dot(new Vector4(rhs.m1, rhs.m2, rhs.m3, rhs.m4));
            multiMatrix.m2 = new Vector4(lhs.m2, lhs.m6, lhs.m10, lhs.m14).Dot(new Vector4(rhs.m1, rhs.m2, rhs.m3, rhs.m4));
            multiMatrix.m3 = new Vector4(lhs.m3, lhs.m7, lhs.m11, lhs.m15).Dot(new Vector4(rhs.m1, rhs.m2, rhs.m3, rhs.m4));
            multiMatrix.m4 = new Vector4(lhs.m4, lhs.m8, lhs.m12, lhs.m16).Dot(new Vector4(rhs.m1, rhs.m2, rhs.m3, rhs.m4));


            multiMatrix.m5 = new Vector4(lhs.m1, lhs.m5, lhs.m9, lhs.m13).Dot(new Vector4(rhs.m5, rhs.m6, rhs.m7, rhs.m8));
            multiMatrix.m6 = new Vector4(lhs.m2, lhs.m6, lhs.m10, lhs.m14).Dot(new Vector4(rhs.m5, rhs.m6, rhs.m7, rhs.m8));
            multiMatrix.m7 = new Vector4(lhs.m3, lhs.m7, lhs.m11, lhs.m15).Dot(new Vector4(rhs.m5, rhs.m6, rhs.m7, rhs.m8));
            multiMatrix.m8 = new Vector4(lhs.m4, lhs.m8, lhs.m12, lhs.m16).Dot(new Vector4(rhs.m5, rhs.m6, rhs.m7, rhs.m8));


            multiMatrix.m9 = new Vector4(lhs.m1, lhs.m5, lhs.m9, lhs.m13).Dot(new Vector4(rhs.m9, rhs.m10, rhs.m11, rhs.m12));
            multiMatrix.m10 = new Vector4(lhs.m2, lhs.m6, lhs.m10, lhs.m14).Dot(new Vector4(rhs.m9, rhs.m10, rhs.m11, rhs.m12));
            multiMatrix.m11 = new Vector4(lhs.m3, lhs.m7, lhs.m11, lhs.m15).Dot(new Vector4(rhs.m9, rhs.m10, rhs.m11, rhs.m12));
            multiMatrix.m12 = new Vector4(lhs.m4, lhs.m8, lhs.m12, lhs.m16).Dot(new Vector4(rhs.m9, rhs.m10, rhs.m11, rhs.m12));

            multiMatrix.m13 = new Vector4(lhs.m1, lhs.m5, lhs.m9, lhs.m13).Dot(new Vector4(rhs.m13, rhs.m14, rhs.m15, rhs.m16));
            multiMatrix.m14 = new Vector4(lhs.m2, lhs.m6, lhs.m10, lhs.m14).Dot(new Vector4(rhs.m13, rhs.m14, rhs.m15, rhs.m16));
            multiMatrix.m15 = new Vector4(lhs.m3, lhs.m7, lhs.m11, lhs.m15).Dot(new Vector4(rhs.m13, rhs.m14, rhs.m15, rhs.m16));
            multiMatrix.m16 = new Vector4(lhs.m4, lhs.m8, lhs.m12, lhs.m16).Dot(new Vector4(rhs.m13, rhs.m14, rhs.m15, rhs.m16));

            return multiMatrix;
        }

    }

}

