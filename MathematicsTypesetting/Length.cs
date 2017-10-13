﻿using System;
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

        public static Length operator +(Length length1, Length length2)
        {
            return new Length(length1.Quantity + length2.ConvertToUnits(length1.Units).Quantity, length1.Units);
        }

        public static Length operator -(Length length1, Length length2)
        {
            return new Length(length1.Quantity - length2.ConvertToUnits(length1.Units).Quantity, length1.Units);
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
            return length1.Quantity <= length2.ConvertToUnits(length1.Units).Quantity;
        }

        public static bool operator !=(Length length1, Length length2)
        {
            return length1.Quantity != length2.ConvertToUnits(length1.Units).Quantity;
        }

        public override bool Equals(object obj)
        {
            if (obj is Length)
            {
                if ((obj as Length).Quantity == Quantity && (obj as Length).Units == Units)
                {
                    return true;
                }
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
    }
}
