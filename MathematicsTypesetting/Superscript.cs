using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Superscript : Element
    {
        public Element Element1 { get; set; }
        public Element Element2 { get; set; }

        public Length SuperscriptOffset { get; set; }
        public double SuperscriptScale { get; set; }

        public Superscript()
        {
            SuperscriptOffset = new Length(12, LengthUnits.Arbitrary);
            SuperscriptScale = 0.7;
        }
    }
}
