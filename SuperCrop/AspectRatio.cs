using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCrop
{
    class AspectRatio:IEquatable<AspectRatio>
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public AspectRatio(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public bool Equals(AspectRatio other)
        {
            return (Height == other.Height && Width == other.Width) ? true : false;
        }
    }
}
