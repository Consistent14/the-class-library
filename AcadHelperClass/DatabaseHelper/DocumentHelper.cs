using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;

namespace AcadHelper01.DatabaseHelper
{
    public static class DocumentHelper
    {
        public static Document GetMdiActiveDocument()
        {
            return Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
        }
      
    }
}
