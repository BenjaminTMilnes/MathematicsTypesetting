using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Border
    {
        public Length Width { get; set; }
        public Colour Colour { get; set; }

        public Border()
        {
            Width = 0;
            Colour = new Colour();
        }
    }
}
