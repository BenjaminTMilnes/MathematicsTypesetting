using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class SquareRoot : Element
    {
        public MathematicsLine InnerExpression { get; set; }

        public SquareRoot() : base()
        {
            InnerExpression = new MathematicsLine();
        }

        public override void CascadeStyle(string name, string value)
        {
            base.CascadeStyle(name, value);

            InnerExpression.CascadeStyle(name, value);
        }
    }
}
