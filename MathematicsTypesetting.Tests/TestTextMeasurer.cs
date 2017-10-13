using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathematicsTypesetting;

namespace MathematicsTypesetting.Tests
{
    public class TestTextMeasurer : ITextMeasurer
    {
        public Size MeasureTextSize(string text, FontStyle fontStyle)
        {
            var size = new Size();

            size.Width = new Length(text.Length);
            size.Height = new Length(1);

            return size;
        }
    }
}
