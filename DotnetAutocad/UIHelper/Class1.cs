using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Windows;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;

namespace DotnetAutocad.UIHelper
{
    public class Class1
    {
        private const string MY_TAB_ID = "MY_TAB_ID";

        [CommandMethod("addMyRibbon")]
        public void createRibbon()
        {
            Autodesk.Windows.RibbonControl ribCntrl =
                      Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;
            //can also be Autodesk.Windows.ComponentManager.Ribbon;     

            //add the tab
            RibbonTab ribTab = new RibbonTab();
            ribTab.Title = "My custom tab";
            ribTab.Id = MY_TAB_ID;
            ribCntrl.Tabs.Add(ribTab);

            //create and add both panels
            addPanel1(ribTab);
            addPanel2(ribTab);

            //set as active tab
            ribTab.IsActive = true;
        }

        private void addPanel2(RibbonTab ribTab)
        {
            //create the panel source
            RibbonPanelSource ribPanelSource = new RibbonPanelSource();
            ribPanelSource.Title = "Edit Registry";

            //create the panel
            RibbonPanel ribPanel = new RibbonPanel();
            ribPanel.Source = ribPanelSource;
            ribTab.Panels.Add(ribPanel);

            //create button1
            RibbonButton ribButtonDrawCircle = new RibbonButton();
            ribButtonDrawCircle.Text = "My Draw Circle";
            ribButtonDrawCircle.ShowText = true;
            //pay attention to the SPACE after the command name
            ribButtonDrawCircle.CommandParameter = "DrawCircle ";
            ribButtonDrawCircle.CommandHandler = new AdskCommandHandler();

            ribPanelSource.Items.Add(ribButtonDrawCircle);

        }

        private void addPanel1(RibbonTab ribTab)
        {
            //throw new NotImplementedException();
        }

        [CommandMethod("DrawCircle")]
        public void DrawCircle()
        {
            //画个圆，实现在此略去，这不是这篇blog的重点。
        }
    }
    class AdskCommandHandler : System.Windows.Input.ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //is from Ribbon Button
            RibbonButton ribBtn = parameter as RibbonButton;
            if (ribBtn != null)
            {
                //execute the command 
                Autodesk.AutoCAD.ApplicationServices.Application
                  .DocumentManager.MdiActiveDocument
                  .SendStringToExecute(
                     (string)ribBtn.CommandParameter, true, false, true);
            }
        }
    }
}
