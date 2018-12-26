namespace MathematicsTypesetting
{
    public abstract class TextElement : Element
    {
        public string Content { get; set; }
        public Length Offset { get; set; }

        public Colour BackgroundColour { get; set; }

        public TextElement() : base()
        {
            Offset = 0;

            BackgroundColour = new Colour();
        }
    }
}
