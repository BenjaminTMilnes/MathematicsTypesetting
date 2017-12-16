using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace MathematicsTypesetting
{
    public class PNGExporter : Exporter
    {
        public void ExportMathematics(Document document, string fileLocation)
        {
            var w = (int) document.Size.Width.Quantity + 1;
            var h = (int)document.Size.Height.Quantity + 1;

            using (var bitmap = new Bitmap(w, h))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    graphics.Clear(Color.White);

                    ExportElement(graphics, document.MainElement);
                }

                bitmap.Save(fileLocation, ImageFormat.Png);
            }
        }

        protected void ExportElement(Graphics graphics, Element element)
        {
            if (element is Number) { ExportNumber(graphics, element as Number); }
            if (element is MathematicsLine) { ExportMathematicsLine(graphics, element as MathematicsLine); }
        }

        protected void ExportMathematicsLine(Graphics graphics, MathematicsLine mathematicsLine)
        {
            foreach (var element in mathematicsLine.Elements)
            {
                ExportElement(graphics, element);
            }

            if (mathematicsLine.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, mathematicsLine.Position, mathematicsLine.SizeIncludingOuterMargin);
            }
        }

        protected void ExportNumber(Graphics graphics, Number number)
        {
            var text = number.Content;

            var fontFamily = number.FontStyle.FontName;
            var emSize = (float)number.FontStyle.FontHeight.ConvertToUnits(LengthUnits.Points).Quantity;
            var font = new Font(fontFamily, emSize);

            var brush = Brushes.Black;

            var x = (float)number.Position.X.Quantity;
            var y = (float)number.Position.Y.Quantity;
            var point = new PointF(x, y);

            graphics.DrawString(text, font, brush, point);

            if (number.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, number.Position, number.SizeIncludingOuterMargin);
            }
        }

        protected void DrawConstructionLines(Graphics graphics, Position position, Size size)
        {
            var x = (int)position.X.Quantity;
            var y = (int)position.Y.Quantity;
            var w = (int)size.Width.Quantity;
            var h = (int)size.Height.Quantity;

            var rectangle = new Rectangle(x, y, w, h);

            var pen = new Pen(Color.Black, 0.75f);

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen.DashPattern = new float[] { 1.0f, 2.0f };

            graphics.DrawRectangle(pen, rectangle);
        }
    }
}
