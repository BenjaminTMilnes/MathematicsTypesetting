using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Reflection;
using MathematicsTypesetting.SVG;

namespace MathematicsTypesetting.Fonts
{
    public class FontLoader
    {
        public IList<Style> Styles { get; set; }

        public FontLoader()
        {
            Styles = new List<Style>();
        }

        public void LoadFont()
        {
            Styles.Clear();

            var a = Assembly.GetExecutingAssembly();

            var document = XDocument.Load(a.GetManifestResourceStream("MathematicsTypesetting.Fonts.LatinModern.font.xml"));
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

                    glyph.PathCommands = PathConverter.ParsePath(glyph.Path).Commands;
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
                var g = GetGlyph(fontWeight, fontEmphasis, c.ToString());

                if (g != null)
                {
                    var p = PathConverter.ConvertPath(new SVG.Path() { Commands = g.PathCommands });

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

        public SizeF MeasureString(string text, float fontSize, string fontEmphasis = "none", string fontWeight = "normal")
        {
            var w = 0.0f;
            var letterSpacing = 0.01f;
            var sf = fontSize / 20.0f;

            foreach (var c in text)
            {
                var g = GetGlyph(fontWeight, fontEmphasis, c.ToString());

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
    }
}
