using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadHelper01.DrawingHelper
{
    public static class TextHelper
    {
        public static DBText GetDBText(this DatabaseWorker worker, Point3d OP, string contents, double hight = 3.5)
        {
            var text = new DBText
            {
                Position = OP,

                TextString = contents,

                Height = hight,

                WidthFactor = 0.7,

                ColorIndex = ColorIndex.Green,
            };

            return text;
        }

        public static MText GetMText(this DatabaseWorker worker, Point3d OP, string contents, double hight = 3.5)
        {
            var text = new MText()
            {
                Location = OP,

                Contents = contents,

                TextHeight = hight,

                ColorIndex = ColorIndex.Green,
            };

            return text;
        }
    }
}
