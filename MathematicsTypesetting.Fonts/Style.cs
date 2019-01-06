using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting.Fonts
{
    public class Style
    {
        public string Weight { get; set; }
        public IList<Glyph> Glyphs { get; set; }

        public Style()
        {
            Glyphs = new List<Glyph>();
        }
    }
}
