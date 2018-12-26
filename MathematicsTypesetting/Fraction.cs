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

        public Length FractionBarWidth { get; set; }

        public Colour BackgroundColour { get; set; }

        public Fraction()
        {
            FractionBarWidth = 1;

            OuterMargin.Left = 10;
            OuterMargin.Right = 10;

            BackgroundColour = new Colour();
        }

        public override void CascadeStyle(string name, string value)
        {
            base.CascadeStyle(name, value);

            Numerator.CascadeStyle(name, value);
            Denominator.CascadeStyle(name, value);
        }
    }
}
