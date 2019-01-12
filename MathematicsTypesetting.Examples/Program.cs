using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathematicsTypesetting;
using MathematicsTypesetting.LaTeX;
using MathematicsTypesetting.Fonts;
using MathematicsTypesetting.Rendering;

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
        private Renderer _renderer;

        public ExampleMaker()
        {
            _renderer = new Renderer();
        }

        public void MakeExamples()
        {
            var formulae = new string[] {"1 23 456","456 + \\frac{x}{y} = 7890 a_{c} b^{d}",
                "E = hf",
                "E = \\frac{1}{2} m v^{2}",
                "v_{c} = v_{a} + v_{b}",
                "E = \\frac{hc}{\\lambda}",
                "F = \\frac{mv^2}{r}",
                "\\lambda^{\\prime} - \\lambda = \\frac{2 \\pi \\hbar}{mc} \\left( 1 - \\cos \\theta \\right)",
                "F = k_{e} \\frac{q_1 q_2}{r^2}",
                "\\alpha = \\frac{\\mathrm{d} \\omega}{\\mathrm{d} t}",
                "E = \\gamma m_0 c^2",
                "i \\hbar \\frac{\\partial}{\\partial t} \\Psi (\\textbf{r}, t) = \\left( \\frac{\\hbar^{2}}{2 \\mu} \\nabla^{2} + V (\\textbf{r}, t) \\right) \\Psi (\\textbf{r}, t)",
                "\\Delta x \\Delta p \\geq \\frac{\\hbar}{2}",
                "\\nabla \\cdot B = 0"};

            for (var i = 0; i < formulae.Length; i++)
            {
                if (formulae[i] != "")
                {
                    var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "../../example" + (i + 1).ToString() + ".png");

                    _renderer.RenderMathematics(formulae[i], fileLocation);
                }
            }
        }
    }
}
