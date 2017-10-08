using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Length
    {
        public double Quantity { get; set; }
        public LengthUnits Units { get; set; }

        public Length(double quantity, LengthUnits units)
        {
            Quantity = quantity;
            Units = units;
        }
    }
}
