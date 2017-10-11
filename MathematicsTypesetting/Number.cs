using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Number : Element
    {
        public string Content { get; set; }

        public Position Position { get; set; }

        public Size SizeOfContent { get; set; }
        public Size SizeIncludingInnerMargin { get; set; }
        public Size SizeIncludingBorder { get; set; }
        public Size SizeIncludingOuterMargin { get; set; }

        public Margin OuterMargin { get; set; }
        public Margin InnerMargin { get; set; }

        public Colour BackgroundColour { get; set; }

        public FontStyle FontStyle { get; set; }

        public bool DrawConstructionLines { get; set; }

        public Number()
        {
            Position = new Position();

            SizeOfContent = new Size();
            SizeIncludingInnerMargin = new Size();
            SizeIncludingBorder = new Size();
            SizeIncludingOuterMargin = new Size();

            OuterMargin = new Margin();
            InnerMargin = new Margin();

            BackgroundColour = new Colour();

            FontStyle = new FontStyle();

            DrawConstructionLines = false;
        }
    }
}
