using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCrop
{
    class Slider
    {
        public int Value { get; set; }
        public int Maximum { get; set; }
        
        public Slider(int value, int maximum)
        {
            Value = value;
            Maximum = maximum;
        }
    }
}
