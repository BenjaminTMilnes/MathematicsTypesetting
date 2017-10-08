using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Number
    {
        public string Content { get; set; }

        public Length X { get; set; }
        public Length Y { get; set; }

        public Length Width { get; set; }
        public Length Height { get; set; }

        public Margin OuterMargin { get; set; }
        public Margin InnerMargin { get; set; }

        public Colour BackgroundColour { get; set; }

        public FontStyle FontStyle { get; set; }
    }
}
