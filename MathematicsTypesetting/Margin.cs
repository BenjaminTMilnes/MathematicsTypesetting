using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Margin
    {
        public Length Top { get; set; }
        public Length Right { get; set; }
        public Length Bottom { get; set; }
        public Length Left { get; set; }

        public Margin()
        {
            Top = 0;
            Right = 0;
            Bottom = 0;
            Left = 0;
        }

        public Margin(double topRightBottomLeft)
        {
            Top = topRightBottomLeft;
            Right = topRightBottomLeft;
            Bottom = topRightBottomLeft;
            Left = topRightBottomLeft;
        }

        public static implicit operator Margin(double topRightBottomLeft)
        {
            return new Margin(topRightBottomLeft);
        }
    }
}
