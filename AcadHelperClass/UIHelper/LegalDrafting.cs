using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Windows;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace LegalDrafting
{
    public class LegalDraftingRibbonBuilder
    {
        private const string TAB_TITLE = "FSJ CAD Tools";
        private const string TAB_ID = "FSJ_Custom_Tab";
        private const string PANEL_TITLE = "Legal Drafting Tools";

        private RibbonControl _ribbonControl;
        private RibbonTab _ribbonTab;
        private RibbonPanel _ribbonPanel;
        private RibbonPanelSource _panelSource;

        public LegalDraftingRibbonBuilder()
        {
            //Create ribbon tab: FSJ_Custom_Tab 
            _ribbonControl = RibbonServices.RibbonPaletteSet.RibbonControl;

            _ribbonTab = GetExistingFSJCadToolsRibbonTab();
            if (_ribbonTab == null)
            {
                //Create new tab
                _ribbonTab = new RibbonTab();
                _ribbonTab.Title = TAB_TITLE;
                _ribbonTab.Id = TAB_ID;

                _ribbonControl.Tabs.Add(_ribbonTab);
            }
        }

        public void CreateLegalDraftingRibbonPanel()
        {
            //get existing or create new ribbon panel
            if (GetExistingLegalDraftingRibbonPanel()) return;

            //Create new panel, if not exists
            _panelSource = new RibbonPanelSource();
            _panelSource.Title = PANEL_TITLE;

            _ribbonPanel = new RibbonPanel();
            _ribbonPanel.Source = _panelSource;

            _ribbonTab.Panels.Add(_ribbonPanel);

            //Add buttons
            RibbonButton button = new RibbonButton();
            button.Text = "Legend";
            button.CommandParameter = "LegalLegend ";
            button.ShowText = true;
            button.CommandHandler = new LegalDraftingRibbonCommandHandler();

            RibbonToolTip toolTip = new RibbonToolTip();
            toolTip.Command = "LegalLegend"; ;
            toolTip.Title = "Command: LegalLegend";
            toolTip.Content = "Generate legal plan's legend list";
            toolTip.ExpandedContent = "Generate legal plan's legend list " +
                "by searching drawing content visible in the viewports of layouts";
            button.ToolTip = toolTip;

            _panelSource.Items.Add(button);
            _panelSource.Items.Add(new RibbonRowBreak());
        }

        #region private methods

        private RibbonTab GetExistingFSJCadToolsRibbonTab()
        {
            //Find existing ribbon tab
            foreach (var t in _ribbonControl.Tabs)
            {
                if (t.Title.ToUpper() == TAB_TITLE.ToUpper() &&
                    t.Id.ToUpper() == TAB_ID.ToUpper())
                {
                    return t;
                }
            }

            return null;
        }

        private bool GetExistingLegalDraftingRibbonPanel()
        {
            //Find existing panel
            foreach (var p in _ribbonTab.Panels)
            {
                if (p.Source.Title.ToUpper() == PANEL_TITLE.ToUpper())
                {
                    _ribbonPanel = p;
                    _panelSource = p.Source;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region IExtensionApplication

        public void Initialize()
        {
            Document dwg = Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;
            try
            {
                ed.WriteMessage("\nLoading legal drafting utility...");

                //Get database connection
                //_connectionString = SetConnectionString();

                //Load ribbon panel for legal drafting command access
                Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSetCreated +=
                    new EventHandler(RibbonServices_RibbonPaletteSetCreated);

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nLoading legal drafting utility loaded failed:");
                ed.WriteMessage("\n{0}", ex.ToString());
            }

            Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
        }

        //private void RibbonPaletteSet_Loaded(object sender, EventArgs e)
        //{
        //    LoadLegalDraftingRibbonPanel();
        //}

        private void RibbonServices_RibbonPaletteSetCreated(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.Load += RibbonPaletteSet_Load;
                
        }

        private void RibbonPaletteSet_Load(object sender, Autodesk.AutoCAD.Windows.PalettePersistEventArgs e)
        {
            LoadLegalDraftingRibbonPanel(); 
        }

        private static void LoadLegalDraftingRibbonPanel()
        {
            Editor ed = null;
            Document dwg = Application.DocumentManager.MdiActiveDocument;
            if (dwg != null) ed = dwg.Editor;

            try
            {
                LegalDraftingRibbonBuilder rib = new LegalDraftingRibbonBuilder();
                rib.CreateLegalDraftingRibbonPanel();
                if (ed != null)
                {
                    ed.WriteMessage("\nFSJ Custom CAD ribbon tab loaded.");
                }
            }
            catch (System.Exception ex)
            {
                if (ed != null)
                {
                    ed.WriteMessage("\nLoading FSJ Custom CAD ribbon tab failed\n{0}.", ex.ToString());
                }
            }
        }

        public void Terminate()
        {

        }

        #endregion
    }

    public class LegalDraftingRibbonCommandHandler : System.Windows.Input.ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            RibbonButton btn = parameter as RibbonButton;
            if (btn != null)
            {
                Document dwg = Autodesk.AutoCAD.ApplicationServices.Application.
                    DocumentManager.MdiActiveDocument;

                dwg.SendStringToExecute((string)btn.CommandParameter, true, false, true);
            }
        }
    }
}