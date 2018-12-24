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
            var w = (int)document.Size.Width.Quantity + 1;
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
            if (element is Identifier) { ExportIdentifier(graphics, element as Identifier); }
            if (element is BinomialOperator) { ExportBinomialOperator(graphics, element as BinomialOperator); }
            if (element is NamedFunction) { ExportNamedFunction(graphics, element as NamedFunction); }
            if (element is MathematicsLine) { ExportMathematicsLine(graphics, element as MathematicsLine); }
            if (element is Fraction) { ExportFraction(graphics, element as Fraction); }
            if (element is Subscript) { ExportSubscript(graphics, element as Subscript); }
            if (element is Superscript) { ExportSuperscript(graphics, element as Superscript); }
            if (element is BracketExpression) { ExportBracketExpression(graphics, element as BracketExpression); }
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

        protected void ExportFraction(Graphics graphics, Fraction fraction)
        {
            ExportElement(graphics, fraction.Numerator);
            ExportElement(graphics, fraction.Denominator);

            var pen = new Pen(Color.Black, 1);
            var x1 = fraction.Position.X.Quantity;
            var y1 = fraction.Position.Y.Quantity + fraction.Numerator.SizeIncludingOuterMargin.Height.Quantity;
            var x2 = x1 + fraction.SizeIncludingOuterMargin.Width.Quantity;
            var y2 = y1;

            graphics.DrawLine(pen, new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2));

            if (fraction.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, fraction.Position, fraction.SizeIncludingOuterMargin);
            }
        }

        protected void ExportBracketExpression(Graphics graphics, BracketExpression bracketExpression)
        {
            ExportElement(graphics, bracketExpression.InnerExpression);
        }

        protected void ExportSubscript(Graphics graphics, Subscript subscript)
        {
            ExportElement(graphics, subscript.Element1);
            ExportElement(graphics, subscript.Element2);

            if (subscript.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, subscript.Position, subscript.SizeIncludingOuterMargin);
            }
        }

        protected void ExportSuperscript(Graphics graphics, Superscript superscript)
        {
            ExportElement(graphics, superscript.Element1);
            ExportElement(graphics, superscript.Element2);

            if (superscript.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, superscript.Position, superscript.SizeIncludingOuterMargin);
            }
        }

        protected void ExportTextElement(Graphics graphics, TextElement textElement)
        {
            var text = textElement.Content;

            var fontFamily = textElement.FontStyle.FontName;
            var emSize = (float)textElement.FontStyle.FontHeight.ConvertToUnits(LengthUnits.Points).Quantity;
            var font = new Font(fontFamily, emSize);

            if (textElement.FontStyle.FontEmphasis == FontEmphasis.Italic)
            {
                font = new Font(fontFamily, emSize, System.Drawing.FontStyle.Italic);
            }

            var brush = Brushes.Black;

            var x = (float)(textElement.Position.X + textElement.OuterMargin.Left + textElement.Border.Width + textElement.InnerMargin.Left).Quantity;
            var y = (float)(textElement.Position.Y + textElement.OuterMargin.Top + textElement.Border.Width + textElement.InnerMargin.Top).Quantity;
            var point = new PointF(x, y);

            graphics.DrawString(text, font, brush, point);

            if (textElement.DrawConstructionLines == true)
            {
                DrawConstructionLines(graphics, textElement.Position, textElement.SizeIncludingOuterMargin);
            }
        }

        protected void ExportNumber(Graphics graphics, Number number)
        {
            ExportTextElement(graphics, number);
        }

        protected void ExportIdentifier(Graphics graphics, Identifier identifier)
        {
            ExportTextElement(graphics, identifier);
        }

        protected void ExportBinomialOperator(Graphics graphics, BinomialOperator binomialOperator)
        {
            ExportTextElement(graphics, binomialOperator);
        }

        protected void ExportNamedFunction(Graphics graphics, NamedFunction namedFunction)
        {
            ExportTextElement(graphics, namedFunction);
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
