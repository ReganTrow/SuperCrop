using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCrop
{
    class Sliders
    {
        public Slider OriginX { get; set; }
        public Slider OriginY { get; set; }
        public Slider DimensionWidth { get; set; }
        public Slider DimensionHeight { get; set; }

        public Sliders(AspectRatio aspectRatio)
        {
            OriginX = new Slider(0,aspectRatio.Width*1000);
            OriginY = new Slider(0, aspectRatio.Height*1000);
            DimensionWidth = new Slider(0, aspectRatio.Width*1000);
            DimensionHeight = new Slider(0, aspectRatio.Height*1000);            
        }

    }
}
