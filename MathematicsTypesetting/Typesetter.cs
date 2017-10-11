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
            if (mathematicsLine.Elements.Any())
            {
                var elements = mathematicsLine.Elements.ToArray();

                Element elementM = null;
                Element elementN = null;
                Length maximumContentHeight = 0;

                for (var n = 0; n < elements.Length; n++)
                {
                    elementN = elements[n];

                    // Add the width of the current element.
                    mathematicsLine.SizeOfContent.Width += elementN.SizeIncludingBorder.Width;

                    if (n > 0)
                    {
                        elementM = elements[n - 1];

                        // Add the spacing of the margins of the current element and the previous element.
                        mathematicsLine.SizeOfContent.Width += ChooseGreaterLength(elementM.OuterMargin.Right, elementN.OuterMargin.Left);
                    }

                    // If this element is taller, update the maximum content height.
                    if (elementN.SizeIncludingBorder.Height > maximumContentHeight)
                    {
                        maximumContentHeight = elementN.SizeIncludingBorder.Height;
                    }
                }

                mathematicsLine.SizeOfContent.Height = maximumContentHeight;
            }
            else
            {
                mathematicsLine.SizeOfContent = 0;
            }
        }

        protected Length ChooseGreaterLength(Length length1, Length length2)
        {
            if (length1 > length2)
            {
                return length1;
            }

            return length2;
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
