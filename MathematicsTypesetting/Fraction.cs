using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Fraction : Element
    {
        public Element Numerator { get; set; }
        public Element Denominator { get; set; }

        public Colour BackgroundColour { get; set; }

        public Fraction()
        {
            BackgroundColour = new Colour();
        }
    }
}
