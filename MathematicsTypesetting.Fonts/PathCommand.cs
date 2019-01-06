﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting.Fonts
{
    public enum PathCommandType
    {
        MoveTo = 1,
        BezierCurveTo = 2,
        ClosePath = 3,
        LineTo = 4,
        HorizontalLineTo = 5,
        BezierSplineTo = 6,
        VerticalLineTo = 7
    }

    public class PathCommand
    {
        public PathCommandType Type { get; set; }
        public IList<float> Arguments { get; set; }
    }
}
