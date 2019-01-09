using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;

[assembly: ExtensionApplication(typeof(DotnetAutocad.RibbonRun))]

namespace DotnetAutocad
{
    public class RibbonRun : IExtensionApplication
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }

    public class Ribbon
    {

    }
}
