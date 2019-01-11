using System.Collections.Generic;

namespace MathematicsTypesetting.Fonts
{
    public enum PathCommandType
    {
        MoveTo = 1,
        LineTo = 2,
        HorizontalLineTo = 3,
        VerticalLineTo = 4,
        BezierCurveTo = 5,
        BezierSplineTo = 6,
        ClosePath = 7
    }

    public class PathCommand
    {
        public PathCommandType Type { get; set; }
        public IList<float> Arguments { get; set; }

        public PathCommand()
        {
            Arguments = new List<float>();
        }
    }
}
