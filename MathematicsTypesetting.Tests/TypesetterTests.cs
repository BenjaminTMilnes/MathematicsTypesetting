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
        public TestContext TestContext { get; set; }

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

            Assert.AreEqual(1, number1.SizeOfContent.Width);
            Assert.AreEqual(1, number1.SizeOfContent.Height);
            Assert.AreEqual(1, number1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(1, number1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(1, number1.SizeIncludingBorder.Width);
            Assert.AreEqual(1, number1.SizeIncludingBorder.Height);
            Assert.AreEqual(1, number1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(1, number1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetNumberSizeTest2()
        {
            var number1 = new Number();

            number1.Content = "123";

            _typesetter.SetNumberSize(number1);

            Assert.AreEqual(3, number1.SizeOfContent.Width);
            Assert.AreEqual(1, number1.SizeOfContent.Height);
            Assert.AreEqual(3, number1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(1, number1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(3, number1.SizeIncludingBorder.Width);
            Assert.AreEqual(1, number1.SizeIncludingBorder.Height);
            Assert.AreEqual(3, number1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(1, number1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetNumberSizeTest3()
        {
            var number1 = new Number();

            number1.Content = "123";
            number1.InnerMargin.Top = 1;
            number1.InnerMargin.Right = 2;
            number1.InnerMargin.Bottom = 3;
            number1.InnerMargin.Left = 4;

            _typesetter.SetNumberSize(number1);

            Assert.AreEqual(3, number1.SizeOfContent.Width);
            Assert.AreEqual(1, number1.SizeOfContent.Height);
            Assert.AreEqual(9, number1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(5, number1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(9, number1.SizeIncludingBorder.Width);
            Assert.AreEqual(5, number1.SizeIncludingBorder.Height);
            Assert.AreEqual(9, number1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(5, number1.SizeIncludingOuterMargin.Height);
        }
    }
}
