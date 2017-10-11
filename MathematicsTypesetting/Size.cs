using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class UnableToConvertToSizeException : Exception
    {
        public object Object { get; private set; }

        public UnableToConvertToSizeException(object object1)
        {
            Object = object1;
        }

        public override string ToString()
        {
            return $"Cannot implicitly convert {Object.ToString()} to a size.";
        }
    }

    public class Size
    {
        public Length Width { get; set; }
        public Length Height { get; set; }

        public Size()
        {
            Width = 0;
            Height = 0;
        }

        public static implicit operator Size(int i)
        {
            if (i == 0)
            {
                return new Size();
            }

            throw new UnableToConvertToSizeException(i);
        }
    }
}
