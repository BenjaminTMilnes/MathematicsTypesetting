using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathematicsTypesetting;
using MathematicsTypesetting.LaTeX;

namespace MathematicsTypesetting.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var exampleMaker = new ExampleMaker();

            exampleMaker.MakeExamples();
        }
    }

    public class ExampleMaker
    {
        private TextMeasurer _textMeasurer;
        private Typesetter _typesetter;
        private PNGExporter _exporter;

        public ExampleMaker()
        {
            _textMeasurer = new TextMeasurer();
            _typesetter = new Typesetter(_textMeasurer);
            _exporter = new PNGExporter();
        }

        public void MakeExamples()
        {
            MakeExample1();
            MakeExample2();
            MakeExample3();
        }

        private void MakeExample1()
        {
            var document = new Document();
            var mathematicsLine = new MathematicsLine();
            var number1 = new Number();
            var number2 = new Number();
            var number3 = new Number();
            var number4 = new Number();

            number1.Content = "1";
            number2.Content = "23";
            number3.Content = "456";
            number4.Content = "7890";

            mathematicsLine.InnerMargin = 5;
            mathematicsLine.Elements.Add(number1);
            mathematicsLine.Elements.Add(number2);
            mathematicsLine.Elements.Add(number3);
            mathematicsLine.Elements.Add(number4);

            document.MainElement = mathematicsLine;

            _typesetter.TypesetDocument(document);

            var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "../../example1.png");

            _exporter.ExportMathematics(document, fileLocation);
        }

        private void MakeExample2()
        {
            var document = new Document();
            var mathematicsLine = new MathematicsLine();
            var number1 = new Number();
            var number2 = new Number();
            var number3 = new Number();
            var number4 = new Number();
            var identifier1 = new Identifier();
            var identifier2 = new Identifier();
            var identifier3 = new Identifier();
            var identifier4 = new Identifier();
            var identifier5 = new Identifier();
            var identifier6 = new Identifier();
            var operator1 = new BinomialOperator();
            var operator2 = new BinomialOperator();
            var fraction = new Fraction();
            var subscript1 = new Subscript();
            var superscript1 = new Superscript();

            number1.Content = "1";
            number2.Content = "23";
            number3.Content = "456";
            number4.Content = "7890";

            identifier1.Content = "a";
            identifier2.Content = "b";
            identifier3.Content = "x";
            identifier4.Content = "y";
            identifier5.Content = "c";
            identifier6.Content = "d";

            operator1.Content = "+";
            operator2.Content = "=";

            fraction.Numerator = identifier3;
            fraction.Denominator = identifier4;

            subscript1.Element1 = identifier1;
            subscript1.Element2 = identifier2;

            superscript1.Element1 = identifier5;
            superscript1.Element2 = identifier6;

            mathematicsLine.InnerMargin = 5;
            mathematicsLine.Elements.Add(number3);
            mathematicsLine.Elements.Add(operator1);
            mathematicsLine.Elements.Add(fraction);
            mathematicsLine.Elements.Add(operator2);
            mathematicsLine.Elements.Add(number4);
            mathematicsLine.Elements.Add(subscript1);
            mathematicsLine.Elements.Add(superscript1);

            document.MainElement = mathematicsLine;

            _typesetter.TypesetDocument(document);

            var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "../../example2.png");

            _exporter.ExportMathematics(document, fileLocation);
        }

        private void MakeExample3()
        {
            var formulae = new string[] { "E = hf", "E = 1/2 m v^{2}", "v_{c} = v_{a} + v_{b}", "E = \\frac{hc}{\\lambda}" };

            for (var i = 0; i < formulae.Length; i++)
            {
                var document = new Document();
                var parser = new LaTeXParser();

                document.MainElement = parser.ParseLaTeX(formulae[i]);

                _typesetter.TypesetDocument(document);

                var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "../../example" + (i + 3).ToString() + ".png");

                _exporter.ExportMathematics(document, fileLocation);
            }
        }
    }
}
