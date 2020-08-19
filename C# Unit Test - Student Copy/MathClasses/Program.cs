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
            Matrix3 m3a = new Matrix3();
            m3a.SetRotateX(3.98f);

            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(9.62f);

            Matrix3 m3d = m3a * m3c;
            Console.ReadKey();
        }
    }
}
