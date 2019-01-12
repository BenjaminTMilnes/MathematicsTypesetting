using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathematicsTypesetting.Fonts;

namespace MathematicsTypesetting
{
    public class HyperfontTextMeasurer : ITextMeasurer
    {
        private FontLoader _fontLoader;

        public HyperfontTextMeasurer(FontLoader fontLoader)
        {
            _fontLoader = fontLoader;
        }

        public Size MeasureTextSize(string text, FontStyle fontStyle)
        {
            var emSize = fontStyle.FontHeight.ConvertToUnits(LengthUnits.Points).Quantity;

            var fontEmphasis = (fontStyle.FontEmphasis == FontEmphasis.Italic) ? "italic" : "none";
            var fontWeight = (fontStyle.FontWeight == FontWeight.Bold) ? "bold" : "normal";

            var sizeF = _fontLoader.MeasureString(text, (float)emSize, fontEmphasis, fontWeight);
            var size = new Size();

            size.Width = new Length(sizeF.Width, LengthUnits.Arbitrary);
            size.Height = new Length(sizeF.Height, LengthUnits.Arbitrary);

            return size;
        }
    }
}
