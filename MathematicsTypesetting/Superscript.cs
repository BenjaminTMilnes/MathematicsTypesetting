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

        public Length TopExcess { get { return Element2.OuterHeight + SuperscriptOffset - Element1.OuterHeight; } }

        public Superscript()
        {
            SuperscriptOffset = new Length(60, LengthUnits.Arbitrary);
            SuperscriptScale = 0.7;
        }

        public override void CascadeStyle(string name, string value)
        {
            base.CascadeStyle(name, value);

            Element1.CascadeStyle(name, value);
            Element2.CascadeStyle(name, value);
        }
    }
}
