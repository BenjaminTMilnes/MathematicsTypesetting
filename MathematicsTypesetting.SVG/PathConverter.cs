using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MathematicsTypesetting.SVG
{
     public    class PathConverter
    {


        public GraphicsPath ConvertPath(    Path path)
        {
            var    graphicsPath =  new GraphicsPath();
            var cursorX = 0.0f;
            var cursorY = 0.0f;
            var lastAnchorPointX = 0.0f;
            var lastAnchorPointY = 0.0f;

            foreach (var c in   path.Commands)
            {
                if (c.Type == PathCommandType.MoveTo)
                {
                    cursorX = c.Arguments[0];
                    cursorY = c.Arguments[1];
                }
                if (c.Type == PathCommandType.BezierCurveTo)
                {
                    graphicsPath.AddBezier(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], c.Arguments[1]), new PointF(c.Arguments[2], c.Arguments[3]), new PointF(c.Arguments[4], c.Arguments[5]));
                    
                    lastAnchorPointX = c.Arguments[2];
                    lastAnchorPointY = c.Arguments[3];

                    cursorX = c.Arguments[4];
                    cursorY = c.Arguments[5];
                }
                if (c.Type == PathCommandType.BezierSplineTo)
                {
                    var x1 = (cursorX - lastAnchorPointX) + cursorX;
                    var y1 = (cursorY - lastAnchorPointY) + cursorY;

                    graphicsPath.AddBezier(new PointF(cursorX, cursorY), new PointF(x1, y1), new PointF(c.Arguments[0], c.Arguments[1]), new PointF(c.Arguments[2], c.Arguments[3]));
                    
                    lastAnchorPointX = c.Arguments[0];
                    lastAnchorPointY = c.Arguments[1];

                    cursorX = c.Arguments[2];
                    cursorY = c.Arguments[3];
                }
                if (c.Type == PathCommandType.LineTo)
                {
                    graphicsPath.AddLine(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], c.Arguments[1]));

                    cursorX = c.Arguments[0];
                    cursorY = c.Arguments[1];
                }
                if (c.Type == PathCommandType.HorizontalLineTo)
                {
                    graphicsPath.AddLine(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], cursorY));

                    cursorX = c.Arguments[0];
                }
                if (c.Type == PathCommandType.VerticalLineTo)
                {
                    graphicsPath.AddLine(new PointF(cursorX, cursorY), new PointF(cursorX, c.Arguments[0]));

                    cursorY = c.Arguments[0];
                }
                if (c.Type == PathCommandType.ClosePath)
                {
                    graphicsPath.CloseFigure();
                }
            }

            return graphicsPath;
        }

        public Path       ParsePath(    string svgPath)
        {
            var path = new Path();

            var a = svgPath.Split(' ');
            var n = 0;            

            while (n < a.Length)
            {
                var c1 = a[n];

                if ("MLHVCSZ".Any(c2 => c2.ToString() == c1))
                {
                    var pathCommand = new PathCommand();

                    if (c1 == "M")
                    {
                        pathCommand.Type = PathCommandType.MoveTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };
                    }
                    else if (c1 == "L")
                    {
                        pathCommand.Type = PathCommandType.LineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };
                    }
                    else if (  c1 == "H")
                    {
                        pathCommand.Type = PathCommandType.HorizontalLineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]) };
                    }
                    else if (c1 == "V")
                    {
                        pathCommand.Type = PathCommandType.VerticalLineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]) };
                    }
                    else if (   c1 == "C")
                    {
                        pathCommand.Type = PathCommandType.BezierCurveTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]), float.Parse(a[n + 5]), float.Parse(a[n + 6]) };
                    }
                    else if (c1 == "S")
                    {
                        pathCommand.Type = PathCommandType.BezierSplineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]) };
                    }
                    else if (   c1 == "Z")
                    {
                        pathCommand.Type = PathCommandType.ClosePath;
                    }

                    n += pathCommand.Arguments.Count() + 1;

                    path.Commands.Add(pathCommand);
                }
            }

            return path;
        }
    }
}
