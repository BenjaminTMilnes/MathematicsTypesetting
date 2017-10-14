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

        [TestMethod]
        public void SetNumberSizeTest4()
        {
            var number1 = new Number();

            number1.Content = "123";
            number1.InnerMargin.Top = 1;
            number1.InnerMargin.Right = 2;
            number1.InnerMargin.Bottom = 1;
            number1.InnerMargin.Left = 2;
            number1.Border.Width = 5;

            _typesetter.SetNumberSize(number1);

            Assert.AreEqual(3, number1.SizeOfContent.Width);
            Assert.AreEqual(1, number1.SizeOfContent.Height);
            Assert.AreEqual(7, number1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(3, number1.SizeIncludingInnerMargin.Height);

            TestContext.WriteLine(number1.SizeIncludingBorder.Width.ToString());
            TestContext.WriteLine(number1.SizeIncludingBorder.Height.ToString());

            Assert.AreEqual(17, number1.SizeIncludingBorder.Width);
            Assert.AreEqual(13, number1.SizeIncludingBorder.Height);
            Assert.AreEqual(17, number1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(13, number1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetNumberSizeTest5()
        {
            var number1 = new Number();

            number1.Content = "123";
            number1.InnerMargin.Top = 1;
            number1.InnerMargin.Right = 2;
            number1.InnerMargin.Bottom = 1;
            number1.InnerMargin.Left = 2;
            number1.Border.Width = 5;
            number1.OuterMargin.Top = 1;
            number1.OuterMargin.Right = 1;
            number1.OuterMargin.Bottom = 1;
            number1.OuterMargin.Left = 1;

            _typesetter.SetNumberSize(number1);

            Assert.AreEqual(3, number1.SizeOfContent.Width);
            Assert.AreEqual(1, number1.SizeOfContent.Height);
            Assert.AreEqual(7, number1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(3, number1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(17, number1.SizeIncludingBorder.Width);
            Assert.AreEqual(13, number1.SizeIncludingBorder.Height);
            Assert.AreEqual(19, number1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(15, number1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetMathematicsLineSize1()
        {
            var mathematicsLine1 = new MathematicsLine();

            _typesetter.SetMathematicsLineSize(mathematicsLine1);

            Assert.AreEqual(0, mathematicsLine1.SizeOfContent.Width);
            Assert.AreEqual(0, mathematicsLine1.SizeOfContent.Height);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingBorder.Width);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingBorder.Height);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(0, mathematicsLine1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetMathematicsLineSize2()
        {
            var mathematicsLine1 = new MathematicsLine();
            var number1 = new Number();

            number1.Content = "123";

            mathematicsLine1.Elements.Add(number1);

            _typesetter.SetMathematicsLineSize(mathematicsLine1);

            Assert.AreEqual(3, mathematicsLine1.SizeOfContent.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeOfContent.Height);
            Assert.AreEqual(3, mathematicsLine1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(3, mathematicsLine1.SizeIncludingBorder.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingBorder.Height);
            Assert.AreEqual(3, mathematicsLine1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingOuterMargin.Height);
        }

        [TestMethod]
        public void SetMathematicsLineSize3()
        {
            var mathematicsLine1 = new MathematicsLine();
            var number1 = new Number();
            var number2 = new Number();

            number1.Content = "123";
            number2.Content = "45";

            mathematicsLine1.Elements.Add(number1);
            mathematicsLine1.Elements.Add(number2);

            _typesetter.SetMathematicsLineSize(mathematicsLine1);

            Assert.AreEqual(5, mathematicsLine1.SizeOfContent.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeOfContent.Height);
            Assert.AreEqual(5, mathematicsLine1.SizeIncludingInnerMargin.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingInnerMargin.Height);
            Assert.AreEqual(5, mathematicsLine1.SizeIncludingBorder.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingBorder.Height);
            Assert.AreEqual(5, mathematicsLine1.SizeIncludingOuterMargin.Width);
            Assert.AreEqual(1, mathematicsLine1.SizeIncludingOuterMargin.Height);
        }
    }
}
