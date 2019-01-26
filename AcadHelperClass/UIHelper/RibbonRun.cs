using System;
using System.Windows.Input;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using resource = AcadHelper01.UIHelper.RibbonImage;
using AcadHelper01.UIHelper;
using Autodesk.AutoCAD.EditorInput;
using AcadHelper01.DatabaseHelper;
using System.Windows.Controls;

namespace AcadHelper01.UIHelper
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


        [CommandMethod("ADDRIB")]
        public void AddRibbon()
        {
            var rbControl = ComponentManager.Ribbon;

            var date = DateTime.Now;

            var mid = date.Millisecond;

            var ribTab = RibbonHelper.GetRibbonTab($"{1}", $"{mid + 2}");

            rbControl.Tabs.Add(ribTab);

            var ribPanelSource = new RibbonPanelSource()
            {
                Title = $"{mid + 5}"
            };

            var ribPanel = ribPanelSource.GetRibbonPanel();

            var ribBtn1 = RibbonHelper.GetRibbonButton("btn", resource.Move, new MyCommand(), RibbonItemSize.Large);

            ribBtn1.AddAttachContents("a", "b");

            ribPanelSource.AddItem(ribBtn1);

            RibbonSplitButton ribSplitBtn = new RibbonSplitButton()
            {
                Text = "文字",
                ShowText = true,
                ShowImage = true,
                Size = RibbonItemSize.Large,
                Orientation = Orientation.Vertical,
            };

            ribSplitBtn.AddItem(ribBtn1);
            ribSplitBtn.AddItem(ribBtn1);
            ribSplitBtn.AddItem(ribBtn1);

            ribPanelSource.AddItem(ribSplitBtn);

            //创建行面板

            ribTab.Panels.Add(ribPanel);
        }
    }
    public class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var editor = DocumentHelper.GetMdiActiveDocument().Editor;

            editor.WriteMessage("command sucess~~~");
        }
    }
}
