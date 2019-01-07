using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting.Fonts
{
    public class Glyph
    {
        public string Character { get; set; }
        public string Path { get; set; }
        public IList<PathCommand> PathCommands { get; set; }

        public Glyph()
        {
            PathCommands = new List<PathCommand>();
        }
    }
}
