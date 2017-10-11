using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Typesetter
    {
        protected ITextMeasurer _textMeasurer;

        public Typesetter(ITextMeasurer textMeasurer)
        {
            _textMeasurer = textMeasurer;
        }

         public void SetMathematicsLineSize(MathematicsLine mathematicsLine)
        {
        }

        public void SetNumberSize(Number number)
        {
            number.SizeOfContent = _textMeasurer.MeasureTextSize(number.Content, number.FontStyle);
            number.SizeIncludingInnerMargin = AddMarginToSize(number.SizeOfContent, number.InnerMargin);

            number.SizeIncludingOuterMargin = AddMarginToSize(number.SizeIncludingInnerMargin, number.OuterMargin);
        }

        protected Size AddMarginToSize(Size size, Margin margin)
        {
            var newSize = new Size();

            newSize.Width = size.Width + margin.Left + margin.Right;
            newSize.Height = size.Height + margin.Top + margin.Bottom;

            return newSize;
        }

        protected Size AddBorderToSize(Size size)
        {
            var newSize = new Size();

            return newSize;
        }
    }
}
