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

    public class UnableToConvertArbitraryLengthUnitsException : Exception
    {
        public override string ToString()
        {
            return "Cannot convert between arbitrary and non-arbitrary length units.";
        }
    }

    public struct Length
    {
        public double Quantity { get; set; }
        public LengthUnits Units { get; set; }

        public Length(double quantity, LengthUnits units)
        {
            Quantity = quantity;
            Units = units;
        }

        public Length(double quantity) : this(quantity, LengthUnits.Arbitrary) { }

        public static implicit operator Length(double i)
        {
            return new Length(i);
        }

        public static Length operator +(Length length1, Length length2)
        {
            return new Length(length1.Quantity + length2.ConvertToUnits(length1.Units).Quantity, length1.Units);
        }

        public static Length operator -(Length length1, Length length2)
        {
            return new Length(length1.Quantity - length2.ConvertToUnits(length1.Units).Quantity, length1.Units);
        }

        public static Length operator -(Length length)
        {
            return new Length(-length.Quantity, length.Units);
        }

        public static Length operator *(Length length, int scalar)
        {
            return new Length(length.Quantity * scalar, length.Units);
        }

        public static Length operator *(int scalar, Length length)
        {
            return length * scalar;
        }

        public static Length operator *(Length length, double scalar)
        {
            return new Length(length.Quantity * scalar, length.Units);
        }

        public static Length operator *(double scalar, Length length)
        {
            return length * scalar;
        }

        public static Length operator /(Length length, int scalar)
        {
            return new Length(length.Quantity / scalar, length.Units);
        }

        public static bool operator >(Length length1, Length length2)
        {
            return length1.Quantity > length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator <(Length length1, Length length2)
        {
            return length1.Quantity < length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator >=(Length length1, Length length2)
        {
            return length1.Quantity >= length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator <=(Length length1, Length length2)
        {
            return length1.Quantity <= length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator ==(Length length1, Length length2)
        {
            return length1.Quantity == length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator !=(Length length1, Length length2)
        {
            return length1.Quantity != length2.ConvertToUnits(length1.Units).Quantity;
        }

        public override bool Equals(object obj)
        {
            if (obj is Length)
            {
                var length = (Length)obj;

                if (length.Quantity == Quantity && length.Units == Units)
                {
                    return true;
                }
            }
            else if (obj is double)
            {
                return Equals(new Length((double)obj));
            }

            return false;
        }

        /// <summary>
        /// Converts this length into a new length with the given units.
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public Length ConvertToUnits(LengthUnits units)
        {
            if (Quantity == 0)
            {
                return new Length(0, units);
            }

            if ((Units == LengthUnits.Arbitrary && units != LengthUnits.Arbitrary) || (Units != LengthUnits.Arbitrary && units == LengthUnits.Arbitrary))
            {
                throw new UnableToConvertArbitraryLengthUnitsException();
            }

            if (Units == LengthUnits.Arbitrary && units == LengthUnits.Arbitrary)
            {
                return this;
            }

            var lengthInMillimetres = 0.0;
            var lengthInNewUnits = 0.0;

            if (Units == LengthUnits.Millimetres)
            {
                lengthInMillimetres = Quantity;
            }
            else if (Units == LengthUnits.Centimetres)
            {
                lengthInMillimetres = Quantity * 10;
            }
            else if (Units == LengthUnits.Decimetres)
            {
                lengthInMillimetres = Quantity * 100;
            }
            else if (Units == LengthUnits.Metres)
            {
                lengthInMillimetres = Quantity * 1000;
            }
            else if (Units == LengthUnits.Points)
            {
                lengthInMillimetres = Quantity * 0.3528;
            }
            else if (Units == LengthUnits.Inches)
            {
                lengthInMillimetres = Quantity * 25.4;
            }

            if (units == LengthUnits.Millimetres)
            {
                lengthInNewUnits = lengthInMillimetres;
            }
            else if (units == LengthUnits.Centimetres)
            {
                lengthInNewUnits = lengthInMillimetres / 10;
            }
            else if (units == LengthUnits.Decimetres)
            {
                lengthInNewUnits = lengthInMillimetres / 100;
            }
            else if (units == LengthUnits.Metres)
            {
                lengthInNewUnits = lengthInMillimetres / 1000;
            }
            else if (units == LengthUnits.Points)
            {
                lengthInNewUnits = lengthInMillimetres / 0.3528;
            }
            else if (units == LengthUnits.Inches)
            {
                lengthInNewUnits = lengthInMillimetres / 25.4;
            }

            return new Length(lengthInNewUnits, units);
        }

        public override string ToString()
        {
            return $"{Quantity} {Units}";
        }
    }
}
