using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using MathematicsTypesetting.SVG;

namespace MathematicsTypesetting
{
             public class  SquareRootGlyph
    {
             public float L1 { get; set; }
            public float H1 { get; set; }
           public float W1 { get; set; }
           public float A1 { get; set; }

          public float L2 { get; set; }
          public float W2 { get; set; }
        public float A2 { get; set; }

        public float L3 { get; set; }
        public float W3 { get; set; }
        public float A3 { get; set; }

           public SquareRootGlyph(  float innerWidth = 10, float innerHeight = 2,  float baseWidth = 1)
        {
            L1 = innerWidth;
            H1 = innerHeight;

            A1 = 25.720f;
            A2 = 24.344f;
            A3 = 52.843f;

            W1 = baseWidth;
            W2 = 2 * baseWidth;
            W3 = 0.5f * baseWidth;

            L2 = 0.5f * H1;
            L3 = 0.25f * L2;
        }

            public GraphicsPath GetPath()
        {
            var p = new GraphicsPath();

            var x1 = 0.0f;
            var y1 = 0.0f;

            return p;
               }
    }


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

           public static GraphicsPath GetSquareRootPath(   Length innerHeight , Length innerWidth)
        {
            // True LaTeX square-root shape
            var svg = "M 9.64216 21.1929 L 5.27964 11.5508 C 5.10613 11.1542 4.9822 11.1542 4.90784 11.1542 C 4.88305 11.1542 4.75911 11.1542 4.48646 11.3525 L 2.13169 13.1371 C 1.80945 13.385 1.80945 13.4594 1.80945 13.5337 C 1.80945 13.6577 1.88382 13.8064 2.05733 13.8064 C 2.20605 13.8064 2.62743 13.4594 2.90009 13.2611 C 3.04881 13.1371 3.42061 12.8645 3.69327 12.6662 L 8.57632 23.399 C 8.74983 23.7956 8.87377 23.7956 9.09685 23.7956 C 9.46865 23.7956 9.54302 23.6468 9.71652 23.2998 L 20.9698 0 C 21.1434 -0.347019 21.1434 -0.446167 21.1434 -0.495741 C 21.1434 -0.743612 20.9451 -0.991482 20.6476 -0.991482 C 20.4493 -0.991482 20.2758 -0.867547 20.0775 -0.470954 L 9.64216 21.1929 Z";

            var p = PathConverter.ConvertPath(PathConverter.ParsePath(svg));

            return p;
   }
    }
}
