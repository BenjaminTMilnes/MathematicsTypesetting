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

        public void SetElementSize(Element element)
        {
            if (element is Number)
            {
                SetNumberSize(element as Number);
            }
        }

        /// <summary>
        /// Sets the size properties of a MathematicsLine element.
        /// </summary>
        /// <param name="mathematicsLine"></param>
        public void SetMathematicsLineSize(MathematicsLine mathematicsLine)
        {
            if (mathematicsLine.Elements.Any())
            {
                var elements = mathematicsLine.Elements.ToArray();

                Element elementM = null;
                Element elementN = null;
                Length maximumContentHeight = 0;

                // Add the width of the first element's left margin.
                mathematicsLine.SizeOfContent.Width += elements.First().OuterMargin.Left;

                for (var n = 0; n < elements.Length; n++)
                {
                    elementN = elements[n];

                    SetElementSize(elementN);

                    // Add the width of the current element.
                    mathematicsLine.SizeOfContent.Width += elementN.SizeIncludingBorder.Width;

                    if (n > 0)
                    {
                        elementM = elements[n - 1];

                        // Add the spacing of the margins of the current element and the previous element.
                        mathematicsLine.SizeOfContent.Width += ChooseGreaterLength(elementM.OuterMargin.Right, elementN.OuterMargin.Left);
                    }

                    // If this element is taller, update the maximum content height.
                    if (elementN.SizeIncludingOuterMargin.Height > maximumContentHeight)
                    {
                        maximumContentHeight = elementN.SizeIncludingOuterMargin.Height;
                    }
                }

                // Add the width of the last element's right margin.
                mathematicsLine.SizeOfContent.Width += elements.Last().OuterMargin.Right;

                mathematicsLine.SizeOfContent.Height = maximumContentHeight;
            }
            else
            {
                mathematicsLine.SizeOfContent = 0;
            }

            mathematicsLine.SizeIncludingInnerMargin = AddMarginToSize(mathematicsLine.SizeOfContent, mathematicsLine.InnerMargin);
            mathematicsLine.SizeIncludingBorder = AddBorderToSize(mathematicsLine.SizeIncludingInnerMargin, mathematicsLine.Border);
            mathematicsLine.SizeIncludingOuterMargin = AddMarginToSize(mathematicsLine.SizeIncludingBorder, mathematicsLine.OuterMargin);
        }

        /// <summary>
        /// Returns the greater of two lengths; useful for calculating outer margin spacing.
        /// </summary>
        /// <param name="length1"></param>
        /// <param name="length2"></param>
        /// <returns></returns>
        protected Length ChooseGreaterLength(Length length1, Length length2)
        {
            if (length1 > length2)
            {
                return length1;
            }

            return length2;
        }

        /// <summary>
        /// Sets the size properties of a Number element.
        /// </summary>
        /// <param name="number"></param>
        public void SetNumberSize(Number number)
        {
            number.SizeOfContent = _textMeasurer.MeasureTextSize(number.Content, number.FontStyle);
            number.SizeIncludingInnerMargin = AddMarginToSize(number.SizeOfContent, number.InnerMargin);
            number.SizeIncludingBorder = AddBorderToSize(number.SizeIncludingInnerMargin, number.Border);
            number.SizeIncludingOuterMargin = AddMarginToSize(number.SizeIncludingBorder, number.OuterMargin);
        }

        protected Size AddMarginToSize(Size size, Margin margin)
        {
            var newSize = new Size();

            newSize.Width = size.Width + margin.Left + margin.Right;
            newSize.Height = size.Height + margin.Top + margin.Bottom;

            return newSize;
        }

        protected Size AddBorderToSize(Size size, Border border)
        {
            var newSize = new Size();

            newSize.Width = size.Width + border.Width + border.Width;
            newSize.Height = size.Height + border.Width + border.Width;

            return newSize;
        }
    }
}
