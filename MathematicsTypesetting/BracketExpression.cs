using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public enum BracketType
    {
        Curve = 0, // ()
        Square = 1, // []
        Recurve = 2, // {}
        Angle = 3 // <>
    }

    public class BracketExpression : Element
    {
        public BracketType Type { get; set; }
        public MathematicsLine InnerExpression { get; set; }

        public BracketExpression() : base()
        {
            Type = BracketType.Curve;
            InnerExpression = new MathematicsLine();
        }

        public override void CascadeStyle(string name, string value)
        {
            base.CascadeStyle(name, value);

            InnerExpression.CascadeStyle(name, value);
        }
    }
}
