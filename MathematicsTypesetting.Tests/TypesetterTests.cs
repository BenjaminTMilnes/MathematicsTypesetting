using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathematicsTypesetting.Tests
{
    [TestClass]
    public class TypesetterTests
    {
        private ITextMeasurer _textMeasurer;
        private Typesetter _typesetter;

        public TypesetterTests()
        {
            _textMeasurer = new TestTextMeasurer();
            _typesetter = new Typesetter(_textMeasurer);
        }

        [TestMethod]
        public void SetNumberSizeTest1()
        {
            var number1 = new Number();

            number1.Content = "1";

            _typesetter.SetNumberSize(number1);

            Assert.AreEqual(new Length(1, LengthUnits.Millimetres), number1.SizeOfContent.Width);
        }
    }
}
