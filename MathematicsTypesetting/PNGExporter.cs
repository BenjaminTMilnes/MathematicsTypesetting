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
            using (var bitmap = new Bitmap(100, 100))
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
        }

        protected void ExportNumber(Graphics graphics, Number number)
        {
            graphics.DrawString(number.Content, new Font("Book Antiqua", 20), Brushes.Black, new PointF((float)number.Position.X.Quantity, (float)number.Position.Y.Quantity));

            if (number.DrawConstructionLines == true)
            {
                graphics.DrawRectangle(Pens.Black, new Rectangle( (int) number.Position.X.Quantity, (int)number.Position.Y.Quantity, (int)number.SizeIncludingOuterMargin.Width.Quantity, (int)number.SizeIncludingOuterMargin.Height.Quantity));
            }
        }
    }
}
