using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using WinApp = System.Windows.Application;
using res = AcadHelper01.UIHelper.RibbonImage;
using System.Drawing;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media;
using AcadHelper01.UIHelper;
using System.Windows.Input;
using System.Windows.Controls;

namespace AcadHelper01.UIHelper
{
    public static class RibbonHelper
    {
        //面板类     

        public static RibbonPanel GetRibbonPanel(this RibbonPanelSource ribPanelSource)
        {
            var ribPanel = new RibbonPanel()
            {
                Source = ribPanelSource,
            };

            return ribPanel;
        }

        public static void AddItem(this RibbonPanelSource ribbonPanelSource, RibbonItem ribbonItem)
        {
            ribbonPanelSource.Items.Add(ribbonItem);
        }

        public static RibbonTab GetRibbonTab(string RibbonTabTitle, string RibbonTabID, bool isActive = true)
        {
            var ribTab = new RibbonTab()
            {
                Title = RibbonTabTitle,

                Id = RibbonTabID,
            };

            ribTab.IsActive = isActive;

            return ribTab;
        }

        //控件类

        public static RibbonButton GetRibbonButton(string text, Icon icon, ICommand command, RibbonItemSize ribbonItemSize = RibbonItemSize.Standard)
        {
            var bitmap = icon.ToBitmap();

            var ribButton = new RibbonButton()
            {
                Name = text,

                Text = text,

                ShowText = true,

                Image = bitmap.ChangeBitmapToImageSource(),

                ShowImage = true,

                Size = ribbonItemSize,

                LargeImage = bitmap.ChangeBitmapToImageSource(),

                Orientation = Orientation.Vertical,

                CommandHandler = command,
            };

            return ribButton;
        }

        public static void AddAttachContents(this RibbonButton ribbonButton,string RibbonButtonToolTip,string RibbonButtonDescription)
        {
            ribbonButton.ToolTip = RibbonButtonToolTip;

            ribbonButton.Description = RibbonButtonDescription;
        }

        public static void AddRibbonButton(string text, Bitmap bitmap, ICommand command, RibbonItemSize ribbonItemSize = RibbonItemSize.Standard)
        {
            var ribButton = new RibbonButton()
            {
                Text = text,

                ShowText = true,

                Image = bitmap.ChangeBitmapToImageSource(),

                ShowImage = true,

                Size = ribbonItemSize,

                CommandHandler = command,
            };
        }

        public static RibbonCheckBox GetRibbonCheckBox()
        {
            return new RibbonCheckBox();
        }

        public static RibbonCombo GetRibbonCombo()
        {
            return new RibbonCombo();
        }

        public static RibbonToggleButton GetRibbonToggleButton()
        {
            return new RibbonToggleButton();
        }

        public static void AddItem(this RibbonSplitButton ribbonSplitButton,RibbonItem ribbonItem)
        {
            ribbonSplitButton.Items.Add(ribbonItem);
        }
    }
}
