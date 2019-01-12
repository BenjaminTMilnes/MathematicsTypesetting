using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathematicsTypesetting;

namespace MathematicsTypesetting.LaTeX
{
    public class Marker
    {
        public int Position { get; set; }
    }

    public class LaTeXParser
    {
        public MathematicsLine ParseLaTeX(string latex)
        {
            var line = new MathematicsLine();

            line.Elements = ParseSubsection(latex);

            return line;
        }

        public IList<Element> ParseSubsection(string subsection)
        {
            var marker = new Marker();

            marker.Position = 0;

            var elements = new List<Element>();

            while (marker.Position < subsection.Length)
            {
                var n = marker.Position;

                GetGreekLetter(subsection, elements, marker);
                GetNamedFunction(subsection, elements, marker);
                GetIdentifier(subsection, elements, marker);
                GetNumber(subsection, elements, marker);
                GetOperator(subsection, elements, marker);
                GetSubscript(subsection, elements, marker);
                GetSuperscript(subsection, elements, marker);
                GetFraction(subsection, elements, marker);
                GetMathRoman(subsection, elements, marker);
                GetTextBold(subsection, elements, marker);
                GetBracketExpression(subsection, elements, marker);
                GetBracket(subsection, elements, marker);
                GetPunctuationMark(subsection, elements, marker);
                GetWhitespace(subsection, elements, marker);

                if (marker.Position == n)
                {
                    break;
                }
            }

            return elements;
        }

        public void GetSubscript(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            if (latex.Substring(marker.Position, 1) == "_")
            {
                marker.Position += 1;

                var subsection = GetSubsection(latex, container, marker);

                if (subsection != null)
                {
                    var subscript = new Subscript();
                    var line = new MathematicsLine();

                    subscript.Element1 = container.Last();

                    if (subsection.Item2.Count() == 1)
                    {
                        subscript.Element2 = subsection.Item2.First();
                    }
                    else
                    {
                        line.Elements = subsection.Item2;
                        subscript.Element2 = line;
                    }

                    container.Remove(container.Last());
                    container.Add(subscript);

                    marker.Position += subsection.Item1;
                }
                else
                {
                    var elements = new List<Element>();

                    GetGreekLetter(latex, elements, marker);
                    GetIdentifier(latex, elements, marker);
                    GetNumber(latex, elements, marker);
                    GetOperator(latex, elements, marker);
                    GetMathRoman(latex, elements, marker);
                    GetPunctuationMark(latex, elements, marker);

                    if (elements.Any())
                    {
                        var subscript = new Subscript();

                        subscript.Element1 = container.Last();
                        subscript.Element2 = elements.First();

                        container.Remove(container.Last());
                        container.Add(subscript);
                    }
                }
            }
        }

        public void GetSuperscript(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            if (latex.Substring(marker.Position, 1) == "^")
            {
                marker.Position += 1;

                var subsection = GetSubsection(latex, container, marker);

                if (subsection != null)
                {
                    var superscript = new Superscript();
                    var line = new MathematicsLine();

                    superscript.Element1 = container.Last();

                    if (subsection.Item2.Count() == 1)
                    {
                        superscript.Element2 = subsection.Item2.First();
                    }
                    else
                    {
                        line.Elements = subsection.Item2;
                        superscript.Element2 = line;
                    }

                    container.Remove(container.Last());
                    container.Add(superscript);

                    marker.Position += subsection.Item1;
                }
                else
                {
                    var elements = new List<Element>();

                    GetGreekLetter(latex, elements, marker);
                    GetIdentifier(latex, elements, marker);
                    GetNumber(latex, elements, marker);
                    GetOperator(latex, elements, marker);
                    GetMathRoman(latex, elements, marker);
                    GetPunctuationMark(latex, elements, marker);

                    if (elements.Any())
                    {
                        var superscript = new Superscript();

                        superscript.Element1 = container.Last();
                        superscript.Element2 = elements.First();

                        container.Remove(container.Last());
                        container.Add(superscript);
                    }
                }
            }
        }

        public void GetFraction(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length || latex.Length - marker.Position < 5)
            {
                return;
            }

            if (latex.Substring(marker.Position, 5) == "\\frac")
            {
                marker.Position += 5;

                var fraction = new Fraction();

                var subsection1 = GetSubsection(latex, container, marker);

                if (subsection1 != null)
                {
                    var line1 = new MathematicsLine();

                    if (subsection1.Item2.Count() == 1)
                    {
                        fraction.Numerator = subsection1.Item2.First();
                    }
                    else
                    {
                        line1.Elements = subsection1.Item2;

                        fraction.Numerator = line1;
                    }

                    marker.Position += subsection1.Item1;
                }

                var subsection2 = GetSubsection(latex, container, marker);

                if (subsection2 != null)
                {
                    var line2 = new MathematicsLine();

                    if (subsection2.Item2.Count() == 1)
                    {
                        fraction.Denominator = subsection2.Item2.First();
                    }
                    else
                    {
                        line2.Elements = subsection2.Item2;

                        fraction.Denominator = line2;
                    }

                    marker.Position += subsection2.Item1;
                }

                if (fraction.Numerator != null && fraction.Denominator != null)
                {
                    container.Add(fraction);
                }
            }
        }

        public void GetBracketExpression(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position < latex.Length - 13)
            {
                if (latex.Substring(marker.Position, 6) == "\\left(")
                {
                    var n = 1;
                    var i = 0;
                    var j = marker.Position + 6;

                    while (j < latex.Length)
                    {
                        if (latex.Substring(j, 6) == "\\left(")
                        {
                            n += 1;
                            i += 6;
                            j += 6;
                        }
                        if (latex.Substring(j, 7) == "\\right)")
                        {
                            n -= 1;
                            i += 7;
                            j += 7;
                        }
                        else
                        {
                            i += 1;
                            j += 1;
                        }

                        if (n == 0)
                        {
                            break;
                        }
                    }

                    var t = latex.Substring(marker.Position + 6, i - 7);

                    var bracketExpression = new BracketExpression();
                    var mathematicsLine = new MathematicsLine();

                    mathematicsLine.Elements = ParseSubsection(t);
                    bracketExpression.InnerExpression = mathematicsLine;

                    container.Add(bracketExpression);

                    marker.Position += 6 + i;
                }
            }
        }

        public Tuple<int, IList<Element>> GetSubsection(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return null;
            }

            if (latex.Substring(marker.Position, 1) == "{")
            {
                var n = 1;
                var i = 0;

                for (var j = marker.Position + 1; j < latex.Length; j++)
                {
                    if (latex.Substring(j, 1) == "{")
                    {
                        n += 1;
                    }
                    if (latex.Substring(j, 1) == "}")
                    {
                        n -= 1;
                    }

                    i += 1;

                    if (n == 0)
                    {
                        break;
                    }
                }

                var t = latex.Substring(marker.Position + 1, i - 1);

                return new Tuple<int, IList<Element>>(i + 1, ParseSubsection(t));
            }

            return null;
        }

        public void GetIdentifier(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Any(l => l.ToString() == latex.Substring(marker.Position, 1)))
            {
                var i = new Identifier();

                i.Content = latex.Substring(marker.Position, 1);

                container.Add(i);

                marker.Position += 1;
            }
        }

        public void GetNumber(string latex, IList<Element> container, Marker marker)
        {
            var n = "";

            for (var i = marker.Position; i < latex.Length; i++)
            {
                if ("0123456789.".Any(l => l.ToString() == latex.Substring(i, 1)))
                {
                    n += latex.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }

            if (n != "")
            {
                var number = new Number();

                number.Content = n;

                container.Add(number);

                marker.Position += n.Length;
            }
        }

        public void GetOperator(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            if ("+-=/".Any(l => l.ToString() == latex.Substring(marker.Position, 1)))
            {
                var o = new BinomialOperator();

                o.Content = latex.Substring(marker.Position, 1);

                if (o.Content == "-")
                {
                    o.Content = "−";
                }

                container.Add(o);

                marker.Position += 1;
            }
        }

        public void GetWhitespace(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            if (latex.Substring(marker.Position, 1) == " ")
            {
                marker.Position += 1;
            }
        }

        public void GetBracket(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position >= latex.Length)
            {
                return;
            }

            var brackets = new string[] { "(", ")", "{", "}", "[", "]" };

            foreach (var bracket in brackets)
            {
                if (latex.Substring(marker.Position, 1) == bracket)
                {
                    var b = new Bracket();

                    b.Content = bracket;

                    container.Add(b);

                    marker.Position += 1;

                    break;
                }
            }
        }

        public void GetGreekLetter(string latex, IList<Element> container, Marker marker)
        {
            var greekLetterCommands = new string[] { "alpha", "beta", "gamma", "delta", "epsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigma", "tau", "upsilon", "phi", "chi", "psi", "omega", "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega", "prime", "hbar", "partial", "nabla", "times", "cdot", "leq", "geq", "propto" };

            var greekLetters = new string[] { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "′", "ħ", "∂", "∇", "×", "·", "≤", "≥", "∝" };

            var uppercaseGreekLetters = new string[] { "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "∇", "×", "·", "≤", "≥", "∝" };

            for (var j = 0; j < greekLetterCommands.Length; j++)
            {
                var command = "\\" + greekLetterCommands[j];
                var letter = greekLetters[j];

                if (marker.Position <= latex.Length - command.Length)
                {
                    if (latex.Substring(marker.Position, command.Length) == command)
                    {
                        var i = new Identifier();

                        i.Content = letter;

                        if (uppercaseGreekLetters.Any(l => l == letter))
                        {
                            i.FontStyle.FontEmphasis = FontEmphasis.None;
                        }

                        container.Add(i);

                        marker.Position += command.Length;

                        break;
                    }
                }
            }
        }

        public void GetNamedFunction(string latex, IList<Element> container, Marker marker)
        {
            var functionNames = new string[] { "sin", "cos", "tan" };

            for (var j = 0; j < functionNames.Length; j++)
            {
                var command = "\\" + functionNames[j];
                var name = functionNames[j];

                if (marker.Position <= latex.Length - command.Length)
                {
                    if (latex.Substring(marker.Position, command.Length) == command)
                    {
                        var f = new NamedFunction();

                        f.Content = name;

                        container.Add(f);

                        marker.Position += command.Length;

                        break;
                    }
                }
            }
        }

        public void GetPunctuationMark(string latex, IList<Element> container, Marker marker)
        {
            var punctuationMarks = ",.?!~:;%";

            if (marker.Position >= latex.Length)
            {
                return;
            }

            if (punctuationMarks.Any(l => l.ToString() == latex.Substring(marker.Position, 1)))
            {
                var t = new Text();

                t.Content = latex.Substring(marker.Position, 1);

                container.Add(t);

                marker.Position += 1;
            }
        }

        public void GetMathRoman(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position <= latex.Length - 7)
            {
                if (latex.Substring(marker.Position, 7) == "\\mathrm")
                {
                    marker.Position += 7;

                    var line = new MathematicsLine();

                    var subsection = GetSubsection(latex, container, marker);

                    if (subsection != null)
                    {
                        line.Elements = subsection.Item2;

                        marker.Position += subsection.Item1;

                        line.CascadeStyle("FontEmphasis", "None");

                        container.Add(line);
                    }
                }
            }
        }

        public void GetTextBold(string latex, IList<Element> container, Marker marker)
        {
            if (marker.Position <= latex.Length - 7)
            {
                if (latex.Substring(marker.Position, 7) == "\\textbf")
                {
                    marker.Position += 7;

                    var line = new MathematicsLine();

                    var subsection = GetSubsection(latex, container, marker);

                    if (subsection != null)
                    {
                        line.Elements = subsection.Item2;

                        marker.Position += subsection.Item1;

                        line.CascadeStyle("FontWeight", "Bold");

                        container.Add(line);
                    }
                }
            }
        }
    }
}
