using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class UnableToConvertToLengthException : Exception
    {
        public object Object { get; private set; }

        public UnableToConvertToLengthException(object object1)
        {
            Object = object1;
        }

        public override string ToString()
        {
            return $"Cannot implicitly convert {Object.ToString()} to a length.";
        }
    }

    public class Length
    {
        public double Quantity { get; set; }
        public LengthUnits Units { get; set; }

        public Length(double quantity, LengthUnits units)
        {
            Quantity = quantity;
            Units = units;
        }

        public static implicit operator Length(int i)
        {
            if (i == 0)
            {
                return new Length(0, LengthUnits.Millimetres);
            }

            throw new UnableToConvertToLengthException(i);
        }
    }
}
