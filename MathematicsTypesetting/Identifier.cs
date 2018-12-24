namespace MathematicsTypesetting
{
    public class Identifier : TextElement
    {
        public Identifier() : base()
        {
            FontStyle.FontEmphasis = FontEmphasis.Italic;

            InnerMargin.Left = 0;
            InnerMargin.Right = 0;
        }
    }
}
