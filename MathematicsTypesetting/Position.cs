using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    /// <summary>
    /// Represents the position of an element relative to the origin; a very simplified form of vector.
    /// </summary>
    public struct Position
    {
        public Length X { get; set; }
        public Length Y { get; set; }
    }
}
