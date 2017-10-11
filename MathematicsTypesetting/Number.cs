﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsTypesetting
{
    public class Number : Element
    {
        public string Content { get; set; }

        public Colour BackgroundColour { get; set; }

        public FontStyle FontStyle { get; set; }

        public Number() : base()
        {
            BackgroundColour = new Colour();

            FontStyle = new FontStyle();
        }
    }
}
