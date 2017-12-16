using System.Drawing;

namespace MathematicsTypesetting
{
    public class TextMeasurer : ITextMeasurer
    {
        private Bitmap _bitmap;
        private Graphics _graphics;

        public TextMeasurer()
        {
            _bitmap = new Bitmap(1, 1);
            _graphics = Graphics.FromImage(_bitmap);
        }

        public Size MeasureTextSize(string text, FontStyle fontStyle)
        {
            var fontFamily = fontStyle.FontName;
            var emSize = fontStyle.FontHeight.ConvertToUnits(LengthUnits.Points).Quantity;
            var font = new Font(fontFamily, (float)emSize);

            var sizeF = _graphics.MeasureString(text, font);
            var size = new Size();

            size.Width = new Length(sizeF.Width, LengthUnits.Arbitrary);
            size.Height = new Length(sizeF.Height, LengthUnits.Arbitrary);

            return size;
        }
    }
}
