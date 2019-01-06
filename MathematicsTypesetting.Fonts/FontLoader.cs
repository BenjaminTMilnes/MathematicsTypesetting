using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace MathematicsTypesetting.Fonts
{
    public class FontLoader
    {
        public IList<Style> Styles { get; set; }

        public FontLoader()
        {
            Styles = new List<Style>();
        }

        public void LoadFont(string fileName = "LatinModern.font.xml")
        {
            Styles.Clear();

            var document = XDocument.Load("../../../MathematicsTypesetting.Fonts/" + fileName);
            var root = document.Root;

            foreach (var s in root.Elements())
            {
                var style = new Style();

                style.Weight = s.Attribute(XName.Get("weight", "")).Value;

                foreach (var g in s.Elements())
                {
                    var glyph = new Glyph();

                    glyph.Character = g.Attribute(XName.Get("character", "")).Value;
                    glyph.Path = g.Attribute(XName.Get("path", "")).Value;

                    style.Glyphs.Add(glyph);
                }

                Styles.Add(style);
            }
        }

        public Glyph GetGlyph(string styleWeight, string character)
        {
             if (Styles.Any(s => s.Weight == styleWeight))
            {
                return Styles.First(s => s.Weight == styleWeight).Glyphs.FirstOrDefault(g => g.Character == character);
            }

            return null;                    
        }

        public System.Drawing.Drawing2D.GraphicsPath GetPathForGlyph(Glyph glyph)
        {
            var pathCommands = new List<PathCommand>();

            var a = glyph.Path.Split(' ');
            var n = 0;

            while (n < a.Length)
            {
                if (a[n] == "M")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.MoveTo;
                    pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };

                    n += 3;

                    pathCommands.Add(pathCommand);
                }
                else if (a[n] == "C")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.BezierCurveTo;
                    pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]), float.Parse(a[n + 5]), float.Parse(a[n + 6]) };

                    n += 7;

                    pathCommands.Add(pathCommand);
                }
                else if (a[n] == "S")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.BezierSplineTo;
                    pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]) };

                    n += 5;

                    pathCommands.Add(pathCommand);
                }
                else if (a[n] == "L")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.LineTo;
                    pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };

                    n += 3;

                    pathCommands.Add(pathCommand);
                }
                else if (a[n] == "H")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.HorizontalLineTo;
                    pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]) };

                    n += 2;

                    pathCommands.Add(pathCommand);
                }
                else if (a[n] == "Z")
                {
                    var pathCommand = new PathCommand();

                    pathCommand.Type = PathCommandType.ClosePath;
                    pathCommand.Arguments = new List<float>() { };

                    n += 1;

                    pathCommands.Add(pathCommand);
                }
            }

            var path = new System.Drawing.Drawing2D.GraphicsPath();
            var cursorX = 0.0f;
            var cursorY = 0.0f;
            var lastAnchorPointX = 0.0f;
            var lastAnchorPointY = 0.0f;

            foreach (var c in pathCommands)
            {
                if (c.Type == PathCommandType.MoveTo)
                {
                    cursorX = c.Arguments[0];
                    cursorY = c.Arguments[1];
                }
                if (c.Type == PathCommandType.BezierCurveTo)
                {
                    path.AddBezier(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], c.Arguments[1]), new PointF(c.Arguments[2], c.Arguments[3]), new PointF(c.Arguments[4], c.Arguments[5]));

                    cursorX = c.Arguments[4];
                    cursorY = c.Arguments[5];

                    lastAnchorPointX = c.Arguments[2];
                    lastAnchorPointY = c.Arguments[3];
                }
                if (c.Type == PathCommandType.BezierSplineTo)
                {
                    var x1 = (cursorX - lastAnchorPointX) + cursorX;
                    var y1 = (cursorY - lastAnchorPointY) + cursorY;

                    path.AddBezier(new PointF(cursorX, cursorY), new PointF(x1, y1), new PointF(c.Arguments[0], c.Arguments[1]), new PointF(c.Arguments[2], c.Arguments[3]));

                    cursorX = c.Arguments[2];
                    cursorY = c.Arguments[3];

                    lastAnchorPointX = c.Arguments[0];
                    lastAnchorPointY = c.Arguments[1];
                }
                if (c.Type == PathCommandType.LineTo)
                {
                    path.AddLine(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], c.Arguments[1]));
                    
                    cursorX = c.Arguments[0];
                    cursorY = c.Arguments[1];
                }
                if (c.Type == PathCommandType.HorizontalLineTo)
                {
                    path.AddLine(new PointF(cursorX, cursorY), new PointF(c.Arguments[0], cursorY));

                    cursorX = c.Arguments[0];
                }
                if (c.Type == PathCommandType.ClosePath)
                {
                    path.CloseFigure();
                }
            }

            return path;
        }
    }
}
