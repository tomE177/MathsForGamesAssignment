using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            //Matrix3 m3a = new Matrix3();
            //m3a.SetRotateX(3.98f);

            //Matrix3 m3c = new Matrix3();
            //m3c.SetRotateZ(9.62f);

            //Matrix3 m3d = m3a * m3c;

            Matrix3 m3b = new Matrix3(new Vector3(1, 0, 55), new Vector3(0, 1, 44), new Vector3(0, 0, 1));

            Vector3 v3a = new Vector3(13.5f, -48.23f, 1);

            Vector3 v3b = m3b * v3a;

            Console.ReadKey();
        }
    }
}
