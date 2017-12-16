using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathematicsTypesetting;

namespace MathematicsTypesetting.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var document = new Document();
            var mathematicsLine = new MathematicsLine();
            var number1 = new Number();
            var number2 = new Number();
            var number3 = new Number();

            number1.Content = "1";
            number2.Content = "23";
            number3.Content = "456";

            mathematicsLine.InnerMargin = 5;
            mathematicsLine.Elements.Add(number1);
            mathematicsLine.Elements.Add(number2);
            mathematicsLine.Elements.Add(number3);

            document.MainElement = mathematicsLine;

            var textMeasurer = new TextMeasurer();
            var typesetter = new Typesetter(textMeasurer);

            typesetter.TypesetDocument(document);

            var exporter = new PNGExporter();

            var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "example1.png");

            exporter.ExportMathematics(document, fileLocation);
        }
    }
}
