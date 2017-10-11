using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Element
    {
        public Position Position { get; set; }

        public Size SizeOfContent { get; set; }
        public Size SizeIncludingInnerMargin { get; set; }
        public Size SizeIncludingBorder { get; set; }
        public Size SizeIncludingOuterMargin { get; set; }

        public Margin OuterMargin { get; set; }
        public Border Border { get; set; }
        public Margin InnerMargin { get; set; }

        public bool DrawConstructionLines { get; set; }

        public Element()
        {
            Position = new Position();

            SizeOfContent = new Size();
            SizeIncludingInnerMargin = new Size();
            SizeIncludingBorder = new Size();
            SizeIncludingOuterMargin = new Size();

            OuterMargin = new Margin();
            Border = new Border();
            InnerMargin = new Margin();

            DrawConstructionLines = false;
        }
    }
}
