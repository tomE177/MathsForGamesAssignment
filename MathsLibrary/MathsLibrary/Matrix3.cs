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

        public Vector3[] Axis = new Vector3[3]; //xyz

        public Matrix3(Vector3 XAxis, Vector3 YAxis, Vector3 ZAxis)
        {
            Axis[0] = XAxis;
            Axis[1] = YAxis;
            Axis[2] = ZAxis;
            UpdateMFloats();
        }


        public Matrix3()
        {
            for (int i = 0; i < 3; ++i)
            {
                Axis[i] = new Vector3();
            }

            for (int i = 0; i < 3; ++i)
            {
                Axis[i].xyz[i] = 1;
            }

            UpdateMFloats();
        }

        void UpdateMFloats()
        {
            m1 = Axis[0].xyz[0];
            m4 = Axis[0].xyz[1];
            m7 = Axis[0].xyz[2];

            m2 = Axis[1].xyz[0];
            m5 = Axis[1].xyz[1];
            m8 = Axis[1].xyz[2];

            m3 = Axis[2].xyz[0];
            m6 = Axis[2].xyz[1];
            m9 = Axis[2].xyz[2];

            foreach (Vector3 v3 in Axis)
            {
                v3.UpdatePoints();
            }
        }


        public void RotateX(float rotation)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(rotation);
            m = this * m;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Axis[x].xyz[y] = m.Axis[x].xyz[y];
                }
            }
            UpdateMFloats();
        }

        public void RotateZ(float rotation)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(rotation);
            m = this * m;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Axis[x].xyz[y] = m.Axis[x].xyz[y];
                }
            }
            UpdateMFloats();
        }

        public void SetRotateX(float rotation)
        {
            Axis[1].xyz[1] = (float)Math.Cos(rotation);
            Axis[1].xyz[2] = (float)Math.Sin(rotation);

            Axis[2].xyz[1] = (float)-Math.Sin(rotation);
            Axis[2].xyz[2] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }

        public void SetRotateY(float rotation)
        {
            Axis[0].xyz[0] = (float)Math.Cos(rotation);
            Axis[0].xyz[2] = (float)-Math.Sin(rotation);

            Axis[2].xyz[0] = (float)Math.Sin(rotation);
            Axis[2].xyz[2] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }

        public void SetRotateZ(float rotation)
        {
            Axis[0].xyz[0] = (float)Math.Cos(rotation);
            Axis[0].xyz[1] = (float)Math.Sin(rotation);

            Axis[1].xyz[0] = (float)-Math.Sin(rotation);
            Axis[1].xyz[1] = (float)Math.Cos(rotation);
            UpdateMFloats();
        }


        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            Matrix3 multiMatrix = new Matrix3();

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    multiMatrix.Axis[x].xyz[y] = rhs.Axis[x].Dot(new Vector3(lhs.Axis[0].xyz[y], lhs.Axis[1].xyz[y], lhs.Axis[2].xyz[y]));
                }
            }

            multiMatrix.UpdateMFloats();
            return multiMatrix;
        }

        public void SetTranslation(float x, float y)
        {
            Axis[2].xyz[0] = x;
            Axis[2].xyz[1] = y;
            UpdateMFloats();
        }

        public void Translate(Vector3 vector3)
        {
            Vector3 vector = this * vector3;
            this.Axis[2].xyz[0] = vector.xyz[0];
            this.Axis[2].xyz[1] = vector.xyz[1];
            UpdateMFloats();
        }

    }
}