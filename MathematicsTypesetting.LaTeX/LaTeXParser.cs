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
                GetIdentifier(subsection, elements, marker);
                GetNumber(subsection, elements, marker);
                GetOperator(subsection, elements, marker);
                GetSubscript(subsection, elements, marker);
                GetSuperscript(subsection, elements, marker);
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
                    if (n != "")
                    {
                        var number = new Number();

                        number.Content = n;

                        container.Add(number);

                        marker.Position += n.Length;
                    }

                    break;
                }
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

        public void GetGreekLetter(string latex, IList<Element> container, Marker marker)
        {
            var greekLetterCommands = new string[] { "alpha", "beta", "gamma", "delta", "epsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigma", "tau", "upsilon", "phi", "chi", "psi", "omega", "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" };

            var greekLetters = new string[] { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω" };

            for (var j = 0; j < greekLetterCommands.Length; j++)
            {
                var command = "\\" + greekLetterCommands[j];
                var letter = greekLetters[j];

                if (marker.Position < latex.Length - command.Length)
                {
                    if (latex.Substring(marker.Position, command.Length) == command)
                    {
                        var i = new Identifier();

                        i.Content = letter;

                        container.Add(i);

                        marker.Position += command.Length;

                        break;
                    }
                }
            }
        }

    }
}
