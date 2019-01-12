using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathematicsTypesetting;
using MathematicsTypesetting.Fonts;
using MathematicsTypesetting.LaTeX;

namespace MathematicsTypesetting.Examples
{
    public sealed class Renderer
    {
        private FontLoader _fontLoader;
        private ITextMeasurer _textMeasurer;
        private Typesetter _typesetter;
        private PNGExporter _exporter;
        private LaTeXParser _parser;

        public Renderer()
        {
            _fontLoader = new FontLoader();
            _textMeasurer = new HyperfontTextMeasurer(_fontLoader);
            _typesetter = new Typesetter(_textMeasurer);
            _exporter = new PNGExporter(_fontLoader);
            _parser = new LaTeXParser();
        }

        public void RenderMathematics(string latex, string outputFileLocation)
        {
            var document = new Document();

            document.MainElement = _parser.ParseLaTeX(latex);

            _typesetter.TypesetDocument(document);
            _exporter.ExportMathematics(document, outputFileLocation);
        }
    }
}
