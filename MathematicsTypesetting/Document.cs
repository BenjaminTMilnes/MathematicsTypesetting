using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Document
    {
        public Size Size { get; set; }
        public double Resolution { get; set; }
        public Element MainElement { get; set; }

        public Document()
        {
            Size = 0;
            Resolution = 0;
        }
    }
}
