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

            DrawConstructionLines = true;
        }
    }
}
