using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MathematicsTypesetting
{
    public class TextMeasurer : ITextMeasurer
    {
        private Bitmap _bitmap;
        private Graphics _graphics;

        public TextMeasurer()
        {
            _bitmap = new Bitmap(100, 100);
            _graphics = Graphics.FromImage(_bitmap);
        }

        public Size MeasureTextSize(string text, FontStyle fontStyle)
        {
            var sizeF = _graphics.MeasureString(text, new Font("Book Antiqua", 20));
            var size = new Size();

            size.Width = sizeF.Width;
            size.Height = sizeF.Height;

            return size;
        }
    }
}
