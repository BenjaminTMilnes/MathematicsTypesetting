using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class FontStyle
    {
        public string FontName { get; set; }
        public Length FontHeight { get; set; }
        public Colour FontColour { get; set; }

        public FontStyle()
        {
            FontName = "Book Antiqua";
            FontHeight = new Length(10, LengthUnits.Points);
            FontColour = new Colour();
        }
    }
}
