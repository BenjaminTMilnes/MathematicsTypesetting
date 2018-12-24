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

        public void TypesetDocument(Document document)
        {
            SetElementSize(document.MainElement);
            SetElementPosition(new Position(), document.MainElement);

            document.Size.Width = document.MainElement.SizeIncludingOuterMargin.Width;
            document.Size.Height = document.MainElement.SizeIncludingOuterMargin.Height;
        }

        public void SetElementPosition(Position containerOrigin, Element element)
        {
            if (element is Number) { SetNumberPosition(containerOrigin, element as Number); }
            if (element is Identifier) { SetIdentifierPosition(containerOrigin, element as Identifier); }
            if (element is BinomialOperator) { SetBinomialOperatorPosition(containerOrigin, element as BinomialOperator); }
            if (element is NamedFunction) { SetNamedFunctionPosition(containerOrigin, element as NamedFunction); }
            if (element is MathematicsLine) { SetMathematicsLinePosition(containerOrigin, element as MathematicsLine); }
            if (element is Fraction) { SetFractionPosition(containerOrigin, element as Fraction); }
            if (element is Subscript) { SetSubscriptPosition(containerOrigin, element as Subscript); }
            if (element is Superscript) { SetSuperscriptPosition(containerOrigin, element as Superscript); }
            if (element is BracketExpression) { SetBracketExpressionPosition(containerOrigin, element as BracketExpression); }
        }

        public void SetMathematicsLinePosition(Position containerOrigin, MathematicsLine mathematicsLine)
        {
            mathematicsLine.Position = containerOrigin;

            containerOrigin.X += mathematicsLine.OuterMargin.Left + mathematicsLine.Border.Width + mathematicsLine.InnerMargin.Left;
            containerOrigin.Y += mathematicsLine.OuterMargin.Top + mathematicsLine.Border.Width + mathematicsLine.InnerMargin.Top;

            var elements = mathematicsLine.Elements.ToArray();

            for (var m = 0; m < elements.Length; m++)
            {
                var elementM = elements[m];

                containerOrigin.Y = mathematicsLine.Position.Y + mathematicsLine.OuterMargin.Top + mathematicsLine.Border.Width + mathematicsLine.InnerMargin.Top + mathematicsLine.SizeOfContent.Height / 2;

                var elementYOffset = elementM.CentreAlignmentPoint.Y;

                containerOrigin.Y -= elementYOffset;

                SetElementPosition(containerOrigin, elementM);

                containerOrigin.Y += elementYOffset;

                if (m < elements.Length - 1)
                {
                    var elementN = elements[m + 1];

                    var separation = ChooseGreaterLength(elementM.OuterMargin.Right, elementN.OuterMargin.Left);

                    containerOrigin.X += elementM.SizeIncludingOuterMargin.Width - elementM.OuterMargin.Right + separation - elementN.OuterMargin.Left;
                }
                else
                {
                    containerOrigin.X += elementM.SizeIncludingOuterMargin.Width;
                }
            }
        }

        public void SetBracketExpressionPosition(Position containerOrigin, BracketExpression bracketExpression)
        {
            bracketExpression.Position = containerOrigin;

            SetElementPosition(containerOrigin, bracketExpression.InnerExpression);
        }

        public void SetFractionPosition(Position containerOrigin, Fraction fraction)
        {
            fraction.Position = containerOrigin;

            var numeratorOffset = (fraction.SizeOfContent.Width - fraction.Numerator.SizeIncludingOuterMargin.Width) / 2;
            var denominatorOffset = (fraction.SizeOfContent.Width - fraction.Denominator.SizeIncludingOuterMargin.Width) / 2;

            containerOrigin.X += fraction.LeftWidth + numeratorOffset;
            containerOrigin.Y += fraction.TopWidth;

            SetElementPosition(containerOrigin, fraction.Numerator);

            containerOrigin.X += -numeratorOffset + denominatorOffset;
            containerOrigin.Y += fraction.Numerator.SizeIncludingOuterMargin.Height;

            SetElementPosition(containerOrigin, fraction.Denominator);
        }

        public void SetSubscriptPosition(Position containerOrigin, Subscript subscript)
        {
            subscript.Position = containerOrigin;

            containerOrigin.X += subscript.LeftWidth;
            containerOrigin.Y += subscript.TopWidth;

            SetElementPosition(containerOrigin, subscript.Element1);

            var marginAdjustment = ChooseLesserLength(subscript.Element1.OuterMargin.Right, subscript.Element2.OuterMargin.Left);

            containerOrigin.X += subscript.Element1.ContentWidth + marginAdjustment;
            containerOrigin.Y += -subscript.TopWidth + subscript.SubscriptOffset;

            SetElementPosition(containerOrigin, subscript.Element2);

            containerOrigin.Y += -subscript.SubscriptOffset;
        }

        public void SetSuperscriptPosition(Position containerOrigin, Superscript superscript)
        {
            superscript.Position = containerOrigin;

            containerOrigin.X += superscript.LeftWidth;
            containerOrigin.Y += superscript.TopWidth;

            SetElementPosition(containerOrigin, superscript.Element1);

            var marginAdjustment = ChooseLesserLength(superscript.Element1.OuterMargin.Right, superscript.Element2.OuterMargin.Left);

            containerOrigin.X += superscript.Element1.ContentWidth + marginAdjustment;
            containerOrigin.Y += -superscript.TopWidth + superscript.OuterHeight - superscript.SuperscriptOffset - superscript.Element2.OuterHeight;

            SetElementPosition(containerOrigin, superscript.Element2);

            containerOrigin.Y += -superscript.OuterHeight + superscript.SuperscriptOffset + superscript.Element2.OuterHeight;
        }

        public void SetTextElementPosition(Position containerOrigin, TextElement textElement)
        {
            textElement.Position = containerOrigin;
        }

        public void SetNumberPosition(Position containerOrigin, Number number)
        {
            SetTextElementPosition(containerOrigin, number);
        }

        public void SetIdentifierPosition(Position containerOrigin, Identifier identifier)
        {
            SetTextElementPosition(containerOrigin, identifier);
        }

        public void SetBinomialOperatorPosition(Position containerOrigin, BinomialOperator binomialOperator)
        {
            SetTextElementPosition(containerOrigin, binomialOperator);
        }

        public void SetNamedFunctionPosition(Position containerOrigin, NamedFunction namedFunction)
        {
            SetTextElementPosition(containerOrigin, namedFunction);
        }

        public void SetElementSize(Element element)
        {
            if (element is Number) { SetNumberSize(element as Number); }
            if (element is Identifier) { SetIdentifierSize(element as Identifier); }
            if (element is BinomialOperator) { SetBinomialOperatorSize(element as BinomialOperator); }
            if (element is NamedFunction) { SetNamedFunctionSize(element as NamedFunction); }
            if (element is MathematicsLine) { SetMathematicsLineSize(element as MathematicsLine); }
            if (element is Fraction) { SetFractionSize(element as Fraction); }
            if (element is Subscript) { SetSubscriptSize(element as Subscript); }
            if (element is Superscript) { SetSuperscriptSize(element as Superscript); }
            if (element is BracketExpression) { SetBracketExpressionSize(element as BracketExpression); }
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

            SetSizesOfElement(mathematicsLine);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = mathematicsLine.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = mathematicsLine.SizeIncludingOuterMargin.Height / 2;

            mathematicsLine.CentreAlignmentPoint = centreAlignmentPoint;
        }

        /// <summary>
        /// Returns the greater of two lengths; useful for calculating outer margin spacing.
        /// </summary>
        /// <param name="length1"></param>
        /// <param name="length2"></param>
        /// <returns></returns>
        protected Length ChooseGreaterLength(Length length1, Length length2)
        {
            return (length1 > length2) ? length1 : length2;
        }

        protected Length ChooseLesserLength(Length length1, Length length2)
        {
            return (length1 < length2) ? length1 : length2;
        }

        public void SetBracketExpressionSize(BracketExpression bracketExpression)
        {
            SetElementSize(bracketExpression.InnerExpression);

            bracketExpression.SizeOfContent.Width = bracketExpression.InnerExpression.SizeIncludingOuterMargin.Width;

            bracketExpression.SizeOfContent.Height = bracketExpression.InnerExpression.SizeIncludingOuterMargin.Height;

            SetSizesOfElement(bracketExpression);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = bracketExpression.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = bracketExpression.SizeIncludingOuterMargin.Height / 2;

            bracketExpression.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetFractionSize(Fraction fraction)
        {
            SetElementSize(fraction.Numerator);
            SetElementSize(fraction.Denominator);

            if (fraction.Numerator.SizeIncludingOuterMargin.Width > fraction.Denominator.SizeIncludingOuterMargin.Width)
            {
                fraction.SizeOfContent.Width = fraction.Numerator.SizeIncludingOuterMargin.Width;
            }
            else
            {
                fraction.SizeOfContent.Width = fraction.Denominator.SizeIncludingOuterMargin.Width;
            }

            fraction.SizeOfContent.Height = fraction.Numerator.SizeIncludingOuterMargin.Height + fraction.Denominator.SizeIncludingOuterMargin.Height;

            SetSizesOfElement(fraction);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = fraction.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = fraction.SizeIncludingOuterMargin.Height / 2;

            fraction.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetSubscriptSize(Subscript subscript)
        {
            subscript.Element1.FontStyle.FontHeight = subscript.FontStyle.FontHeight;
            subscript.Element2.FontStyle.FontHeight = subscript.FontStyle.FontHeight * subscript.SubscriptScale;

            SetElementSize(subscript.Element1);
            SetElementSize(subscript.Element2);

            var marginAdjustment = ChooseLesserLength(subscript.Element1.OuterMargin.Right, subscript.Element2.OuterMargin.Left);

            subscript.SizeOfContent.Width = subscript.Element1.SizeIncludingOuterMargin.Width + subscript.Element2.SizeIncludingOuterMargin.Width - marginAdjustment;
            subscript.SizeOfContent.Height = ChooseGreaterLength(subscript.Element1.SizeIncludingOuterMargin.Height, subscript.Element2.SizeIncludingOuterMargin.Height + subscript.SubscriptOffset);

            SetSizesOfElement(subscript);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = subscript.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = subscript.Element1.SizeIncludingOuterMargin.Height / 2;

            subscript.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetSuperscriptSize(Superscript superscript)
        {
            superscript.Element1.FontStyle.FontHeight = superscript.FontStyle.FontHeight;
            superscript.Element2.FontStyle.FontHeight = superscript.FontStyle.FontHeight * superscript.SuperscriptScale;

            SetElementSize(superscript.Element1);
            SetElementSize(superscript.Element2);

            var marginAdjustment = ChooseLesserLength(superscript.Element1.OuterMargin.Right, superscript.Element2.OuterMargin.Left);

            superscript.SizeOfContent.Width = superscript.Element1.SizeIncludingOuterMargin.Width + superscript.Element2.SizeIncludingOuterMargin.Width - marginAdjustment;
            superscript.SizeOfContent.Height = ChooseGreaterLength(superscript.Element1.SizeIncludingOuterMargin.Height, superscript.Element2.SizeIncludingOuterMargin.Height + superscript.SuperscriptOffset);

            SetSizesOfElement(superscript);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = superscript.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = superscript.Element1.SizeIncludingOuterMargin.Height / 2;

            superscript.CentreAlignmentPoint = centreAlignmentPoint;
        }

        protected void SetSizesOfElement(Element element)
        {
            element.SizeIncludingInnerMargin = element.SizeOfContent + element.InnerMargin;
            element.SizeIncludingBorder = element.SizeIncludingInnerMargin + element.Border;
            element.SizeIncludingOuterMargin = element.SizeIncludingBorder + element.OuterMargin;
        }

        public void SetTextElementSize(TextElement textElement)
        {
            textElement.SizeOfContent = _textMeasurer.MeasureTextSize(textElement.Content, textElement.FontStyle);

            SetSizesOfElement(textElement);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = textElement.SizeIncludingOuterMargin.Width / 2;
            centreAlignmentPoint.Y = textElement.SizeIncludingOuterMargin.Height / 2;

            textElement.CentreAlignmentPoint = centreAlignmentPoint;
        }

        /// <summary>
        /// Sets the size properties of a Number element.
        /// </summary>
        /// <param name="number"></param>
        public void SetNumberSize(Number number)
        {
            SetTextElementSize(number);
        }

        public void SetIdentifierSize(Identifier identifier)
        {
            SetTextElementSize(identifier);
        }

        public void SetBinomialOperatorSize(BinomialOperator binomialOperator)
        {
            SetTextElementSize(binomialOperator);
        }

        public void SetNamedFunctionSize(NamedFunction namedFunction)
        {
            SetTextElementSize(namedFunction);
        }
    }
}
