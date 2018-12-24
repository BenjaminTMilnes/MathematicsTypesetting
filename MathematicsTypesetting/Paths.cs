using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MathematicsTypesetting
{
    public class Paths
    {
        public static System.Drawing.Drawing2D.GraphicsPath GetBracketPath(PointF offset,  float fontHeight = 50, string bracket = "(")
        {
            var sf = 2* fontHeight / 10;

            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddBezier(new PointF(2.0f * sf, 0.0f * sf), new PointF(1.0f * sf, 1.5f * sf), new PointF(0.2f * sf, 3.5f * sf), new PointF(0.0f * sf, 5.0f * sf));

            path.AddBezier(new PointF(0.0f * sf, 5.0f * sf), new PointF(1.0f * sf, 7.0f * sf), new PointF(2.0f * sf, 8.0f * sf), new PointF(3.0f * sf, 10.0f * sf));

            path.AddBezier(new PointF(3.1f * sf, 10.0f * sf), new PointF(2.1f * sf, 8.0f * sf), new PointF(1.2f * sf, 7.0f * sf), new PointF(0.3f * sf, 5.0f * sf));

            path.AddBezier(new PointF(0.3f * sf, 5.0f * sf), new PointF(1.2f * sf, 3.0f * sf), new PointF(2.1f * sf, 2.0f * sf), new PointF(3.1f * sf, 0.0f * sf));

            return path;
        }
    }
}
