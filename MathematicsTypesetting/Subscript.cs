using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Subscript : Element
    {
        public Element Element1 { get; set; }
        public Element Element2 { get; set; }

        public Length SubscriptOffset { get; set; }
        public double SubscriptScale { get; set; }

        public Subscript()
        {
            SubscriptOffset = new Length(30, LengthUnits.Arbitrary);
            SubscriptScale = 0.7;
        }
    }
}
