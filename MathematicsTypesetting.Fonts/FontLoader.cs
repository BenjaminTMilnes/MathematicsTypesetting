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
                style.Emphasis = s.Attribute(XName.Get("emphasis", "")).Value;

                foreach (var g in s.Elements())
                {
                    var glyph = new Glyph();

                    glyph.Character = g.Attribute(XName.Get("character", "")).Value;
                    glyph.Path = g.Attribute(XName.Get("path", "")).Value;

                    ParsePathCommands(glyph);
                    SetGlyphWidth(glyph);

                    style.Glyphs.Add(glyph);
                }

                Styles.Add(style);
            }
        }

        public Glyph GetGlyph(string styleWeight, string styleEmphasis, string character)
        {
            if (Styles.Any(s => s.Weight == styleWeight && s.Emphasis == styleEmphasis))
            {
                return Styles.First(s => s.Weight == styleWeight && s.Emphasis == styleEmphasis).Glyphs.FirstOrDefault(g => g.Character == character);
            }

            return null;
        }

        public void DrawString(Graphics graphics, string text, float fontSize, string fontEmphasis, string fontWeight, Brush brush, PointF point)
        {
            var x = point.X;
            var y = point.Y;
            var letterSpacing = 0.01f;

            foreach (var c in text)
            {
                var g = GetGlyph("normal", fontEmphasis, c.ToString());

                if (g != null)
                {
                    var p = GetPathForGlyph(g);

                    var m = new System.Drawing.Drawing2D.Matrix();

                    var sf = fontSize / 20.0f;

                    m.Scale(sf, sf);
                    m.Translate(x / sf, y / sf + 25f);

                    p.Transform(m);

                    graphics.FillPath(brush, p);

                    x += g.Width * sf + letterSpacing * fontSize * sf;
                }
            }
        }

        public SizeF MeasureString(string text, float fontSize, string fontEmphasis, string fontWeight)
        {
            var w = 0.0f;
            var letterSpacing = 0.01f;
            var sf = fontSize / 20.0f;

            foreach (var c in text)
            {
                var g = GetGlyph("normal", fontEmphasis, c.ToString());

                if (g != null)
                {
                    w += g.Width * sf + letterSpacing * fontSize * sf;
                }
            }

            return new SizeF(w, fontSize * 1.65f);
        }

        public void SetGlyphWidth(Glyph glyph)
        {
            var x1 = glyph.PathCommands.First().Arguments[0];
            var x2 = glyph.PathCommands.First().Arguments[0];
            var y1 = glyph.PathCommands.First().Arguments[1];
            var y2 = glyph.PathCommands.First().Arguments[1];

            foreach (var c in glyph.PathCommands)
            {
                var x = 0.0f;
                var y = 0.0f;

                if (c.Type == PathCommandType.MoveTo)
                {
                    x = c.Arguments[0];
                    y = c.Arguments[1];
                }
                if (c.Type == PathCommandType.LineTo)
                {
                    x = c.Arguments[0];
                    y = c.Arguments[1];
                }
                if (c.Type == PathCommandType.HorizontalLineTo)
                {
                    x = c.Arguments[0];
                }
                if (c.Type == PathCommandType.VerticalLineTo)
                {
                    y = c.Arguments[0];
                }
                if (c.Type == PathCommandType.BezierCurveTo)
                {
                    x = c.Arguments[4];
                    y = c.Arguments[5];
                }
                if (c.Type == PathCommandType.BezierSplineTo)
                {
                    x = c.Arguments[2];
                    y = c.Arguments[3];
                }

                if (x < x1)
                {
                    x1 = x;
                }

                if (x > x2)
                {
                    x2 = x;
                }

                if (y < y1)
                {
                    y1 = y;
                }

                if (y > y2)
                {
                    y2 = y;
                }
            }

            glyph.Width = x2 - x1;
            glyph.Height = y2 - y1;
        }

        public void ParsePathCommands(Glyph glyph)
        {
            var a = glyph.Path.Split(' ');
            var n = 0;

            glyph.PathCommands.Clear();

            while (n < a.Length)
            {
                if ("MCSLHVZ".Any(c => c.ToString() == a[n]))
                {
                    var pathCommand = new PathCommand();

                    if (a[n] == "M")
                    {
                        pathCommand.Type = PathCommandType.MoveTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };
                    }
                    else if (a[n] == "C")
                    {
                        pathCommand.Type = PathCommandType.BezierCurveTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]), float.Parse(a[n + 5]), float.Parse(a[n + 6]) };
                    }
                    else if (a[n] == "S")
                    {
                        pathCommand.Type = PathCommandType.BezierSplineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]), float.Parse(a[n + 3]), float.Parse(a[n + 4]) };
                    }
                    else if (a[n] == "L")
                    {
                        pathCommand.Type = PathCommandType.LineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]), float.Parse(a[n + 2]) };
                    }
                    else if (a[n] == "H")
                    {
                        pathCommand.Type = PathCommandType.HorizontalLineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]) };
                    }
                    else if (a[n] == "V")
                    {
                        pathCommand.Type = PathCommandType.VerticalLineTo;
                        pathCommand.Arguments = new List<float>() { float.Parse(a[n + 1]) };
                    }
                    else if (a[n] == "Z")
                    {
                        pathCommand.Type = PathCommandType.ClosePath;
                    }

                    n += pathCommand.Arguments.Count() + 1;

                    glyph.PathCommands.Add(pathCommand);
                }
            }
        }

        public System.Drawing.Drawing2D.GraphicsPath GetPathForGlyph(Glyph glyph)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            var cursorX = 0.0f;
            var cursorY = 0.0f;
            var lastAnchorPointX = 0.0f;
            var lastAnchorPointY = 0.0f;

            foreach (var c in glyph.PathCommands)
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
                if (c.Type == PathCommandType.VerticalLineTo)
                {
                    path.AddLine(new PointF(cursorX, cursorY), new PointF(cursorX, c.Arguments[0]));

                    cursorY = c.Arguments[0];
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
