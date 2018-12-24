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
        public FontEmphasis FontEmphasis { get; set; }

        public FontStyle()
        {
            FontName = "Times New Roman";
            FontHeight = new Length(50, LengthUnits.Points);
            FontColour = new Colour();
            FontEmphasis = FontEmphasis.None;
        }
    }
}
