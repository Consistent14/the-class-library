using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using WinApp = System.Windows.Application;
using res = DotnetAutocad.UIHelper.RibbonImage;
using System.Drawing;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DotnetAutocad.UIHelper
{
    public class RibbonTabProxy
    {
        //---------------------------私有字段-----------------------------//
        //---------------------------私有字段-----------------------------//

        private readonly RibbonControl _rbControl;

        private RibbonTab _ribbonTab;

        private RibbonPanel _rbPanel;

        private RibbonButton _rbButton;

        private List<RibbonButton> _rbButtonCollections;

        private List<RibbonTab> _rbTabCollections;

        private readonly string _rbTabCollectionsJsonPath;

        //---------------------------构造函数-----------------------------//
        //---------------------------构造函数-----------------------------//

        public RibbonTabProxy(string title, string id)
        {
            _rbControl = ComponentManager.Ribbon;

            if (_rbTabCollections == null)
            {
                var tab = new RibbonTab() { Title = title, Id = id };

                _rbTabCollections = new List<RibbonTab>() { tab };

                var midd = new Middle() { RibbonTabs = _rbTabCollections };

                var tabJson = JsonConvert.SerializeObject(midd);

                var assemblyLocation = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

                assemblyLocation = @"D:\Git\the-class-library\DotnetAutocad";

                _rbTabCollectionsJsonPath = Path.Combine(assemblyLocation, $"{nameof(RibbonTabProxy)}.json");

                using (var fileStream = new FileStream(_rbTabCollectionsJsonPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var bytes = Encoding.Default.GetBytes(tabJson);

                    fileStream.Write(bytes, 0, bytes.Length);
                }
            }

            this.Title = title;

            this.Id = id;
        }

        public RibbonTab this[string id]
        {
            get
            {
                if (_rbTabCollections == null)
                {
                    return new RibbonTab();
                }

                if (_rbTabCollections != null && _rbTabCollections.Count == 0)
                {
                    return new RibbonTab();
                }

                var rbBtnCollections = _rbTabCollections.Where(m => m.Id == id).ToList();

                if (rbBtnCollections.Count != 0)
                {
                    return rbBtnCollections.First();
                }
                else
                {
                    return new RibbonTab();
                }
            }
        }

        //---------------------------公开属性-----------------------------//
        //---------------------------公开属性-----------------------------//

        public string Title { get; set; }

        public string Id { get; set; }

        public bool IsActive { get; set; }

        public RibbonControl RbControl => _rbControl;

        //---------------------------私有函数-----------------------------//
        //---------------------------私有函数-----------------------------//

        private RibbonTab CreateRibbonTab()
        {
            _ribbonTab.Title = Title;

            _ribbonTab.Id = Id;

            _ribbonTab.IsActive = IsActive;

            return _ribbonTab;
        }

        private RibbonPanel RibbonPanel(bool isNewPanel)
        {
            if (isNewPanel)
            {
                return new RibbonPanel();
            }
            else
            {
                return _rbPanel;
            }
        }

        private bool IsNotSameTabId(string id)
        {
            if (!File.Exists(_rbTabCollectionsJsonPath))
            {
                return false;
            }

            var json = File.ReadAllText(_rbTabCollectionsJsonPath);

            var middle = JsonConvert.DeserializeObject(json) as Middle;

            if (middle == null)
            {
                return false;
            }

            var rbTabs = middle.RibbonTabs;

            foreach (var item in rbTabs)
            {
                if (item.Id != id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void RibbonTabInit()
        {
            if (_rbTabCollections == null)
            {
                return;
            }

            foreach (var item in _rbTabCollections)
            {
                if (!_rbControl.Tabs.Contains(item))
                {
                    _rbControl.Tabs.Add(item);
                }
            }
        }

        //---------------------------公共函数-----------------------------//
        //---------------------------公共函数-----------------------------//

        public RibbonButton GetRibbonButton(string name, string text)
        {
            var btn = new RibbonButton();

            if (_rbButton == null)
            {
                _rbButton = btn;
            }

            _rbButton.Name = name;

            _rbButton.Text = name;

            _rbButton.ShowImage = true;

            _rbButton.ShowText = true;

            return _rbButton;
        }

        public RibbonButton GetRibbonButton(string name, string text, Bitmap bitmap)
        {
            var btn = new RibbonButton();

            if (_rbButton == null)
            {
                _rbButton = btn;
            }

            _rbButton.Name = name;

            _rbButton.Text = name;

            _rbButton.ShowImage = true;

            _rbButton.ShowText = true;

            _rbButton.Image = ImageHelper.ChangeBitmapToImageSource(bitmap);

            return _rbButton;
        }

        public void AddRibbonButton(RibbonPanelSource rbPanelSource, string name, Bitmap bitmap, bool isNewPanel = false)
        {
            if (_ribbonTab == null) return;

            rbPanelSource.Items.Add(_rbButton);

            var rbPanel = RibbonPanel(isNewPanel);

            rbPanel.Source = rbPanelSource;

            _ribbonTab.Panels.Add(rbPanel);
        }

        public void AddRibbonTab(string name, string id)
        {
            var rbTab = new RibbonTab() { Name = name, Id = id };

            if (IsNotSameTabId(id))
            {
                _rbTabCollections.Add(rbTab);

                RibbonTabInit();
            }
        }

        public RibbonButton FindRibbonButton(string name)
        {
            if (_rbButtonCollections == null)
            {
                return new RibbonButton();
            }

            if (_rbButtonCollections != null && _rbButtonCollections.Count == 0)
            {
                return new RibbonButton();
            }

            var rbBtnCollections = _rbButtonCollections.Where(m => m.Name == name).ToList();

            if (rbBtnCollections.Count != 0)
            {
                return rbBtnCollections.First();
            }
            else
            {
                return new RibbonButton();
            }
        }
    }

    public class Middle
    {
        public List<RibbonTab> RibbonTabs { get; set; }
    }
}
