using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MathematicsTypesetting
{
    public class Paths
    {
        public static GraphicsPath GetBracketPath(PointF offset, float fontHeight = 50, string bracket = "(")
        {
            var rf = 1;

            if (bracket == ")")
            {
                rf = -1;
            }

            var sfx = 1.5f * fontHeight / 30;
            var sfy = 1.5f * fontHeight / 16;

            var path = new GraphicsPath();

            var x1 = 1.5f * sfx * rf + offset.X;
            var x2 = 0.6f * sfx * rf + offset.X;
            var x3 = 0.05f * sfx * rf + offset.X;
            var x4 = 0.0f * sfx * rf + offset.X;

            var x5 = 1.9f * sfx * rf + offset.X;
            var x6 = 1.1f * sfx * rf + offset.X;
            var x7 = 0.7f * sfx * rf + offset.X;
            var x8 = 0.6f * sfx * rf + offset.X;

            var y1 = 0.0f * sfy + offset.Y;
            var y2 = 1.5f * sfy + offset.Y;
            var y3 = 3.5f * sfy + offset.Y;
            var y4 = 5.0f * sfy + offset.Y;

            var y5 = 10.0f * sfy - y4 + 2 * offset.Y;
            var y6 = 10.0f * sfy - y3 + 2 * offset.Y;
            var y7 = 10.0f * sfy - y2 + 2 * offset.Y;
            var y8 = 10.0f * sfy - y1 + 2 * offset.Y;

            path.AddBezier(new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4));

            path.AddBezier(new PointF(x4, y5), new PointF(x3, y6), new PointF(x2, y7), new PointF(x1, y8));

            path.AddBezier(new PointF(x5, y8), new PointF(x6, y7), new PointF(x7, y6), new PointF(x8, y5));

            path.AddBezier(new PointF(x8, y4), new PointF(x7, y3), new PointF(x6, y2), new PointF(x5, y1));

            return path;
        }

        public static Length GetBracketLength(float fontHeight = 50.0f)
        {
            return 4.0f * 1.9f * 1.5f * fontHeight / 30.0f;
        }
    }
}
