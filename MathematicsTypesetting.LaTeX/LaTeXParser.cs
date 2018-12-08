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

    class LaTeXParser
    {

  public IList< Element>  ParseSubsection(string subsection)
        {

        }

    public void GetGreekLetter( string latex, IList<Element> container, Marker marker)
        {
            var greekLetters = new string[] {"alpha", "beta" , "gamma", "delta", "epsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigma", "tau", "upsilon", "phi", "chi", "psi", "omega", "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" };

            foreach (var letter in greekLetters)
            {
                if ( marker.Position < latex.Length - letter.Length - 1)
                {
                     if (latex.Substring(marker.Position, letter.Length + 1) == "\\" + letter)
                    {
                        var i = new Identifier();

                        i.Content = letter;

                        container.Add(i);

                        break;
                    }
                }
            }
        }
        
    }
}
