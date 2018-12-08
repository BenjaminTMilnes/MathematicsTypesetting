namespace MathematicsTypesetting
{
    public abstract class TextElement : Element
    {
        public string Content { get; set; }

        public Colour BackgroundColour { get; set; }

        public TextElement() : base()
        {
            BackgroundColour = new Colour();
        }
    }
}
