namespace MathematicsTypesetting
{
    public class Element
    {
        public Position Position { get; set; }

        public Size SizeOfContent { get; set; }
        public Size SizeIncludingInnerMargin { get; set; }
        public Size SizeIncludingBorder { get; set; }
        public Size SizeIncludingOuterMargin { get; set; }

        public Position CentreAlignmentPoint { get; set; }

        public Margin OuterMargin { get; set; }
        public Border Border { get; set; }
        public Margin InnerMargin { get; set; }

        public FontStyle FontStyle { get; set; }

        public bool DrawConstructionLines { get; set; }

        public Element()
        {
            Position = new Position();

            SizeOfContent = 0;
            SizeIncludingInnerMargin = 0;
            SizeIncludingBorder = 0;
            SizeIncludingOuterMargin = 0;

            CentreAlignmentPoint = new Position();

            OuterMargin = new Margin();
            Border = new Border();
            InnerMargin = new Margin();

            FontStyle = new FontStyle();

            DrawConstructionLines = true;
        }

        public Length TopWidth { get { return OuterMargin.Top + Border.Width + InnerMargin.Top; } }
        public Length RightWidth { get { return OuterMargin.Right + Border.Width + InnerMargin.Right; } }
        public Length BottomWidth { get { return OuterMargin.Bottom + Border.Width + InnerMargin.Bottom; } }
        public Length LeftWidth { get { return OuterMargin.Left + Border.Width + InnerMargin.Left; } }

        public Length ContentWidth { get { return SizeOfContent.Width; } }
        public Length ContentHeight { get { return SizeOfContent.Height; } }
        public Length OuterWidth { get { return SizeIncludingOuterMargin.Width; } }
        public Length OuterHeight { get { return SizeIncludingOuterMargin.Height; } }
    }
}
