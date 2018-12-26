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

            document.Size.Width = document.MainElement.OuterWidth;
            document.Size.Height = document.MainElement.OuterHeight;
        }

        public void SetElementPosition(Position containerOrigin, Element element)
        {
            if (element is Number) { SetTextElementPosition(containerOrigin, element as Number); }
            if (element is Identifier) { SetTextElementPosition(containerOrigin, element as Identifier); }
            if (element is BinomialOperator) { SetTextElementPosition(containerOrigin, element as BinomialOperator); }
            if (element is Bracket) { SetTextElementPosition(containerOrigin, element as Bracket); }
            if (element is NamedFunction) { SetTextElementPosition(containerOrigin, element as NamedFunction); }
            if (element is MathematicsLine) { SetMathematicsLinePosition(containerOrigin, element as MathematicsLine); }
            if (element is Fraction) { SetFractionPosition(containerOrigin, element as Fraction); }
            if (element is Subscript) { SetSubscriptPosition(containerOrigin, element as Subscript); }
            if (element is Superscript) { SetSuperscriptPosition(containerOrigin, element as Superscript); }
            if (element is BracketExpression) { SetBracketExpressionPosition(containerOrigin, element as BracketExpression); }
            if (element is Text) { SetTextElementPosition(containerOrigin, element as Text); }
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

                    containerOrigin.X += elementM.OuterWidth - elementM.OuterMargin.Right + separation - elementN.OuterMargin.Left;
                }
                else
                {
                    containerOrigin.X += elementM.OuterWidth;
                }
            }
        }

        public void SetBracketExpressionPosition(Position containerOrigin, BracketExpression bracketExpression)
        {
            bracketExpression.Position = containerOrigin;

            containerOrigin.X += Paths.GetBracketLength();

            SetElementPosition(containerOrigin, bracketExpression.InnerExpression);

            containerOrigin.X += Paths.GetBracketLength();
        }

        public void SetFractionPosition(Position containerOrigin, Fraction fraction)
        {
            fraction.Position = containerOrigin;

            var numeratorOffset = (fraction.SizeOfContent.Width - fraction.Numerator.OuterWidth) / 2;
            var denominatorOffset = (fraction.SizeOfContent.Width - fraction.Denominator.OuterWidth) / 2;

            containerOrigin.X += fraction.LeftWidth + numeratorOffset;
            containerOrigin.Y += fraction.TopWidth;

            SetElementPosition(containerOrigin, fraction.Numerator);

            containerOrigin.X += -numeratorOffset + denominatorOffset;
            containerOrigin.Y += fraction.Numerator.OuterHeight;

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
            var position = containerOrigin;

            position.Y -= superscript.TopExcess;

            superscript.Position = position;

            containerOrigin.X += superscript.LeftWidth;
            containerOrigin.Y += superscript.TopWidth;

            SetElementPosition(containerOrigin, superscript.Element1);

            var marginAdjustment = ChooseLesserLength(superscript.Element1.OuterMargin.Right, superscript.Element2.OuterMargin.Left);

            containerOrigin.X += superscript.Element1.ContentWidth + marginAdjustment;
            containerOrigin.Y += -superscript.TopExcess;

            SetElementPosition(containerOrigin, superscript.Element2);

            containerOrigin.Y += superscript.TopExcess - superscript.TopWidth;
        }

        public void SetTextElementPosition(Position containerOrigin, TextElement textElement)
        {
            textElement.Position = containerOrigin;
        }

        public void SetElementSize(Element element)
        {
            if (element is Number) { SetTextElementSize(element as Number); }
            if (element is Identifier) { SetTextElementSize(element as Identifier); }
            if (element is BinomialOperator) { SetTextElementSize(element as BinomialOperator); }
            if (element is Bracket) { SetTextElementSize(element as Bracket); }
            if (element is NamedFunction) { SetTextElementSize(element as NamedFunction); }
            if (element is MathematicsLine) { SetMathematicsLineSize(element as MathematicsLine); }
            if (element is Fraction) { SetFractionSize(element as Fraction); }
            if (element is Subscript) { SetSubscriptSize(element as Subscript); }
            if (element is Superscript) { SetSuperscriptSize(element as Superscript); }
            if (element is BracketExpression) { SetBracketExpressionSize(element as BracketExpression); }
            if (element is Text) { SetTextElementSize(element as Text); }
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
                    if (elementN.OuterHeight > maximumContentHeight)
                    {
                        maximumContentHeight = elementN.OuterHeight;
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

            centreAlignmentPoint.X = mathematicsLine.OuterWidth / 2;
            centreAlignmentPoint.Y = mathematicsLine.OuterHeight / 2;

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

        /// <summary>
        /// Returns the lesser of two lengths.
        /// </summary>
        /// <param name="length1"></param>
        /// <param name="length2"></param>
        /// <returns></returns>
        protected Length ChooseLesserLength(Length length1, Length length2)
        {
            return (length1 < length2) ? length1 : length2;
        }

        /// <summary>
        /// For two elements that are next to each other, their outer margins overlap. This function returns the outer margin between the two elements that is larger, which is ultimately the size of the gap between the (borders of the) elements.
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        /// <returns></returns>
        protected Length GetMarginAdjustment(Element element1, Element element2)
        {
            return ChooseLesserLength(element1.OuterMargin.Right, element2.OuterMargin.Left);
        }

        public void SetBracketExpressionSize(BracketExpression bracketExpression)
        {
            SetElementSize(bracketExpression.InnerExpression);

            bracketExpression.SizeOfContent.Width = bracketExpression.InnerExpression.OuterWidth + 2 * Paths.GetBracketLength();

            bracketExpression.SizeOfContent.Height = bracketExpression.InnerExpression.OuterHeight;

            SetSizesOfElement(bracketExpression);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = bracketExpression.OuterWidth / 2;
            centreAlignmentPoint.Y = bracketExpression.OuterHeight / 2;

            bracketExpression.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetFractionSize(Fraction fraction)
        {
            SetElementSize(fraction.Numerator);
            SetElementSize(fraction.Denominator);

            fraction.SizeOfContent.Width = ChooseGreaterLength(fraction.Numerator.OuterWidth, fraction.Denominator.OuterWidth);

            fraction.SizeOfContent.Height = fraction.Numerator.OuterHeight + fraction.Denominator.OuterHeight;

            SetSizesOfElement(fraction);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = fraction.OuterWidth / 2;
            centreAlignmentPoint.Y = fraction.OuterHeight / 2;

            fraction.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetSubscriptSize(Subscript subscript)
        {
            subscript.Element1.FontStyle.FontHeight = subscript.FontStyle.FontHeight;
            subscript.Element2.FontStyle.FontHeight = subscript.FontStyle.FontHeight * subscript.SubscriptScale;

            SetElementSize(subscript.Element1);
            SetElementSize(subscript.Element2);

            var marginAdjustment = GetMarginAdjustment(subscript.Element1, subscript.Element2);

            subscript.SizeOfContent.Width = subscript.Element1.OuterWidth + subscript.Element2.OuterWidth - marginAdjustment;
            subscript.SizeOfContent.Height = ChooseGreaterLength(subscript.Element1.OuterHeight, subscript.Element2.OuterHeight + subscript.SubscriptOffset);

            SetSizesOfElement(subscript);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = subscript.OuterWidth / 2;
            centreAlignmentPoint.Y = subscript.Element1.OuterHeight / 2;

            subscript.CentreAlignmentPoint = centreAlignmentPoint;
        }

        public void SetSuperscriptSize(Superscript superscript)
        {
            superscript.Element1.FontStyle.FontHeight = superscript.FontStyle.FontHeight;
            superscript.Element2.FontStyle.FontHeight = superscript.FontStyle.FontHeight * superscript.SuperscriptScale;

            SetElementSize(superscript.Element1);
            SetElementSize(superscript.Element2);

            var marginAdjustment = GetMarginAdjustment(superscript.Element1, superscript.Element2);

            superscript.SizeOfContent.Width = superscript.Element1.OuterWidth + superscript.Element2.OuterWidth - marginAdjustment;
            superscript.SizeOfContent.Height = ChooseGreaterLength(superscript.Element1.OuterHeight, superscript.Element2.OuterHeight + superscript.SuperscriptOffset);

            SetSizesOfElement(superscript);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = superscript.OuterWidth / 2;
            centreAlignmentPoint.Y = superscript.Element1.OuterHeight / 2;

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
            textElement.SizeOfContent = _textMeasurer.MeasureTextSize(textElement.Content, textElement.FontStyle).ScaleX(0.85);

            textElement.Offset = _textMeasurer.MeasureTextSize(textElement.Content, textElement.FontStyle).Width * 0.025 + 0.05;

            SetSizesOfElement(textElement);

            var centreAlignmentPoint = new Position();

            centreAlignmentPoint.X = textElement.OuterWidth / 2;
            centreAlignmentPoint.Y = textElement.OuterHeight / 2;

            textElement.CentreAlignmentPoint = centreAlignmentPoint;
        }
    }
}
