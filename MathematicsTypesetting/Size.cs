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

        public static Size operator +(Size size, Margin margin)
        {
            var newSize = new Size();

            newSize.Width = size.Width + margin.Left + margin.Right;
            newSize.Height = size.Height + margin.Top + margin.Bottom;

            return newSize;
        }

        public static Size operator +(Margin margin, Size size)
        {
            return size + margin;
        }

        public static Size operator +(Size size, Border border)
        {
            var newSize = new Size();

            newSize.Width = size.Width + border.Width + border.Width;
            newSize.Height = size.Height + border.Width + border.Width;

            return newSize;
        }

        public static Size operator +(Border border, Size size)
        {
            return size + border;
        }

        public static Size operator *(Size size, int scalar)
        {
            var newSize = new Size();

            newSize.Width = size.Width * scalar;
            newSize.Height = size.Height * scalar;

            return newSize;
        }

        public static Size operator *(int scalar, Size size)
        {
            return size * scalar;
        }

        public static Size operator *(Size size, double scalar)
        {
            var newSize = new Size();

            newSize.Width = size.Width * scalar;
            newSize.Height = size.Height * scalar;

            return newSize;
        }

        public static Size operator *(double scalar, Size size)
        {
            return size * scalar;
        }

        public Size ScaleX(double scalar)
        {
            var newSize = new Size();

            newSize.Width = Width * scalar;
            newSize.Height = Height;

            return newSize;
        }

        public Size ScaleY(double scalar)
        {
            var newSize = new Size();

            newSize.Width = Width;
            newSize.Height = Height * scalar;

            return newSize;
        }
    }
}
