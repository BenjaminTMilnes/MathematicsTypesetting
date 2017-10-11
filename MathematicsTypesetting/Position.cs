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
    public class Position
    {
        public Length X { get; set; }
        public Length Y { get; set; }

        public Position()
        {
            X = new Length(0, LengthUnits.Millimetres);
            Y = new Length(0, LengthUnits.Millimetres);
        }
    }
}
