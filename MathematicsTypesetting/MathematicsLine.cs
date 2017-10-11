using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    /// <summary>
    /// Represents a line of mathematics where the elements in the line are aligned vertically.
    /// </summary>
    public class MathematicsLine
    {
        public IList<Element> Elements { get; set; }

        public MathematicsLine()
        {
            Elements = new List<Element>();
        }
    }
}
