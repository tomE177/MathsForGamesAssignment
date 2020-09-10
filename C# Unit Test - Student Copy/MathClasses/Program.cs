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
            Colour colour = new Colour(94,0,0,0);
            colour.colour = colour.colour >> 8;
            //Colour colour = new Colour();
            Console.WriteLine(colour.GetRed());
            Console.WriteLine(colour.GetGreen());
            Console.WriteLine(colour.GetBlue());
            Console.WriteLine(colour.GetAlpha());
            Console.WriteLine(colour.colour.ToString());
            Console.ReadKey();
        }
    }
}
