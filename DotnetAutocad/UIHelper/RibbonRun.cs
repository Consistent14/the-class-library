using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Customization;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using RibbonButton = Autodesk.AutoCAD.Customization.RibbonButton;
using RibbonControl = Autodesk.Windows.RibbonControl;
using RibbonPanelSource = Autodesk.AutoCAD.Customization.RibbonPanelSource;

namespace DotnetAutocad.UIHelper
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

        void AddRibbon()
        {

        }
    }
}
