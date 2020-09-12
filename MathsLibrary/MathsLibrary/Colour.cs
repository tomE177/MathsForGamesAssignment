using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibrary
{
    public class Colour
    {
        public UInt32 colour;

        public Colour()
        {
            colour = 0;
        }

        public Colour(byte red, byte green, byte blue, byte alpha)
        {
            SetRed(red);
            SetGreen(green);
            SetBlue(blue);
            SetAlpha(alpha);
        }

        //get the red value
        public byte GetRed()
        {
            UInt32 value = colour & 0xff000000;
            return (byte)((colour & 0xff000000) >> 24);
        }

        //set the red value
        public void SetRed(byte red)
        {
            colour = colour & 0x00ffffff;

            colour |= (UInt32)red << 24;
        }

        //get the green value
        public byte GetGreen()
        {
            UInt32 value = colour & 0x00ff0000;
            return (byte)((colour & 0x00ff0000) >> 16);
        }

        //set the green value
        public void SetGreen(byte green)
        {
            colour = colour & 0xff00ffff;

            colour |= (UInt32)green << 16;
        }

        //get the blue value
        public byte GetBlue()
        {
            UInt32 value = colour & 0x0000ff00;
            return (byte)((colour & 0x0000ff00) >> 8);
        }

        //set the blue value
        public void SetBlue(byte blue)
        {
            colour = colour & 0xffff00ff;

            colour |= (UInt32)blue << 8;
        }

        //get the alpha value
        public byte GetAlpha()
        {
            UInt32 value = colour & 0x000000ff;
            return (byte)((colour & 0x000000ff) >> 0);
        }

        //set the alpha value
        public void SetAlpha(byte alpha)
        {
            colour = colour & 0xffffff00;

            colour |= (UInt32)alpha << 0;
        }
    }
}
