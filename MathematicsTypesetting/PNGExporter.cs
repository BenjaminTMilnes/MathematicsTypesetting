using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace MathematicsTypesetting
{
    public class PNGExporter : Exporter
    {
        public void ExportMathematics(Document document, string fileLocation)
        {
            using (var bitmap = new Bitmap(100, 100))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {

                }

                bitmap.Save(fileLocation, ImageFormat.Png);
            }
        }
    }
}
