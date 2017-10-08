using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public interface ITextMeasurer
    {
        Size MeasureTextSize(string text, FontStyle fontStyle);
    }
}
