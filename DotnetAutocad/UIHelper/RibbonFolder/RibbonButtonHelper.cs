using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetAutocad.UIHelper
{
    public class RibbonButtonHelper
    {
        private readonly RibbonButton _ribbonButton;

        public RibbonButton RibbonButton
        {
            get
            {
                if (_ribbonButton == null)
                {
                    return GetRibbonButton();
                }
                else
                {
                    return _ribbonButton;
                }
            }
        }

        private string _name { get; set; }

        private string _text { get; set; }

        public RibbonButtonHelper(string name,string text)
        {
            this._name = name;

            this._text = text;
        }

        private RibbonButton GetRibbonButton()
        {
            var rb = new RibbonButton
            {
                Name = _name,

                Text = _text
            };

            return rb;
        }
    }
}
